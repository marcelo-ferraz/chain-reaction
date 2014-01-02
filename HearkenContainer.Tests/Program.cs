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
                .Container;

            var uProcess = con.Get<UselessProcessing>();

            uProcess.TriggerThemAll();
        }
    }
}
