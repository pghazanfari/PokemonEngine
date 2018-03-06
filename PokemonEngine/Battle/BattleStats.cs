using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;

namespace PokemonEngine.Battle
{
    public class BattleStats
    {
        public const int MinStage = -6;
        public const int MaxStage = 6;

        private readonly IDictionary<BattleStat, int> stages;
        public int this[BattleStat stat] { get { return stages[stat]; } }

        public event EventHandler<BattleStatStageModifiedEventArgs> OnStatStageModification;
        public event EventHandler<BattleStatStageModifiedEventArgs> OnStatsStageModified;

        public BattleStats()
        {
            stages = new Dictionary<BattleStat, int>();
            foreach (BattleStat stat in Enum.GetValues(typeof(BattleStat)))
            {
                stages.Add(stat, 0);
            }
        }

        public int ModifyStage(BattleStat stat, int delta)
        {
            BattleStatStageModifiedEventArgs args = new BattleStatStageModifiedEventArgs(this, stat, this[stat], delta);

            OnStatStageModification?.Invoke(this, args);
            stages[stat] = Math.Max(Math.Min(stages[stat] + delta, MaxStage), MinStage);
            OnStatsStageModified?.Invoke(this, args);
            return stages[stat];
        }
    }
    public class BattleStatStageModifiedEventArgs : EventArgs
    {
        public readonly BattleStats Stats;
        public readonly BattleStat Stat;
        public readonly int Stage;
        public readonly int Delta;

        public BattleStatStageModifiedEventArgs(BattleStats stats, BattleStat stat, int stage, int delta) : base()
        {
            Stats = stats;
            Stat = stat;
            Stage = stage;
            Delta = delta;
        }
    }
}
