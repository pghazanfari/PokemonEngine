using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle.Weathers
{
    class ExtremelyHarshSunlight : Weather
    {
        public ExtremelyHarshSunlight() : base() { }
        public ExtremelyHarshSunlight(int turnCount) : base(turnCount) { }

        public override void OnMessageBroadcast(object sender, EventArgs args)
        {
            IMessage message = args.Battle.MessageQueue.First;
            if (message is UseMove action)
            {
                if (action.Move.Type == PokemonType.Water)
                {
                    MoveUseFailure newMessage = new MoveUseFailure(action.Move);
                    args.Battle.MessageQueue.Replace(message, newMessage);
                }
            }
        }
    }
}
