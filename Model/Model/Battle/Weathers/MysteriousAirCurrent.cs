using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Weathers
{
    public class MysteriousAirCurrent : Weather
    {
        public MysteriousAirCurrent() : base() { }
        public MysteriousAirCurrent(int turnCount) : base(turnCount) { }
    }
}
