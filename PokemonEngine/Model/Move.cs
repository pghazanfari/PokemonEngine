using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class Move : IMove
    {
        private readonly string name;
        public string Name { get { return name; }  }

        private readonly int? power;
        public int? Power { get { return power; }  }

        private readonly DamageType? damageType;
        public DamageType? DamageType { get { return damageType; } }

        private MoveTarget target;
        public MoveTarget Target { get { return target; } }

        private readonly int basePP;
        public int BasePP { get { return basePP; } }

        private readonly int maxPossiblePP;
        public int MaxPossiblePP { get { return maxPossiblePP; } }

        // TODO: Effects

        public Move(string name, int? power, DamageType? damageType, MoveTarget target, int basePP, int maxPossiblePP)
        {
            this.name = name;
            this.power = power;
            this.damageType = damageType;
            this.target = target;
            this.basePP = basePP;
            this.maxPossiblePP = maxPossiblePP;
        }
    }
}
