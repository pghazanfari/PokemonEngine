using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class Nature
    {
        public const double IncreaseMultiplier = 1.1;
        public const double DecreaseMultiplier = 0.9;

        public static readonly Nature Hardy = new Nature("Hardy", null, null);
        public static readonly Nature Lonely = new Nature("Lonely", Stat.Attack, Stat.Defense);
        public static readonly Nature Brave = new Nature("Brave", Stat.Attack, Stat.Speed);
        public static readonly Nature Adamant = new Nature("Adamant", Stat.Attack, Stat.SpecialAttack);
        public static readonly Nature Naughty = new Nature("Naughty", Stat.Attack, Stat.SpecialDefense);
        public static readonly Nature Bold = new Nature("Bold", Stat.Defense, Stat.Attack);
        public static readonly Nature Docile = new Nature("Docile", null, null);
        public static readonly Nature Relaxed = new Nature("Relaxed", Stat.Defense, Stat.Speed);
        public static readonly Nature Impish = new Nature("Impish", Stat.Defense, Stat.SpecialAttack);
        public static readonly Nature Lax = new Nature("Lax", Stat.Defense, Stat.SpecialDefense);
        public static readonly Nature Timid = new Nature("Timid", Stat.Speed, Stat.Attack);
        public static readonly Nature Hasty = new Nature("Hasty", Stat.Speed, Stat.Defense);
        public static readonly Nature Serious = new Nature("Serious", null, null);
        public static readonly Nature Jolly = new Nature("Jolly", Stat.Speed, Stat.SpecialAttack);
        public static readonly Nature Naive = new Nature("Naive", Stat.Speed, Stat.SpecialDefense);
        public static readonly Nature Modest = new Nature("Modest", Stat.SpecialAttack, Stat.Attack);
        public static readonly Nature Mild = new Nature("Mild", Stat.SpecialAttack, Stat.Defense);
        public static readonly Nature Quiet = new Nature("Quiet", Stat.SpecialAttack, Stat.Speed);
        public static readonly Nature Bashful = new Nature("Bashful", null, null);
        public static readonly Nature Rash = new Nature("Rash", Stat.SpecialAttack, Stat.SpecialDefense);
        public static readonly Nature Calm = new Nature("Calm", Stat.SpecialDefense, Stat.Attack);
        public static readonly Nature Gentle = new Nature("Gentle", Stat.SpecialDefense, Stat.Defense);
        public static readonly Nature Sassy = new Nature("Sassy", Stat.SpecialDefense, Stat.Speed);
        public static readonly Nature Careful = new Nature("Careful", Stat.SpecialDefense, Stat.SpecialAttack);
        public static readonly Nature Quirky = new Nature("Quirky", null, null);

        public readonly string Name;
        public readonly Stat IncreasedStat;
        public readonly Stat DecreasedStat;

        private Nature(string name, Stat increasedStat, Stat decreasedStat)
        {
            Name = name;
            IncreasedStat = increasedStat;
            DecreasedStat = decreasedStat;
        }

        public double Multiplier(Stat stat)
        {
            if (stat == IncreasedStat) { return Nature.IncreaseMultiplier; }
            if (stat == DecreasedStat) { return Nature.DecreaseMultiplier; }

            return 1.0;
        }
    }
}
