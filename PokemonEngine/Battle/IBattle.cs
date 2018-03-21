using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Battle
{
    public interface IBattle
    {
        IReadOnlyList<BattleTeam> Teams { get; }
    }
}
