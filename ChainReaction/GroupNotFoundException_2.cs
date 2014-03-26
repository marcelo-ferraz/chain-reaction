using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChainReaction.Model
{
    class GroupNotFoundException : Exception
    {
        private string p;
        private string groupName;

        public GroupNotFoundException(string p, string groupName)
        {
            // TODO: Complete member initialization
            this.p = p;
            this.groupName = groupName;
        }
    }
}
