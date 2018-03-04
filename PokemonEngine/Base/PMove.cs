using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class PMove
    {
        public enum DamageType { Physical, Special }

        public String Name { get; private set;  }
        public int? Power { get; private set;  }
        public DamageType? Damage { get; private set; }

        // TODO: Effects

        public PMove(string name, int power, DamageType damage)
        {
            Name = name;
            Power = power;
            Damage = damage;
        }

        public PMove(string name)
        {
            Name = name;
            Power = null;
            Damage = null; // Maybe DamageType.NONE
        }
    }
}
