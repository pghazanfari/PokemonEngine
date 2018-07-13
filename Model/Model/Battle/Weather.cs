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
                case Model.Weather.ExtremelyHarshSunlight: return new ExtremelyHarshSunlight(turnCount);
                case Model.Weather.Fog: return new Fog(turnCount);
                case Model.Weather.Hail: return new Hail(turnCount);
                case Model.Weather.HarshSunlight: return new HarshSunlight(turnCount);
                case Model.Weather.HeavyRain: return new HeavyRain(turnCount);
                case Model.Weather.MysteriousAirCurrent: return new MysteriousAirCurrent(turnCount);
                case Model.Weather.Rain: return new Rain(turnCount);
                case Model.Weather.Sandstorm: return new Sandstorm(turnCount);
                default: throw new ArgumentException($"Unsupported weather type {weather.ToString()}", "weather");
            }
        }

        public static implicit operator Weather(Model.Weather weather)
        {
            switch (weather)
            {
                case Model.Weather.ClearSkies: return new ClearSkies();
                case Model.Weather.ExtremelyHarshSunlight: return new ExtremelyHarshSunlight();
                case Model.Weather.Fog: return new Fog();
                case Model.Weather.Hail: return new Hail();
                case Model.Weather.HarshSunlight: return new HarshSunlight();
                case Model.Weather.HeavyRain: return new HeavyRain();
                case Model.Weather.MysteriousAirCurrent: return new MysteriousAirCurrent();
                case Model.Weather.Rain: return new Rain();
                case Model.Weather.Sandstorm: return new Sandstorm();
                default: throw new ArgumentException($"Unsupported weather type {weather.ToString()}", "weather");
            }
        }

        public static implicit operator Model.Weather(Weather weather)
        {
            if (weather is ClearSkies) return Model.Weather.ClearSkies;
            if (weather is ExtremelyHarshSunlight) return Model.Weather.ExtremelyHarshSunlight;
            if (weather is Fog) return Model.Weather.Fog;
            if (weather is Hail) return Model.Weather.Hail;
            if (weather is HarshSunlight) return Model.Weather.HarshSunlight;
            if (weather is HeavyRain) return Model.Weather.HeavyRain;
            if (weather is MysteriousAirCurrent) return Model.Weather.MysteriousAirCurrent;
            if (weather is Rain) return Model.Weather.Rain;
            if (weather is Sandstorm) return Model.Weather.Sandstorm;

            throw new ArgumentException($"Unsupported weather type {weather.GetType().ToString()}", "weather");
        }
    }
}
