using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer;
using HearkenContainer.Configuration;
using HearkenContainer.Tests.Model.Notation;

namespace HearkenContainer.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var con = Configure
                .This(new SimpleDispatchContainer())
                .Source(Using.Annotations(typeof(Program).Assembly))
                //.Source(Using.AppConfig())
                .Container;
            
             Logger logger1 = null;

            var uselessProcess = con.Invoke<UselessProcessing>(
                listener => logger1 = listener as Logger);

            uselessProcess.Start();

            Logger logger2 = null;

            var moreUselessProcess = con.Invoke < MoreUselessProcessing > (
                   listener => logger2 = listener as Logger);
            
            moreUselessProcess.Start();
        }
    }
}
