using System;

namespace PokemonEngine.Model.Unique
{
    public class Statistics : IStatistics
    {
        private readonly IPokemon pokemon;

        public int this[Statistic stat] { get { return CalculateStat(stat); } }

        public Statistics(IPokemon pokemon)
        {
            this.pokemon = pokemon;
        }

        private int CalculateStat(Statistic stat)
        {
            int baseStat = (pokemon as Model.IPokemon).Stats[stat];
            int baseVal = (int)Math.Floor((2.0 * baseStat + pokemon.IVs[stat] + Math.Floor(pokemon.EVs[stat] / 4.0)) * pokemon.Level / 100.0);

            if (stat == Statistic.HP)
            {
                return baseVal + pokemon.Level + 10;
            }

            return (int)Math.Floor(Math.Floor(baseVal + 5.0) * pokemon.Nature.Multiplier(stat));
        }
    }
}
