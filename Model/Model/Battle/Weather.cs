using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Weathers;

namespace PokemonEngine.Model.Battle
{
    public abstract class Weather : Effect
    {
        public Model.Weather Type { get { return this; } }

        public int TurnsRemaining { get; private set; }

        public bool IsComplete { get { return TurnsRemaining == 0; } }

        public Weather() : this(-1)
        { }

        public Weather(int turnCount)
        {
            TurnsRemaining = turnCount;
        }

        public void DecrementTurnCounter()
        {
            if (TurnsRemaining == 0) return;
            TurnsRemaining--;
        }

        public static Weather Create(Model.Weather weather, int turnCount)
        {
            switch (weather)
            {
                case Model.Weather.ClearSkies: return new ClearSkies(turnCount);
                default: throw new ArgumentException($"Unsupported weather type {weather.ToString()}", "weather");
            }
        }

        public static implicit operator Weather(Model.Weather weather)
        {
            switch (weather)
            {
                case Model.Weather.ClearSkies: return new ClearSkies();
                default: throw new ArgumentException($"Unsupported weather type {weather.ToString()}", "weather");
            }
        }

        public static implicit operator Model.Weather(Weather weather)
        {
            if (weather is ClearSkies) return Model.Weather.ClearSkies;

            throw new ArgumentException($"Unsupported weather type {weather.GetType().ToString()}", "weather");
        }
    }
}
