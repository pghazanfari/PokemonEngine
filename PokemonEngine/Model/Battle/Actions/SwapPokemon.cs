using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Unique;
using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class SwapPokemon : IAction
    {
        public readonly Unique.IPokemon Replacement;

        public SwapPokemon(Team team, Slot slot, Unique.IPokemon replacement) : base(team, slot)
        {
            if (!slot.Participant.Party.Contains(replacement)) { throw new ArgumentException("Replacement pokemon is not part of the Participant's Party", "replacement"); }
            if (!replacement.HasFainted()) { throw new ArgumentException("Replacement pokemon has fainted", "replacement");  }
            Replacement = replacement;
        }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
