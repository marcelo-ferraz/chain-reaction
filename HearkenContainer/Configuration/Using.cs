using System;
using System.Reflection;
using HearkenContainer.Sources;

namespace HearkenContainer.Configuration
{
    public static class Using
    {
        /// <summary>
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static NotationSource Annotations(params Type[] types)
        {
            var source = 
                new NotationSource();
            
            source.Types.AddRange(types);

            return source;
        }

        /// <summary>
        /// Explicits that One source of groups should come from a given set of assemblies
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static NotationSource Annotations(params Assembly[] assemblies)
        {
            var source = 
                new NotationSource();

            for (int i = 0; i < assemblies.Length; i++)
            {
                source.Types.AddRange(assemblies[i].GetTypes());
            }

            return source;
        }
    }
}
