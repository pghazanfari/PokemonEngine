using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class UseItem : IAction
    {
        //TODO: Implement Items

        public UseItem(Slot slot) : base(slot)
        {
        }

        public override int Priority
        {
            get
            {
                return 6;
            }
        }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
