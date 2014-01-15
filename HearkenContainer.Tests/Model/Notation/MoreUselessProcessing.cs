using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer.Notations;

namespace HearkenContainer.Tests.Model.Notation
{
    [Source]
    public class MoreUselessProcessing : UselessProcessing
    {        
        public event Action<String> Ignored;

        [Trigger]
        public event Action<string> ListenedTwice;

        public override void Start()
        {
            base.Start();

            if (ListenedTwice != null)
            { ListenedTwice("Supposed To be listened two times, but not on appConfig."); }
            if (Ignored != null)
            { Ignored("Although it was put to be called, no one is supposed to be listening."); }
        }
    }
}
