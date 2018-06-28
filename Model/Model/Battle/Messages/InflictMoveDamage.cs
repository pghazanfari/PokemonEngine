using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public class InflictMoveDamage : IMessage
    {
        public const float MultiTargetModifier = 0.75f;
        public const float SingleTargetModifier = 1.0f;

        public const float CriticalHitAmplification = 1.5f;

        public readonly Model.IMove Move;
        public readonly Slot User;
        public readonly IReadOnlyCollection<Slot> Targets;

        private readonly bool criticalHit;
        private readonly float randomModifier;

        public InflictMoveDamage(Random random, Model.IMove move, Slot user, IReadOnlyCollection<Slot> targets)
        {
            Move = move;
            User = user;
            Targets = targets;

            criticalHit = random.NextDouble() < CriticalHitProbability(move.CriticalHitStage);
            randomModifier = 1.0f - (random.Next(16) / 100.0f);
        }

        public InflictMoveDamage(Model.IMove move, Slot user, IReadOnlyCollection<Slot> targets) : this(new Random(), move, user, targets) { }

        private float CriticalHitProbability(int stage)
        {
            switch(Math.Abs(stage))
            {
                case 0: return 1.0f / 24.0f;
                case 1: return 1.0f / 8.0f;
                case 2: return 1.0f / 2.0f;
                default:
                    return 1.0f;
            }
        }

        public bool IsCriticalHit { get { return criticalHit; } }
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
                return 1.0f; //TODO: Implement once weather works
            }
        }

        public float TargetsModifier
        {
            get
            {
                return Targets.Count > 1 ? MultiTargetModifier : SingleTargetModifier;
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

        public int Damage(Slot target)
        {
            // https://bulbapedia.bulbagarden.net/wiki/Damage#Damage_calculation
            return (int)Math.Floor((Math.Floor(Math.Floor(Math.Floor(LevelInfluence) * Move.Power.Value * AttackDefenseRatio(target)) / 50.0) + 2.0) * Modifier(target));
            //return (int)(((LevelInfluence * Move.Power.Value * AttackDefenseRatio(target) / 50) + 2) * Modifier(target));
        }

        public void Apply()
        {
            foreach (Slot target in Targets)
            {
                target.Pokemon.UpdateHP(-Damage(target));
            }
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
