using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messages;
using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Battle
{
    public class Move : IMove
    {
        private readonly Unique.IMove Base;

        #region Base Move Wrapper Methods
        public string Name { get { return Base.Name; } }
        public PokemonType Type { get { return Base.Type; } }
        public int? Power { get { return Base.Power; } }
        public DamageType? DamageType { get { return Base.DamageType; } }
        public MoveTarget Target { get { return Base.Target; } }
        public int BasePP { get { return Base.BasePP; } }
        public int MaxPPLimit { get { return Base.MaxPPLimit; } }
        public void Use(IBattle battle, UseMove useMoveAction) { Base.Use(battle, useMoveAction); }

        public int PP { get { return Base.PP; } }
        public int MaxPP { get { return Base.MaxPP; } }
        #endregion

        public bool IsDisabled { get; private set; }

        public Move(Unique.IMove baseMove, bool isDisabled)
        {
            this.Base = baseMove;
            IsDisabled = isDisabled;
        }

        public Move(Unique.IMove baseMove) : this(baseMove, false) { }
    }
}
