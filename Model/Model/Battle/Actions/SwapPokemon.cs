using System;

using PokemonEngine.Model.Unique;
using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class SwapPokemon : IAction
    {
        public Unique.IPokemon Replacement { get; }

        public SwapPokemon(Slot slot, Unique.IPokemon replacement) : base(slot)
        {
            if (!slot.Participant.Party.Contains(replacement)) { throw new ArgumentException("Replacement pokemon is not part of the Participant's Party", "replacement"); }
            if (!replacement.HasFainted()) { throw new ArgumentException("Replacement pokemon has fainted", "replacement");  }
            Replacement = replacement;
        }

        public override int Priority
        {
            get
            {
                return 6;
            }
        }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
