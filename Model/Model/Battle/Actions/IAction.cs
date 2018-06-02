using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public abstract class IAction : IMessage// TODO: , IComparable<Action>
    {
        private readonly Slot slot;
        public Slot Slot { get { return slot; } }

        public abstract int Priority { get; }

        public IAction( Slot slot)
        {
            this.slot = slot;
        }

        public abstract void Dispatch(ISubscriber receiver);
    }
}
