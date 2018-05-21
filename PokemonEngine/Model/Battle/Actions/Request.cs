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
        private readonly Team team;
        public Team Team { get { return team; } }

        private readonly int slot;
        public int Slot { get { return slot; } }

        public Request(Team team, int slot)
        {
            this.team = team;
            this.slot = slot;
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
