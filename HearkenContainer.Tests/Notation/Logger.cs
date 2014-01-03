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
        StringBuilder builder = new StringBuilder();

        [Action("Init")]
        [Action("ListenedTwice")]
        public void ListenToMoreThanOne(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }

        [Action]
        public void ListenedTwice(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }

        [Action]
        public void Middle(string sentence) 
        {
            builder.Append(sentence);
            builder.Append("|");
        }
        
        [Action]
        public void End(string sentence)
        {
            builder.Append(sentence);
            builder.Append("|");
        }
    }
}
