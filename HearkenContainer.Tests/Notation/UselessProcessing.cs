using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer.Notations;

namespace Test.Notation
{
    [TriggerSource]
    public class UselessProcessing
    {
        [Trigger]
        public event Action<String> Init;
        
        [Trigger]
        public event Action<String> Middle;
        
        public event Action<String> Ignored;

        [Trigger]
        public event Action<string> ListenedTwice;

        [Trigger]
        public event Action<String> End;
        
        public void TriggerThemAll()
        {
            if (Init != null)
            { Init("It was initiated!"); }
            if (Middle != null)
            { Middle("I is in the middle..."); }
            if (ListenedTwice != null)
            { ListenedTwice("Supposed To be listened two times."); }
            if (Ignored != null)
            { Ignored("Although it was put to be called, no one is supposed to be listening."); }
            if (End != null)
            { End("The end!"); }
        }
    }
}
