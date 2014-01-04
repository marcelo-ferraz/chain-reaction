using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer.Notations;

namespace HearkenContainer.Tests.Model.Notation
{
    [TriggerSource]
    public class UselessProcessing
    {
        public event Action<String> Init;
        
        public event Action<String> Middle;

        public event Action<String> End;
        
        public virtual void Start()
        {
            if (Init != null)
            { Init("It was initiated!"); }
            if (Middle != null)
            { Middle("I is in the middle..."); }
            if (End != null)
            { End("The end!"); }
        }
    }
}
