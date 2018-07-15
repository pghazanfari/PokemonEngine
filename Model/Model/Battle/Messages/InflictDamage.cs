using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public abstract class InflictDamage : IMessage
    {
        //TODO: Find a better name for this class
        // WithSource -> InflictDamange.WithSource ???
        public abstract class Typed<T> : InflictDamage
        {
            public abstract T Source { get; }
        }

        public readonly ModifierSet Modifiers = new ModifierSet();

        public abstract IEnumerable<Slot> Targets { get; }
        public abstract int this[Slot slot] { get; }

        public void Apply()
        {
            foreach (Slot slot in Targets)
            {
                slot.Pokemon.UpdateHP(-(int)Modifiers.Calculate(this[slot]));
            }
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }

        private class SimpleInflictDamage<T> : Typed<T>
        {
            private readonly T source;
            public override T Source
            {
                get
                {
                    return source;
                }
            }

            private readonly int damage;
            public override int this[Slot slot]
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            private readonly IEnumerable<Slot> targets;
            public override IEnumerable<Slot> Targets
            {
                get
                {
                    return targets;
                }
            }

            public SimpleInflictDamage(T source, int damage, IEnumerable<Slot> targets)
            {
                this.source = source;
                this.damage = damage;
                this.targets = new List<Slot>(targets).AsReadOnly();
            }
        }

        public static Typed<T> Create<T>(T source, int damage, IEnumerable<Slot> targets)
        {
            return new SimpleInflictDamage<T>(source, damage, targets);
        }

        public static Typed<T> Create<T>(T source, int damage, params Slot[] targets)
        {
            return new SimpleInflictDamage<T>(source, damage, targets);
        }
    }
}
