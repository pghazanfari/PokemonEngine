using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public class WeatherChange : IMessage
    {
        private readonly Model.Weather weather;
        public Model.Weather Weather { get { return weather; } }

        private readonly int turnCount;
        public int TurnCount { get { return turnCount; } }

        public WeatherChange(Model.Weather weather, int turnCount)
        {
            this.weather = weather;
            this.turnCount = turnCount;
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
