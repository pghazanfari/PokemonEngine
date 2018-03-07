using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;
using PokemonEngine.Base.Events;

namespace PokemonEngine.Battle
{
    public class BattleStats
    {
        public class StageShiftEventArgs : ValueChangeEventArgs
        {
            public readonly BattleStat Stat;
            public StageShiftEventArgs(BattleStat stat, int currentValue, int delta) : base(currentValue, delta)
            {
                Stat = stat;
            }
        }

        public const int MinStage = -6;
        public const int MaxStage = 6;

        private readonly IDictionary<BattleStat, int> stages;
        public int this[BattleStat stat] { get { return stages[stat]; } }

        public event EventHandler<BattleStats, StageShiftEventArgs> OnStageShift;
        public event EventHandler<BattleStats, StageShiftEventArgs> OnStageShifted;

        public BattleStats()
        {
            stages = new Dictionary<BattleStat, int>();
            foreach (BattleStat stat in Enum.GetValues(typeof(BattleStat)))
            {
                stages.Add(stat, 0);
            }
        }

        public int ChangeStage(BattleStat stat, int delta)
        {
            StageShiftEventArgs args = new StageShiftEventArgs(stat, this[stat], delta);

            OnStageShift?.Invoke(this, args);
            stages[stat] = Math.Max(Math.Min(stages[stat] + delta, MaxStage), MinStage);
            OnStageShifted?.Invoke(this, args);
            return stages[stat];
        }
    }
}
