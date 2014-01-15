using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer;
using HearkenContainer.Configuration;
using HearkenContainer.Notations;

namespace HearkenContainer.Tests.Model.Notation
{
    [Action]
    public class Logger
    {
        public Logger()
        {
            Builder =
                new StringBuilder();
        }

        public StringBuilder Builder { get; set; }

        [Function("Init")]
        public void ListenToMoreThanOne(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }

        [Function]
        public void ListenedTwice(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }

        [Function]
        [Function("ListenedTwice")]
        public void Middle(string sentence) 
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }
        
        [Function]
        public void End(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }
    }
}
