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
        public int? Power { get { return Base.Power.HasValue ? (int)PowerModifiers.Calculate(Base.Power.Value) : Base.Power; } }
        public DamageType? DamageType { get { return Base.DamageType; } }
        public int Accuracy { get { return (int)AccuracyModifiers.Calculate(Base.Accuracy); } }
        public MoveTarget Target { get { return Base.Target; } }
        public int BasePP { get { return Base.BasePP; } }
        public int MaxPPLimit { get { return Base.MaxPPLimit; } }
        public int Priority { get { return (int)PriorityModifiers.Calculate(Base.Priority); } }
        public int CriticalHitStage {  get { return (int)CriticalHitStageModifiers.Calculate(Base.CriticalHitStage); } }
        public void Use(IBattle battle, UseMove useMoveAction) { Base.Use(battle, useMoveAction); }

        public int PP { get { return Base.PP; } }
        public int MaxPP { get { return Base.MaxPP; } }
        #endregion

        public bool IsDisabled { get; private set; }

        public readonly ModifierSet PowerModifiers = new ModifierSet();
        public readonly ModifierSet AccuracyModifiers = new ModifierSet();
        public readonly ModifierSet PriorityModifiers = new ModifierSet();
        public readonly ModifierSet CriticalHitStageModifiers = new ModifierSet();
        

        public Move(Unique.IMove baseMove, bool isDisabled)
        {
            this.Base = baseMove;
            IsDisabled = isDisabled;
        }

        public Move(Unique.IMove baseMove) : this(baseMove, false) { }

        object ICloneable.Clone()
        {
            return Clone();
        }

        Unique.IMove Unique.IMove.Clone()
        {
            return Clone();
        }

        IMove IMove.Clone()
        {
            return Clone();
        }

        public Move Clone()
        {
            return new Move(Base.Clone(), IsDisabled);
        }
    }
}
