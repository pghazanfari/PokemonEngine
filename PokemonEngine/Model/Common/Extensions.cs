using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common
{
    public static class Extensions
    {
        public static bool ContainsDuplicates<T>(this IList<T> collection)
        {
            return collection.Distinct().Count() == collection.Count;
        }

        public static bool ContainsNull<T>(this IList<T> collection)
        {
            // Using collection.Contains(null) does not work because of generics
            return collection.Any(x => x == null);
        }

        public static IReadOnlyDictionary<K,V> AsReadOnly<K,V>(this IDictionary<K,V> dictionary)
        {
            return new ReadOnlyDictionary<K, V>(dictionary);
        }
    }
}
