using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int this[Model.Statistic stat] { get { return (int)(Modifiers[stat].Calculate(stats[stat] * Multiplier(stat))); } }

        public readonly IReadOnlyDictionary<Statistic, ModifierSet> Modifiers;

        private readonly IDictionary<Statistic, int> stages;

        public Statistics(IStatistics stats)
        {
            this.stats = stats;
            stages = Statistic.All.ToDictionary(kvp => kvp, kvp => InitialStage);
            Modifiers = new ReadOnlyDictionary<Statistic, ModifierSet>(Statistic.All.ToDictionary(kvp => kvp, kvp => new ModifierSet()));
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
            return Statistics.Multiplier(stat, stages[stat]);
        }

        private static float AccuracyMultipler(int stage)
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

        private static float EvasionMultiplier(int stage)
        {
            return AccuracyMultipler(-stage);
        }

        public static float Multiplier(Statistic stat, int stage)
        {
            if (stat == Statistic.Accuracy) { return AccuracyMultipler(stage); }
            if (stat == Statistic.Evasiveness) { return EvasionMultiplier(stage); }

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
    }
}
