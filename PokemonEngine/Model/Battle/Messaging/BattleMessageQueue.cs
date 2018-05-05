using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Messaging
{
    public class BattleMessageQueue : MessageQueue<IBattleMessage>
    {
        public BattleMessageQueue() : base() { }

        public override bool Enqueue(IBattleMessage obj)
        {
            return base.Enqueue(obj);
        }

        public override IBattleMessage Broadcast()
        {
            return base.Broadcast();
        }

        public override bool InjectAfter(IBattleMessage message, IBattleMessage newMessage)
        {
            return base.InjectAfter(message, newMessage);
        }

        public override bool InjectBefore(IBattleMessage message, IBattleMessage newMessage)
        {
            return base.InjectBefore(message, newMessage);
        }

        public override bool Replace(IBattleMessage message, IBattleMessage newMessage)
        {
            return base.Replace(message, newMessage);
        }

        public override bool Remove(IBattleMessage message)
        {
            return base.Remove(message);
        }
    }
}
