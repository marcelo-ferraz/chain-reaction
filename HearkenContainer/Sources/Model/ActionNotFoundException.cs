using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearkenContainer.Sources.Model
{
    class ActionNotFoundException : Exception
    {
        private string p;

        public ActionNotFoundException(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
