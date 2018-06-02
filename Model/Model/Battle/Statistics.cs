using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class Statistics : Model.IStatistics
    {
        public const int MinStage = -6;
        public const int InitialStage = 0;
        public const int MaxStage = 6;

        private readonly IStatistics stats;
        public int this[Model.Statistic stat] { get { return (int)(stats[stat] * Multiplier(stat)); } }

        private readonly IDictionary<Statistic, int> stages;

        public Statistics(IStatistics stats)
        {
            this.stats = stats;
            stages = Statistic.All.ToDictionary(kvp => kvp, kvp => InitialStage);
        }

        public void ShiftStage(Statistic stat, int delta)
        {
            stages[stat] = Math.Max(Math.Min(stages[stat] + delta, MaxStage), MinStage);
        }

        public int Stage(Statistic stat)
        {
            return stages[stat];
        }

        public float Multiplier(Statistic stat)
        {
            if (stat == Statistic.Accuracy) { return accuracyMultipler(stages[stat]); }
            if (stat == Statistic.Evasiveness) { return evasionMultiplier(stages[stat]); }
            return multiplier(stages[stat]);
        }

        private float multiplier(int stage)
        {
            switch (stage)
            {
                case -6: return 0.25f;
                case -5: return 0.285f;
                case -4: return 0.33f;
                case -3: return 0.4f;
                case -2: return 0.5f;
                case -1: return 0.66f;
                case 0: return 1.0f;
                case 1: return 1.5f;
                case 2: return 2.0f;
                case 3: return 2.5f;
                case 4: return 3.0f;
                case 5: return 3.5f;
                case 6: return 4.0f;
                default:
                    throw new ArgumentException($"Stage must be between {MinStage} and {MaxStage} inclusive");
            }
        }

        private float accuracyMultipler(int stage)
        {
            switch (stage)
            {
                case -6: return 0.33f;
                case -5: return 0.375f;
                case -4: return 0.428f;
                case -3: return 0.5f;
                case -2: return 0.6f;
                case -1: return 0.75f;
                case 0: return 1.0f;
                case 1: return 1.33f;
                case 2: return 1.66f;
                case 3: return 2.0f;
                case 4: return 2.33f;
                case 5: return 2.66f;
                case 6: return 3.0f;
                default:
                    throw new ArgumentException($"Stage must be between {MinStage} and {MaxStage} inclusive");
            }
        }

        private float evasionMultiplier(int stage)
        {
            return accuracyMultipler(-stage);
        }
    }
}
