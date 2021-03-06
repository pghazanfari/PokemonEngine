﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public abstract class IAction : IMessage// TODO: , IComparable<Action>
    {
        public Slot Slot { get; }

        public abstract int Priority { get; }

        public IAction( Slot slot)
        {
            Slot = slot;
        }

        public abstract void Dispatch(ISubscriber receiver);

        public void Dispose()
        {
            //Do Nothing
        }
    }
}
