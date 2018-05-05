using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class Run : IBattleAction, IMessage<Run>
    {
        private readonly BattleSlot user;
        public BattleSlot User { get { return user; } }

        public Run(BattleSlot user)
        {
            this.user = user;
        }
        
        public void Dispatch(IMessageReceiver<Run> receiver)
        {
            receiver.Receive(this);
        }

        public void Dispatch(IMessageReceiver<IBattleMessage> receiver)
        {
            // Should never be run
            throw new NotImplementedException();
        }
    }
}
