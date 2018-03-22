using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public enum DamageType { Physical, Special } // Maybe none?
    public interface IMove
    {
        string Name { get; }
        int? Power { get; }
        DamageType? DamageType { get; }
        MoveTarget Target { get;  }
        int BasePP { get; }
        int MaxPossiblePP { get; }
    }
}
