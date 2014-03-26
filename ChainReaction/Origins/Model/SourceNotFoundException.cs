using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChainReaction.Origins.Model
{
    public class SourceNotFoundException : Exception
    {
        public SourceNotFoundException(string msg, Exception inner)
            : base(msg, inner) {}        
    }
}
