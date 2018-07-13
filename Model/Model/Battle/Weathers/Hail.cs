using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Weathers
{
    public class Hail : Weather
    {
        public Hail() : base() { }
        public Hail(int turnCount) : base(turnCount) { }
    }
}
