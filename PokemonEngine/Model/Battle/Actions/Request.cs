﻿using System;
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

        private readonly Slot slot;
        public Slot Slot { get { return slot; } }

        public Request(Team team, Slot slot)
        {
            if (!team.Contains(slot)) { throw new ArgumentException("This slot provided is not part of the given team", "slot"); }
            this.team = team;
            this.slot = slot;  
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
