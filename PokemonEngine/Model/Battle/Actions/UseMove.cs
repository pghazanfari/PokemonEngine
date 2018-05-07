using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class UseMove : IBattleAction
    {
        private readonly BattleSlot user;
        public BattleSlot User { get { return user; } }

        private readonly IBattleMove move;
        public IBattleMove Move { get { return move; } }

        public UseMove(BattleSlot user, IBattleMove move)
        {
            this.user = user;
            this.move = move;
        }

        public void Dispatch(IBattleMessageSubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
