using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer;
using HearkenContainer.Configuration;
using Test.Notation;


namespace Test
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
            
            Logger logger = null;

            var uProcess = con.Invoke<UselessProcessing>(
                listener => logger = listener as Logger);

            uProcess.TriggerThemAll();
        }
    }
}
