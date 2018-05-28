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
        public readonly IReadOnlyList<Slot> Targets;

        public UseMove(Slot slot, IMove move, IList<Slot> targets) : base(slot)
        {
            Move = move;
            Targets = new List<Slot>(targets).AsReadOnly();
        }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
