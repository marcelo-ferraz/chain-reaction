using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainReaction.Configuration;
using NUnit.Framework;
using AppCfg = ChainReaction.Tests.Model.AppConfig;
using Annotations = ChainReaction.Tests.Model.Notation;
namespace ChainReaction.Tests
{
    public class MixedOriginsTest : AssertionHelper
    {
        [Test]
        public void AppCfgAndNotationOrigin()
        {
            var container = Configure
                .This<SimpleChainReactionContainer>()
                .With(InputFrom.AppConfig())
                .With(InputFrom.Annotations(this.GetType().Assembly))
                .Container;

            AppCfg.Logger appCfgLogger = null;
            Annotations.Logger notedLogger = null;

            var uselessProcess = container.Invoke<AppCfg.UselessProcessing>(
                group: "oneGroup"
                ,afterLoad: listener => 
                    GetLoggers(ref appCfgLogger, ref notedLogger, listener));

            uselessProcess.Start();

            var log01 =
                appCfgLogger.Builder.ToString();

            ContainsSubstring("It was initiated!").Matches(log01);
            ContainsSubstring("I is in the middle...").Matches(log01);
            ContainsSubstring("The end!").Matches(log01);

            Not.ContainsSubstring("Supposed To be listened two times, but not on appConfig.").Matches(log01);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log01);

            var log02 =
                notedLogger.Builder.ToString();

            ContainsSubstring("It was initiated!").Matches(log02);
            ContainsSubstring("I is in the middle...").Matches(log02);
            ContainsSubstring("The end!").Matches(log02);

            Not.ContainsSubstring("Supposed To be listened two times, but not on appConfig.").Matches(log02);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log02);

            appCfgLogger = null;
            notedLogger = null;

            var moreUselessProcess = container.Invoke<Annotations.MoreUselessProcessing>(
                    afterLoad: listener =>
                        GetLoggers(ref appCfgLogger, ref notedLogger, listener));

            moreUselessProcess.Start();

            var log03 =
                appCfgLogger.Builder.ToString();

            EqualTo("Supposed To be listened two times, but not on appConfig.|").Matches(log03);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log03);

            var log04 =
                notedLogger.Builder.ToString();

            Not.EqualTo("Supposed To be listened two times, but not on appConfig.|").Matches(log04);
            EqualTo("Supposed To be listened two times, but not on appConfig.|Supposed To be listened two times, but not on appConfig.|").Matches(log04);
            Not.ContainsSubstring("Although it was put to be called, no one is supposed to be listening.").Matches(log04);
        }

        private static void GetLoggers(ref AppCfg.Logger appCfgLogger, ref Annotations.Logger notaLogger, object listener)
        {
            if (listener is AppCfg.Logger)
            { appCfgLogger = listener as AppCfg.Logger; }
            else if (listener is Annotations.Logger)
            { notaLogger = listener as Annotations.Logger; }
        }
    }
}
