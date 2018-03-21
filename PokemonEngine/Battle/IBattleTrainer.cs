using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;

namespace PokemonEngine.Battle
{
    public interface IBattleTrainer : ITrainer
    {
        IReadOnlyList<IBattlePokemon> BattlingPokemon { get; }
        void SwapPokemon(IBattlePokemon inPlayPokemon, IUniquePokemon partyPokemon);

        //TODO: Pokemon Swap Events
    }
}
