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
        public readonly Model.IMove Move;
        public readonly Slot User;
        public readonly IReadOnlyCollection<Slot> Targets;

        private readonly float criticalModifier;
        private readonly float randomModifier;

        public InflictMoveDamage(Random random, Model.IMove move, Slot user, IReadOnlyCollection<Slot> targets)
        {
            Move = move;
            User = user;
            Targets = targets;

            criticalModifier = random.Next(2) + 1.0f;
            randomModifier = 1.0f - (random.Next(16) / 100.0f);
        }

        public InflictMoveDamage(Model.IMove move, Slot user, IReadOnlyCollection<Slot> targets) : this(new Random(), move, user, targets) { }

        public float CriticalModifier
        {
            get
            {
                return criticalModifier;
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
                return Targets.Count > 0 ? 0.75f : 1.0f;
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
                    return ((float)User.Pokemon.Stats[Statistic.Attack]) / ((float)target.Pokemon.Stats[Statistic.Defense]);
                case DamageType.Special:
                    return ((float)User.Pokemon.Stats[Statistic.SpecialAttack]) / ((float)target.Pokemon.Stats[Statistic.SpecialDefense]);
                default:
                    throw new InvalidOperationException("MoveDamageEffect was used on a move without Physical or Special DamageType");
            }
        }

        public float LevelInfluence
        {
            get
            {
                return (2.0f * User.Pokemon.Level / 5.0f) + 2;
            }
        }

        public float Damage(Slot target)
        {
            // https://bulbapedia.bulbagarden.net/wiki/Damage#Damage_calculation
            return ((LevelInfluence * Move.Power.Value * AttackDefenseRatio(target) / 50.0f) + 2) * Modifier(target);
        }

        public void Apply()
        {
            foreach (Slot target in Targets)
            {
                float dmg = Damage(target);

                target.Pokemon.UpdateHP(-(int)dmg);
            }
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
