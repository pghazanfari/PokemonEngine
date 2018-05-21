using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class UseMove : IAction
    {
        private readonly Slot user;
        public Slot User { get { return user; } }

        private readonly IMove move;
        public IMove Move { get { return move; } }

        public UseMove(Slot user, IMove move)
        {
            this.user = user;
            this.move = move;
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
