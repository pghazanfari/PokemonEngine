using System;
using System.Collections.Generic;

namespace PokemonEngine.Model.Battle.Actions
{
    public class Comparer : IComparer<IAction>
    {
        private readonly Random random;

        public Comparer(Random random) {
            this.random = random;
        }

        public int Compare(IAction x, IAction y)
        {
            int priorityComparison = x.Priority.CompareTo(y.Priority);
            if (priorityComparison != 0) { return priorityComparison; }

            if (x is UseMove && y is UseMove)
            {
                UseMove moveA = x as UseMove;
                UseMove moveB = y as UseMove;

                return moveA.Slot.Pokemon.Stats[Statistic.Speed].CompareTo(moveB.Slot.Pokemon.Stats[Statistic.Speed]);
            }

            return random.Next(3) - 1; // -1, 0, 1
        }
    }
}
