using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearkenContainer.Model
{
    public interface IHasTypedInfo
    {
        Type Type { get; set; }
    }
}
