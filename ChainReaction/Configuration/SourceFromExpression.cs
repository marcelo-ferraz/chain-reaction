using System;
using System.Reflection;
using ChainReaction.Origins;

namespace ChainReaction.Configuration
{
    public static class InputFrom
    {
        /// <summary>
        /// </summary>
        /// <param name="getTypes"></param>
        /// <returns></returns>
        public static NotationOrigin Annotations(params Func<Type>[] getTypes)
        {
            var source =
                new NotationOrigin();

            for (int i = 0; i < getTypes.Length; i++)
            {
                source.Types.Add(getTypes[i]());   
            }           

            return source;
        }

        /// <summary>
        /// </summary>
        /// <param name="getTypes"></param>
        /// <returns></returns>
        public static NotationOrigin Annotations(Func<Type[]> getTypes)
        {
            var source =
                new NotationOrigin();

            source.Types.AddRange(getTypes());

            return source;
        }

        /// <summary>
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static NotationOrigin Annotations(params Type[] types)
        {
            var source = 
                new NotationOrigin();
            
            source.Types.AddRange(types);

            return source;
        }

        /// <summary>
        /// Explicits that One source of groups should come from a given set of assemblies
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static NotationOrigin Annotations(params Assembly[] assemblies)
        {
            var source = 
                new NotationOrigin();

            for (int i = 0; i < assemblies.Length; i++)
            {
                source.Types.AddRange(assemblies[i].GetTypes());
            }

            return source;
        }

        public static IOrigin AppConfig()
        {
            return new AppConfigOrigin();
        }
    }
}
