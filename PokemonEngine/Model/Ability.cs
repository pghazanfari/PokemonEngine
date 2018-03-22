using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class Ability
    {
        public string Description { get; private set; }

        public Ability(string description)
        {
            Description = description;
        }
    }
}
