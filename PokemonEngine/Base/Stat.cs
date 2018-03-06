using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class Stat
    {
        public static Stat HP = new Stat("HP");
        public static Stat Attack = new Stat("Attack");
        public static Stat Defense = new Stat("Defense");
        public static Stat SpecialAttack = new Stat("SpecialAttack");
        public static Stat SpecialDefense = new Stat("SpecialDefense");
        public static Stat Speed = new Stat("Speed");

        public readonly string Name;
        private Stat(string name)
        {
            Name = name;
        }
    }
}
