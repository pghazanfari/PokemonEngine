using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model;
using PokemonEngine.Model.Battle;

namespace ModelUnitTests.Util
{
    public class NullAbility : Ability
    {
        private class NullEffect : Effect
        {
        }

        public NullAbility() : base("null", "null")
        {
        }

        public override Effect newEffect(IBattle battle, PokemonEngine.Model.Battle.IPokemon pokemon)
        {
            return new NullEffect();
        }
    }
}
