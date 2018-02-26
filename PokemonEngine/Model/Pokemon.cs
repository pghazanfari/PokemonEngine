using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class Pokemon
    {
        public IReadOnlyList<PType> Types { get; }
    }
}
