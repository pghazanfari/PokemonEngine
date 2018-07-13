using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Weathers
{
    public class Sandstorm : Weather
    {
        public Sandstorm() : base() { }
        public Sandstorm(int turnCount) : base(turnCount) { }
    }
}
