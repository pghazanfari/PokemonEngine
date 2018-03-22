using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PokemonEngine.Model.Battle
{
    public class BattleStat
    {
        public static readonly BattleStat Attack = new BattleStat("Attack");
        public static readonly BattleStat Defense = new BattleStat("Defense");
        public static readonly BattleStat SpecialAttack = new BattleStat("SpecialAttack");
        public static readonly BattleStat SpecialDefense = new BattleStat("SpecialDefense");
        public static readonly BattleStat Speed = new BattleStat("Speed");
        public static readonly BattleStat Evasiveness = new BattleStat("Evasiveness");
        public static readonly BattleStat Accuracy = new BattleStat("Accuracy");

        public readonly String Name;
        private BattleStat(string name)
        {
            Name = name;
        }

        public static implicit operator BattleStat(Stat stat)
        {
            if (stat == Stat.Attack) return Attack;
            if (stat == Stat.Defense) return Defense;
            if (stat == Stat.SpecialAttack) return SpecialAttack;
            if (stat == Stat.SpecialDefense) return SpecialDefense;
            if (stat == Stat.Speed) return Speed;

            throw new Exception($"Can't convert Stat {stat.ToString()} to a BattleStat");
        }

        public static implicit operator Stat(BattleStat stat)
        {
            if (stat == Attack) return Stat.Attack; 
            if (stat == Defense) return Stat.Defense; 
            if (stat == SpecialAttack) return Stat.SpecialAttack; 
            if (stat == SpecialDefense) return Stat.SpecialDefense; 
            if (stat == Speed) return Stat.Speed;

            throw new Exception($"Can't convert BattleStat {stat.ToString()} to a Stat");
        }
    }
}
