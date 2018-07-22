using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle.Weathers
{
    class HeavyRain : Weather
    {
        public HeavyRain() : base() { }
        public HeavyRain(int turnCount) : base(turnCount) { }

        public override void OnMessageBroadcast(object sender, EventArgs args)
        {
            IMessage message = args.Battle.MessageQueue.First;
            if (message is UseMove)
            {
                UseMove action = (UseMove)message;
                if (action.Move.Type == PokemonType.Fire)
                {
                    MoveUseFailure newMessage = new MoveUseFailure(action.Move);
                    args.Battle.MessageQueue.Replace(message, newMessage);
                }
            }
        }
    }
}
