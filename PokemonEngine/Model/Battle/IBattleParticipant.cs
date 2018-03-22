using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public interface IBattleParticipant
    {
        IReadOnlyList<IBattlePokemon> BattlingPokemon { get; }
        Party Party { get; }
    }

    public static class IBattleParticipantImpl
    {
        public static bool HasLost(this IBattleParticipant participant)
        {
            return participant.Party.Pokemon.All(x => x.HasFainted());
        }
    }
}
