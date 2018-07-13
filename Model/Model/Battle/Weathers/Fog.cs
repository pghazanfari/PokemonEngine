using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Weathers
{
    public class Fog : Weather
    {
        public Fog() : base() { }
        public Fog(int turnCount) : base(turnCount) { }
    }
}
