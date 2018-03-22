using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PokemonEngine.Model.Battle
{
    public interface IBattlePokemon : IUniquePokemon
    {
        BattleStats BattleStats { get; }
        MoveSet<IBattleMove> BattleMoves { get; }
    }
}
