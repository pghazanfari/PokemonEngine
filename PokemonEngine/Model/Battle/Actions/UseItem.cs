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

        public UseItem(Team team, Slot slot) : base(team, slot)
        {
        }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
