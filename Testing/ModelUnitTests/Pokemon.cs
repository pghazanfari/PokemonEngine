using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelUnitTests.PokemonImpl;

namespace ModelUnitTests
{
    public class Pokemon
    {
        public static readonly Bulbasaur Bulbasaur = Bulbasaur.Instance;

        private Pokemon() { }
    }
}
