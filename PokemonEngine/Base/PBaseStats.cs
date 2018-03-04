using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class PBaseStats : IPStatsProvider
    {
        public const int Max = 255;
        public const int Min = 0;

        private readonly IReadOnlyDictionary<PStat, int> stats;
        public int this[PStat stat] { get { return stats[stat]; } }

        public PBaseStats(IDictionary<PStat, int> stats)
        {
            foreach (PStat stat in Enum.GetValues(typeof(PStat)))
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

            this.stats = new ReadOnlyDictionary<PStat, int>(stats);
        }
    }
}
