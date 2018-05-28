using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common.Sequence
{
    public interface ISequence<T> : IEnumerable<T>
    {
        int Count { get; }
        T First { get; }
        T Next();
        bool Contains(T obj);
    }
    public static class ISequenceImpl
    {
        public static bool HasNext<T>(this ISequence<T> sequence)
        {
            return sequence.Count > 0;
        }

        public static void ToEnd<T>(this ISequence<T> sequence)
        {
            while (sequence.Count > 0)
            {
                sequence.Next();
            }
        }
    }
}
