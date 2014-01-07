using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearkenContainer.Sources.Model
{
    public class SourceNotFoundException : Exception
    {
        private string p;

        public SourceNotFoundException(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
