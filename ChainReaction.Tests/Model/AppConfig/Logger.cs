using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainReaction;
using ChainReaction.Configuration;
using ChainReaction.Notations;

namespace ChainReaction.Tests.Model.AppConfig
{
    public class Logger
    {
        public Logger()
        {
            Builder =
                new StringBuilder();
        }

        public StringBuilder Builder { get; set; }

        public void ListenToMoreThanOne(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }

        public void ListenedTwice(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }

        public void Middle(string sentence) 
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }
        
        public void End(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }
    }
}
