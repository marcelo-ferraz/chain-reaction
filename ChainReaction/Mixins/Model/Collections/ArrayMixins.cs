using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;

namespace ChainReaction.Mixins.Model.Collections
{
    /// <summary>
    /// Still quite messy and not as performatic as I'd initially wanted. Majorly works, but still looks quite crappy
    /// </summary>
    public static class ArrayMixins
    {
        private delegate bool TrySZBinarySearchFunction(Array sourceArray, int sourceIndex, int count, Object value, out int retVal);
                
        static ArrayMixins()
        {
            _trySZBinarySearch = 
                Get<TrySZBinarySearchFunction>("TrySZBinarySearch");            
        }

        private static TrySZBinarySearchFunction _trySZBinarySearch;
        
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        private static T Get<T>(string functionName) 
        {
            var method = typeof(Array)
                .GetMethod(functionName, BindingFlags.NonPublic | BindingFlags.Static);
            
            return (T) (object) Delegate.CreateDelegate(typeof(T), method);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static T BinaryFind<T>(this T[] self, object criteria)
        {
            int index;
            
            //return _trySZBinarySearch(self, 0, self.Length, criteria, out index) ?
            //    self[index] :
            //    default(T);

            index = Array.BinarySearch(self, criteria);

            return index > -1 ?
                self[index] :
                default(T);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static bool BinarySearch<T>(this T[] self, object criteria, out int index)
        {
            return _trySZBinarySearch(self, 0, self.Length, criteria, out index);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static T[] Insert<T>(this T[] array, int index, T val)
        {
            int count = 1;

            Array.Resize(ref array, array.Length + 1);
            Array.Copy(array, index, array, index + count, array.Length - (index + count)); ; 
            
            Array.Clear(array, index, count);
            array[index] = val;
            return array;
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        internal static T[] Create<T>(int length = 0)
        {
            return (T[])Array.CreateInstance(typeof(T), length);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static T[] Resize<T>(this T[] array, int newSize)
        {
            Array newArray = Array.CreateInstance(typeof(T), newSize);
            Array.Copy(array, 0, newArray, 0, Math.Min(newArray.Length, newSize));
            return (T[]) newArray;
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        internal static T[] Union<T>(this T[] self, T[] otherArray, Func<T, T, bool> predicate = null)
        {
            return self.Union(otherArray, t => t, predicate);
        }


        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        internal static T1[] Union<T1, T2>(this T1[] self, T2[] otherArray, Func<T2, T1> parse, Func<T1, T2, bool> predicate = null)
        {
            if (predicate == null)
            { predicate = (t1, t2) => object.Equals(t1, t2); }

            for (int i = 0; i < otherArray.Length; i++)
            {
                var found =
                    self.Foremost((j, item) => predicate(item, otherArray[i]));

                if (found.Value == null)
                { self = self.Insert(self.Length, parse(otherArray[i])); }
            }

            return self;
        }
    }
}
