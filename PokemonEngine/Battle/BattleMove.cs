using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;

namespace PokemonEngine.Battle
{
    public class BattleMove : IBattleMove
    {
        private readonly IUniqueMove Base;

        #region Base Move Wrapper Methods
        public string Name { get { return Base.Name; } }
        public int? Power { get { return Base.Power; } }
        public DamageType? DamageType { get { return Base.DamageType; } }
        public MoveTarget Target { get { return Base.Target; } }
        public int BasePP { get { return Base.BasePP; } }
        public int MaxPossiblePP { get { return Base.MaxPossiblePP; } }

        public int PP { get { return Base.PP; } }
        public int MaxPP { get { return Base.MaxPP; } }
        #endregion

        public bool IsDisabled { get; private set; }

        public BattleMove(IUniqueMove baseMove, bool isDisabled)
        {
            this.Base = baseMove;
            IsDisabled = isDisabled;
        }

        public BattleMove(IUniqueMove baseMove) : this(baseMove, false) { }
    }
}
