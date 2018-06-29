using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public abstract class InflictDamage : IMessage
    {
        public abstract IReadOnlyCollection<Slot> Targets { get; }
        public abstract int this[Slot slot] { get; }

        public void Apply()
        {
            foreach (Slot slot in Targets)
            {
                slot.Pokemon.UpdateHP(-this[slot]);
            }
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
