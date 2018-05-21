using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class BaseStats
    {
        public const int Max = 255;
        public const int Min = 0;

        private readonly IReadOnlyDictionary<Stat, int> stats;
        public int this[Stat stat] { get { return stats[stat]; } }

        public BaseStats(IDictionary<Stat, int> stats)
        {
            foreach (Stat stat in Enum.GetValues(typeof(Stat)))
            {
                if (!stats.ContainsKey(stat))
                {
                    throw new Exception($"Base stats are missing stat {stat.ToString()}");
                }
                if(stats[stat] < Min || stats[stat] > Max)
                {
                    throw new Exception($"{stat.ToString()} must be >= {Min} and <= {Max}");
                }
            }

            this.stats = new ReadOnlyDictionary<Stat, int>(stats);
        }
    }
}
