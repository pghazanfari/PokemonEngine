using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle
{
    public abstract class Effect : Messaging.IMessage
    {
        public void Dispatch(ISubscriber receiver) { receiver.Receive(this); }
        public abstract void Execute();
    }
}
