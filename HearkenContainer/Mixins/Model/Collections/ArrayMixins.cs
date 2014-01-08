using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;

namespace HearkenContainer.Mixins.Model.Collections
{
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
            Array.Copy(array, index, array, index + count, array.Length - (index + count));
            Array.Clear(array, index, count);
            array[index] = val;
            return array;
        }

        internal static T[] Create<T>(int length = 0)
        {
            return (T[])Array.CreateInstance(typeof(T), length);
        }
    }
}
