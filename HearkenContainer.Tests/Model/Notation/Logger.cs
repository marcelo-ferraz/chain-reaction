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
        StringBuilder builder = new StringBuilder();

        [Function("Init")]
        [Function("ListenedTwice")]
        public void ListenToMoreThanOne(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }

        [Function]
        public void ListenedTwice(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }

        [Function]
        public void Middle(string sentence) 
        {
            builder.Append(sentence);
            builder.Append("|");
        }
        
        [Function]
        public void End(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }
    }
}
