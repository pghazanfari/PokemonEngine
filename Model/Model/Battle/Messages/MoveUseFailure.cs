using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public class MoveUseFailure : IMessage
    {
        public readonly IMove Move;

        public MoveUseFailure(IMove move)
        {
            Move = move;
        }

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
