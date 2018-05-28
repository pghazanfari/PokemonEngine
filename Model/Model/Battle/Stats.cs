using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;

namespace PokemonEngine.Model.Battle
{
    public class Stats
    {
        public const int MinStage = -6;
        public const int MaxStage = 6;

        private readonly IDictionary<Statistic, int> stages;
        public int this[Statistic stat] { get { return stages[stat]; } }

        public Stats()
        {
            stages = new Dictionary<Statistic, int>();
            foreach (Statistic stat in Enum.GetValues(typeof(Statistic)))
            {
                stages.Add(stat, 0);
            }
        }

        public int ShiftStage(Statistic stat, int levels)
        {
            stages[stat] = Math.Max(Math.Min(stages[stat] + levels, MaxStage), MinStage);
            return stages[stat];
        }
    }
}
