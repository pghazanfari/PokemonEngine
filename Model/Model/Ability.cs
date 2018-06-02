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
        public readonly string Name;
        public readonly string Description;

        public Ability(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void OnBattleStart(IBattle battle);
        public abstract void OnEnterBattle(IBattle battle, Battle.IPokemon pokemon);
        public abstract void OnExitBattle(IBattle battle, Battle.IPokemon pokemon);
    }
}
