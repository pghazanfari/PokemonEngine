using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;

namespace PokemonEngine.Battle
{
    interface IBattlePokemon : IUniquePokemon
    {
        BattleStats BattleStats { get; }
    }
}
