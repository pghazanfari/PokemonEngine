using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class UseRun : IAction
    {
        public UseRun(Slot slot) : base(slot) { }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
