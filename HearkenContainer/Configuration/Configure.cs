using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer.Sources;

namespace HearkenContainer.Configuration
{
    public sealed class Configure
    {
        private Configure() { }

        public IHearkenContainer Container { get; set; }

        public static Configure This()
        {
            var instance = new Configure();
            instance.Container = new SimpleDispatchContainer();
            return instance;
        }

        public static Configure This(IHearkenContainer container)
        {
            var instance = new Configure();
            instance.Container = container;
            return instance;
        }

        public Configure Source(ISource source)
        {
            source.Save(Container);

            return this;
        }
    }
}