using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Weathers
{
    public class ClearSkies : Weather
    {
        public ClearSkies() : base() { }
        public ClearSkies(int turnCount) : base(turnCount) { }
    }
}
