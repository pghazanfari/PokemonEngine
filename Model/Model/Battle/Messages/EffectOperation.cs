using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public abstract class EffectOperation : IMessage
    {
        public readonly Effect Effect;

        public EffectOperation(Effect effect)
        {
            Effect = effect;
        }

        public abstract void PerformOperation(IBattle battle);

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
