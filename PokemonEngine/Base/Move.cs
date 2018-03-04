using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class Move
    {
        public enum DamageType { Physical, Special }

        public String Name { get; private set;  }
        public int? Power { get; private set;  }
        public DamageType? Damage { get; private set; }

        // TODO: Effects

        public Move(string name, int power, DamageType damage)
        {
            Name = name;
            Power = power;
            Damage = damage;
        }

        public Move(string name)
        {
            Name = name;
            Power = null;
            Damage = null; // Maybe DamageType.NONE
        }
    }
}
