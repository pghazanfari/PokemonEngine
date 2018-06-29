using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public class InflictMoveDamage : InflictDamage
    {
        public const float MultiTargetModifier = 0.75f;
        public const float SingleTargetModifier = 1.0f;
        public const float CriticalHitAmplification = 1.5f;

        private readonly IReadOnlyCollection<Slot> targets;
        public override IReadOnlyCollection<Slot> Targets { get { return targets; } }
        public override int this[Slot key]
        {
            get
            {
                return CalculateDamage(key);
            }
        }

        public readonly IBattle Battle;
        public readonly Model.IMove Move;
        public readonly Slot User;

        private readonly bool isCriticalHit;
        private readonly float randomModifier;

        public InflictMoveDamage(IBattle battle, Model.IMove move, Slot user, IEnumerable<Slot> targets)
        {
            Battle = battle;
            Move = move;
            User = user;
            this.targets = new List<Slot>(targets).AsReadOnly();
            randomModifier = 1.0f - (battle.RNG.Next(16) / 100.0f);
            isCriticalHit = battle.RNG.NextDouble() < CriticalHitProbability(move.CriticalHitStage);
        }
        public InflictMoveDamage(IBattle battle, Model.IMove move, Slot user, params Slot[] targets) : this(battle, move, user, targets as IEnumerable<Slot>) { }

        public bool IsCriticalHit { get { return isCriticalHit; } }
        public float CriticalModifier
        {
            get
            {
                return IsCriticalHit ? CriticalHitAmplification : 1.0f;
            }
        }

        public float RandomModifier
        {
            get
            {
                return randomModifier;
            }
        }

        public float WeatherModifier
        {
            get
            {
                switch (Battle.CurrentWeather.Type)
                {
                    case Model.Weather.HarshSunlight:
                        {
                            if (Move.Type == PokemonType.Fire) return 1.5f;
                            if (Move.Type == PokemonType.Water) return 0.5f;
                            return 1.0f;
                        }
                    case Model.Weather.ExtremelyHarshSunlight:
                        {
                            if (Move.Type == PokemonType.Fire) return 1.5f; // This might be wrong
                            if (Move.Type == PokemonType.Water) return 0.0f;
                            return 1.0f;
                        }
                    case Model.Weather.Rain:
                        {
                            if (Move.Type == PokemonType.Water) return 1.5f;
                            if (Move.Type == PokemonType.Fire) return 0.5f;
                            return 1.0f;
                        }
                    case Model.Weather.HeavyRain:
                        {
                            if (Move.Type == PokemonType.Water) return 1.5f;
                            if (Move.Type == PokemonType.Fire) return 0.0f; // This might be wrong
                            return 1.0f;
                        }
                    default: return 1.0f;
                }
            }
        }

        public float TargetsModifier
        {
            get
            {
                return Targets.Count() > 1 ? MultiTargetModifier : SingleTargetModifier;
            }
        }

        public float STABModifier
        {
            get
            {
                return User.Pokemon.Types.Contains(Move.Type) ? 1.5f : 1.0f;
            }
        }

        public float TypeModifier(Slot target)
        {
            float sum = 1.0f;
            foreach (PokemonType type in target.Pokemon.Types) {
                sum *= Move.Type.EffectivenessAgainst(type);
            }
            return sum;
        }

        public float OtherModifier(Slot target)
        {
            return 1.0f;
        }

        public float Modifier(Slot target)
        {
            return TargetsModifier * WeatherModifier * CriticalModifier * RandomModifier * STABModifier * TypeModifier(target) * OtherModifier(target);
        }

        public float AttackDefenseRatio(Slot target)
        {
            switch (Move.DamageType)
            {
                case DamageType.Physical:
                    return ((float)User.Pokemon.Stats[Statistic.Attack]) / target.Pokemon.Stats[Statistic.Defense];
                case DamageType.Special:
                    return ((float)User.Pokemon.Stats[Statistic.SpecialAttack]) / target.Pokemon.Stats[Statistic.SpecialDefense];
                default:
                    throw new InvalidOperationException("InflictMoveDamage was used on a move without Physical or Special DamageType");
            }
        }

        public float LevelInfluence
        {
            get
            {
                return ( (2.0f * User.Pokemon.Level) / 5.0f) + 2.0f;
            }
        }

        public int CalculateDamage(Slot target)
        {
            // https://bulbapedia.bulbagarden.net/wiki/Damage#Damage_calculation
            return (int)Math.Floor((Math.Floor(Math.Floor(Math.Floor(LevelInfluence) * Move.Power.Value * AttackDefenseRatio(target)) / 50.0) + 2.0) * Modifier(target));
        }

        public static float CriticalHitProbability(int stage)
        {
            switch (Math.Abs(stage))
            {
                case 0: return 1.0f / 24.0f;
                case 1: return 1.0f / 8.0f;
                case 2: return 1.0f / 2.0f;
                default:
                    return 1.0f;
            }
        }
    }
}
