using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Battle.Requests
{
    public class BattleActionRequest : IBattleMessage
    {
        public void Dispatch(IBattleMessageSubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
