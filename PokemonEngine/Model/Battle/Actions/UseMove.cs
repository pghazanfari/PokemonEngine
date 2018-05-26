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
        public readonly IMove Move;

        public UseMove(Team team, Slot slot, IMove move) : base(team, slot)
        {
            Move = move;
        }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
