using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    // Taken from: https://bulbapedia.bulbagarden.net/wiki/Double_Battle
    public enum MoveTarget
    {
        AnyFoe,
        AllFoes,
        AllOtherPokemon,
        AllPokemon,
        Self,
        SelfOrAlly,
        Ally,
        Team
    }
}
