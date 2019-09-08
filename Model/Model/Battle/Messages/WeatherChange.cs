using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public class WeatherChange : IMessage
    {
        public Model.Weather Weather { get; }
        public int TurnCount { get; }

        public WeatherChange(Model.Weather weather, int turnCount)
        {
            Weather = weather;
            TurnCount = turnCount;
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }

        public void Dispose()
        {
            //Do Nothing
        }
    }
}
