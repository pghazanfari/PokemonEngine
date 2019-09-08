using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle;

namespace PokemonEngine.Model
{
    public abstract class Ability
    {
        public string Name { get; }
        public string Description { get; }

        public Ability(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract Effect newEffect(IBattle battle, Battle.IPokemon pokemon);
    }
}
