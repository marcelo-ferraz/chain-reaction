using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainReaction;
using ChainReaction.Configuration;
using ChainReaction.Tests.Model.Notation;
using NUnit.Framework;

namespace ChainReaction.Tests
{
    [TestFixture]
    public class NotationTests : AssertionHelper
    {
        IChainReactionContainer _container;

        [SetUp]
        public void SetUpAttribute()
        {
            _container = Configure
                .This(new SimpleChainReactionContainer())
                .With(InputFrom.Annotations(this.GetType().Assembly))
                .Container;
        }

        [Test]
        public void SimpleScenarioTest()
        {
            Logger logger1 = null;

            var uselessProcess = 
                _container.Invoke<UselessProcessing>(
                afterLoadHandler: listener => 
                      logger1 = listener as Logger);

            uselessProcess.Start();

            var log =
                logger1.Builder.ToString();

            ContainsSubstring("It was initiated!").Matches(log);
            ContainsSubstring("I is in the middle...").Matches(log);
            ContainsSubstring("The end!").Matches(log);

            Not.ContainsSubstring("Supposed To be listened two times, but not on appConfig.").Matches(log);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log);
        }

        [Test]
        public void TadMoreComplexScenarioTest()
        {
            Logger logger = null;

            var moreUselessProcess = 
                _container.Invoke<MoreUselessProcessing>( 
                afterLoadHandler: listener => 
                    logger = listener as Logger);

            moreUselessProcess.Start();

            var log =
                logger.Builder.ToString();

            Not.ContainsSubstring("It was initiated!").Matches(log);
            Not.ContainsSubstring("I is in the middle...").Matches(log);
            Not.ContainsSubstring("The end!").Matches(log);

            ContainsSubstring("Supposed To be listened two times, but not on appConfig.").Matches(log);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log);
        }
        
        [Test]
        public void SimpleScenarioTestReusingLoggerInstance()
        {
            Logger logger = new Logger();

            var moreUselessProcess =
                _container.Invoke<MoreUselessProcessing>(handlers: logger);

            moreUselessProcess.Start();

            var log =
                logger.Builder.ToString();

            Not.ContainsSubstring("It was initiated!").Matches(log);
            Not.ContainsSubstring("I is in the middle...").Matches(log);
            Not.ContainsSubstring("The end!").Matches(log);

            ContainsSubstring("Supposed To be listened two times, but not on appConfig.").Matches(log);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log);
        }

        [Test]
        public void TestReusingNonMappedType()
        {
            var notSuitableObject = new System.Collections.ArrayList();

            var moreUselessProcess =
                _container.Invoke<MoreUselessProcessing>(handlers: notSuitableObject);

            moreUselessProcess.Start();

            var log =
                notSuitableObject.Builder.ToString();

            Not.ContainsSubstring("It was initiated!").Matches(log);
            Not.ContainsSubstring("I is in the middle...").Matches(log);
            Not.ContainsSubstring("The end!").Matches(log);

            ContainsSubstring("Supposed To be listened two times, but not on appConfig.").Matches(log);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log);
        }
    }
}