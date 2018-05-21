using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public interface IMove : Unique.IMove
    {
        bool IsDisabled { get; }

        void Use(IBattle battle, Slot user, IReadOnlyCollection<Slot> targets);
    }
}
