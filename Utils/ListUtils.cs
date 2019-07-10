using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoMapConverter.Utils
{
    public static class ListUtils
    {
        /// <summary>
        /// Merges two collections that are identical in length.
        /// For example, if the first collection contains "1", "2", "3"
        /// and the second collection contains "a", "b", "c" then the
        /// resulting sequence will be "1", "a", "2", "b", "3", "c"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">First collection</param>
        /// <param name="second">Second collection</param>
        /// <returns></returns>
        public static IEnumerable<T> InterleaveMerge<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            using (IEnumerator<T>
                enumerator1 = first.GetEnumerator(),
                enumerator2 = second.GetEnumerator())
            {
                while (enumerator1.MoveNext() && enumerator2.MoveNext())
                {
                    yield return enumerator1.Current;
                    yield return enumerator2.Current;
                }
            }
        }
    }
}
