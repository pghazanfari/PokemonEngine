using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PokemonEngine.Model.Battle
{
    public class Stat
    {
        public static readonly Stat Attack = new Stat("Attack");
        public static readonly Stat Defense = new Stat("Defense");
        public static readonly Stat SpecialAttack = new Stat("SpecialAttack");
        public static readonly Stat SpecialDefense = new Stat("SpecialDefense");
        public static readonly Stat Speed = new Stat("Speed");
        public static readonly Stat Evasiveness = new Stat("Evasiveness");
        public static readonly Stat Accuracy = new Stat("Accuracy");

        public readonly String Name;
        private Stat(string name)
        {
            Name = name;
        }

        public static implicit operator Stat(Model.Stat stat)
        {
            if (stat == Model.Stat.Attack) return Attack;
            if (stat == Model.Stat.Defense) return Defense;
            if (stat == Model.Stat.SpecialAttack) return SpecialAttack;
            if (stat == Model.Stat.SpecialDefense) return SpecialDefense;
            if (stat == Model.Stat.Speed) return Speed;

            throw new Exception($"Can't convert Stat {stat.ToString()} to a BattleStat");
        }

        public static implicit operator Model.Stat(Stat stat)
        {
            if (stat == Attack) return Model.Stat.Attack; 
            if (stat == Defense) return Model.Stat.Defense; 
            if (stat == SpecialAttack) return Model.Stat.SpecialAttack; 
            if (stat == SpecialDefense) return Model.Stat.SpecialDefense; 
            if (stat == Speed) return Model.Stat.Speed;

            throw new Exception($"Can't convert BattleStat {stat.ToString()} to a Stat");
        }
    }
}
