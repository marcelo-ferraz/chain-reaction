using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer;
using HearkenContainer.Configuration;
using HearkenContainer.Notations;

namespace Test.Notation
{
    [ActionHolder]
    public class Logger
    {
        [Action("Init")]
        public void ListenToInit(string sentence)
        {

        }

        [Action]
        public void Middle(string sentence) { }
        
        [Action]
        public void End(string sentence) { }
    }
}
