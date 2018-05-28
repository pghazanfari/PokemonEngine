using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Unique
{
    public class StatisticSet : IStatisticSet
    {
        public readonly IPokemon Pokemon;
        public int this[Statistic stat] { get { return calculateStat(stat); } }

        public StatisticSet(IPokemon pokemon)
        {
            Pokemon = pokemon;
        }

        private int calculateStat(Statistic stat)
        {
            int baseVal = (int)Math.Floor((2.0 * Pokemon.BaseStats[stat] + Pokemon.IVs[stat] + Math.Floor(Pokemon.EVs[stat] / 4.0)) * Pokemon.Level / 100.0);

            if (stat == Statistic.HP)
            {
                return (int)(baseVal + Pokemon.Level + 10);
            }

            return (int)Math.Floor(Math.Floor(baseVal + 5.0) * Pokemon.Nature.Multiplier(stat));
        }
    }
}
