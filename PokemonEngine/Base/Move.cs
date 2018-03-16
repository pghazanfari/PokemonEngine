using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class Move
    {
        public enum DamageType { Physical, Special } // Maybe none?

        private readonly string name;
        public string Name { get { return name; }  }

        private readonly int? power;
        public int? Power { get { return power; }  }

        private readonly DamageType? damage;
        public DamageType? Damage { get { return damage; } }

        private MoveTarget target;
        public MoveTarget Target { get { return target; } }

        // TODO: Effects

        public Move(string name, int? power, DamageType? damage, MoveTarget target)
        {
            this.name = name;
            this.power = power;
            this.damage = damage;
            this.target = target;
        }
    }
}
