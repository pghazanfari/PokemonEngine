using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PokemonEngine.Model.Events;
using PokemonEngine.Model.Common;

namespace PokemonEngine.Model.Battle
{
    public class Stats
    {
        public class StageShiftEventArgs : ValueChangeEventArgs
        {
            public readonly Stat Stat;
            public StageShiftEventArgs(Stat stat, int currentValue, int delta) : base(currentValue, delta)
            {
                Stat = stat;
            }
        }

        public const int MinStage = -6;
        public const int MaxStage = 6;

        private readonly IDictionary<Stat, int> stages;
        public int this[Stat stat] { get { return stages[stat]; } }

        public event EventHandler<Stats, StageShiftEventArgs> OnStageShift;
        public event EventHandler<Stats, StageShiftEventArgs> OnStageShifted;

        public Stats()
        {
            stages = new Dictionary<Stat, int>();
            foreach (Stat stat in Enum.GetValues(typeof(Stat)))
            {
                stages.Add(stat, 0);
            }
        }

        public int ChangeStage(Stat stat, int delta)
        {
            StageShiftEventArgs args = new StageShiftEventArgs(stat, this[stat], delta);

            OnStageShift?.Invoke(this, args);
            stages[stat] = Math.Max(Math.Min(stages[stat] + delta, MaxStage), MinStage);
            OnStageShifted?.Invoke(this, args);
            return stages[stat];
        }
    }
}
