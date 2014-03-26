using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainReaction;
using ChainReaction.Configuration;
using ChainReaction.Tests.Model.AppConfig;
using NUnit.Framework;

namespace ChainReaction.Tests
{
    [TestFixture]
    public class AppConfigTest: AssertionHelper
    {
        IChainReactionContainer _container;
        [SetUp]
        public void SetUpAttribute()
        {
            _container = Configure
                .This(new SimpleChainReactionContainer())                
                .Source(Using.AppConfig())
                .Container;
        }

        [Test]
        public void CorrectScenarioTest()
        {
            Logger logger1 = null;

            var uselessProcess = _container.Invoke<UselessProcessing>(
                afterLoad: listener => logger1 = listener as Logger);

            uselessProcess.Start();

            var log = 
                logger1.Builder.ToString();

            ContainsSubstring("It was initiated!").Matches(log);
            ContainsSubstring("I is in the middle...").Matches(log);
            ContainsSubstring("The end!").Matches(log);

            Not.ContainsSubstring("Supposed To be listened two times, but not on appConfig.").Matches(log);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log);

            Logger logger2 = null;

            var moreUselessProcess = _container.Invoke<MoreUselessProcessing>(
                   afterLoad: listener => logger2 = listener as Logger);
            
            moreUselessProcess.Start();

            var log2 =
                logger2.Builder.ToString();

            ContainsSubstring("Supposed To be listened two times, but not on appConfig.").Matches(log2);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log2);
        }
    }
}
