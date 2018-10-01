using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Battle.Actions
{
    public class Request : IMessage
    {
        private readonly Slot slot;
        public Slot Slot { get { return slot; } }

        public Request(Slot slot)
        {
            this.slot = slot;  
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }

        public void Dispose()
        {
            // Do Nothing
        }
    }
}
