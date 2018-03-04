using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class PAbility
    {
        public string Description { get; private set; }

        public PAbility(string description)
        {
            Description = description;
        }
    }
}
