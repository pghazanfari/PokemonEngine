﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle;
using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Unique
{
    public class Move : IMove
    {
        public readonly Model.IMove Base;

        #region Base Move Wrapper Methods
        public string Name { get { return Base.Name; } }
        public PokemonType Type {  get { return Base.Type; } }
        public int? Power { get { return Base.Power; } }
        public DamageType? DamageType { get { return Base.DamageType; } }
        public MoveTarget Target { get { return Base.Target; } }
        public int BasePP { get { return Base.BasePP; } }
        public int MaxPPLimit { get { return Base.MaxPPLimit; } }
        public void Use(IBattle battle, UseMove useMoveAction) { Base.Use(battle, useMoveAction); }
        #endregion

        public int PP { get; private set; }
        public int MaxPP { get; private set; }

        public Move(Model.IMove baseMove, int pp, int maxPP)
        {
            if (pp < 0) { throw new Exception($"PP {pp} must be greater than 0"); }
            if (maxPP < 1) { throw new Exception($"Max PP {maxPP} must be greater than 1"); }
            if (pp > maxPP) { throw new Exception($"PP {pp} must be less than or equal to max pp {maxPP}");  }
            if (maxPP > baseMove.MaxPPLimit) { throw new Exception($"Max PP {maxPP} must be less than or equal to max possible pp {baseMove.MaxPPLimit}"); }

            this.Base = baseMove;
            PP = pp;
            MaxPP = maxPP;
        }

        public Move(Model.IMove baseMove) : this(baseMove, baseMove.BasePP, baseMove.BasePP) { }
    }
}