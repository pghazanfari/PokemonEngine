using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common.Sequence
{
    public class Sequence<T> : ISequence<T>
    {
        private IMutableSequence<T> underlyingSequence;

        public Sequence(IMutableSequence<T> mutableSequence)
        {
            this.underlyingSequence = new MutableSequence<T>(mutableSequence);
        }

        public Sequence(IEnumerable<T> enumerable)
        {
            underlyingSequence = new MutableSequence<T>(enumerable);
        }

        public int Count
        {
            get
            {
                return ((ISequence<T>)underlyingSequence).Count;
            }
        }

        public T First
        {
            get
            {
                return underlyingSequence.First;
            }
        }

        public bool Contains(T obj)
        {
            return ((ISequence<T>)underlyingSequence).Contains(obj);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return underlyingSequence.GetEnumerator();
        }

        public T Next()
        {
            return underlyingSequence.Next();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return underlyingSequence.GetEnumerator();
        }
    }
}
