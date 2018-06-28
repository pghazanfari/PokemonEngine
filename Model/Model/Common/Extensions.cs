﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common
{
    public static class Extensions
    {
        public static bool ContainsDuplicates<T>(this IEnumerable<T> collection)
        {
            HashSet<T> set = new HashSet<T>();
            return collection.Any(x => !set.Add(x));
        }

        public static bool ContainsNull<T>(this IEnumerable<T> collection)
        {
            // Using collection.Contains(null) does not work because of generics
            return collection.Any(x => x == null);
        }

        public static IReadOnlyDictionary<K, V> AsReadOnly<K, V>(this IDictionary<K, V> dictionary)
        {
            return new ReadOnlyDictionary<K, V>(dictionary);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, params T[] list) {
            return collection.Except(list as IEnumerable<T>);
        }
    }
}
