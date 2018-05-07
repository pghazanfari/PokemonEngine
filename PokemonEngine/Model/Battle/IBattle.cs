using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle
{
    public interface IBattle : IBattleMessageSubscriber
    {
        IReadOnlyList<BattleTeam> Teams { get; }
        bool IsTurnComplete { get; }
        void ResetTurn();
    }
}
