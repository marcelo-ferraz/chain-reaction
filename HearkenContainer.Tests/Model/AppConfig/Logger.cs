using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer;
using HearkenContainer.Configuration;
using HearkenContainer.Notations;

namespace HearkenContainer.Tests.Model.AppConfig
{
    public class Logger
    {
        StringBuilder builder = new StringBuilder();

        public void ListenToMoreThanOne(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }

        public void ListenedTwice(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }

        public void Middle(string sentence) 
        {
            builder.Append(sentence);
            builder.Append("|");
        }
        
        public void End(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }
    }
}
