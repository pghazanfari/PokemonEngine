﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle;

namespace PokemonEngine.Model
{
    public enum DamageType { Physical, Special } // Maybe none?
    public interface IMove
    {
        string Name { get; }
        PokemonType Type { get; }
        int? Power { get; }
        DamageType? DamageType { get; }
        MoveTarget Target { get;  }
        int BasePP { get; }
        int MaxPossiblePP { get; }
    }
}
