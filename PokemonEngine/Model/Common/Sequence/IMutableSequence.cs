using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common.Sequence
{
    public interface IMutableSequence<T> : ISequence<T>, ICollection<T>
    {
        void Replace(T existingObj, T newObj);
        void Swap(T firstObj, T secondObj);
    }
}
