using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base.Events
{
    public class ValueChangeEventArgs : EventArgs
    {
        public readonly int CurrentValue;
        public readonly int Delta;
        public readonly int NewValue;

        public ValueChangeEventArgs(int currentValue, int delta) : base()
        {
            CurrentValue = currentValue;
            Delta = delta;
            NewValue = CurrentValue + Delta;
        }
    }
}
