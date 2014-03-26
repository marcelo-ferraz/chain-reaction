using System;
using System.Text;
using ChainReaction.Notations;

namespace ChainReaction.Tests.Model.Notation
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
