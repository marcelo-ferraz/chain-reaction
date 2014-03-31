using System;
using System.Text;
using ChainReaction.Notations;

namespace ChainReaction.Tests.Model.Notation
{
    [Handler]
    public class Logger
    {
        public Logger()
        {
            Builder =
                new StringBuilder();
        }

        public StringBuilder Builder { get; set; }

        [Action("Init")]
        public void ListenToMoreThanOne(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }

        [Action]
        public void ListenedTwice(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }

        [Action]
        [Action(EventName="ListenedTwice")]
        public void Middle(string sentence) 
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }
        
        [Action]
        public void End(string sentence)
        {
            Builder.Append(sentence);
            Builder.Append("|");
        }
    }
}
