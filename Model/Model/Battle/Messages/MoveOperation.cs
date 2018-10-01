using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public abstract class MoveOperation : IMessage
    {
        public readonly IMove Move;

        public MoveOperation(IMove move)
        {
            Move = move;
        }

        public abstract void PerformOperation(IBattle battle);

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }

        public void Dispose()
        {
            //Do Nothing
        }
    }
}
