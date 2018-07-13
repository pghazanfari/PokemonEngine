using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class UseMove : IAction
    {
        private readonly int randomNumber;

        public ModifierSet AccuracyModifiers = new ModifierSet();

        public readonly IMove Move;
        public readonly IReadOnlyList<Slot> Targets;

        public IEnumerable<Slot> HitTargets
        {
            get
            {
                foreach (Slot slot in Targets)
                {
                    if (Hit(slot)) yield return slot;
                }
            }
        }

        public UseMove(Random random, Slot slot, IMove move, IList<Slot> targets) : base(slot)
        {
            Move = move;
            Targets = new List<Slot>(targets).AsReadOnly();
            randomNumber = random.Next(101);
        }

        public int Accuracy(Slot target)
        {
            if (!Targets.Contains(target)) throw new ArgumentException("Target was not a target of this move", "target");

            int effectiveStage = Math.Max(Math.Min(Slot.Pokemon.Stats.Stage(Statistic.Accuracy) - target.Pokemon.Stats.Stage(Statistic.Evasiveness), Statistics.MaxStage), Statistics.MinStage);
            return (int)AccuracyModifiers.Calculate(Move.Accuracy * Statistics.Multiplier(Statistic.Accuracy, effectiveStage));
        }
        
        public bool Missed(Slot target)
        {
            // Maybe just return true here??
            if (!Targets.Contains(target)) throw new ArgumentException("Target was not a target of this move", "target");

            return randomNumber > Accuracy(target);
        }

        public bool Hit(Slot target)
        {
            return !Missed(target);
        }

        public UseMove(Slot slot, IMove move, IList<Slot> targets) : this(new Random(), slot, move, targets) { }

        public override int Priority
        {
            get
            {
                return Move.Priority;
            }
        }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
