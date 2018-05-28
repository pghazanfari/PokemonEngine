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
        public static readonly Nature Lonely = new Nature("Lonely", Statistic.Attack, Statistic.Defense);
        public static readonly Nature Brave = new Nature("Brave", Statistic.Attack, Statistic.Speed);
        public static readonly Nature Adamant = new Nature("Adamant", Statistic.Attack, Statistic.SpecialAttack);
        public static readonly Nature Naughty = new Nature("Naughty", Statistic.Attack, Statistic.SpecialDefense);
        public static readonly Nature Bold = new Nature("Bold", Statistic.Defense, Statistic.Attack);
        public static readonly Nature Docile = new Nature("Docile", null, null);
        public static readonly Nature Relaxed = new Nature("Relaxed", Statistic.Defense, Statistic.Speed);
        public static readonly Nature Impish = new Nature("Impish", Statistic.Defense, Statistic.SpecialAttack);
        public static readonly Nature Lax = new Nature("Lax", Statistic.Defense, Statistic.SpecialDefense);
        public static readonly Nature Timid = new Nature("Timid", Statistic.Speed, Statistic.Attack);
        public static readonly Nature Hasty = new Nature("Hasty", Statistic.Speed, Statistic.Defense);
        public static readonly Nature Serious = new Nature("Serious", null, null);
        public static readonly Nature Jolly = new Nature("Jolly", Statistic.Speed, Statistic.SpecialAttack);
        public static readonly Nature Naive = new Nature("Naive", Statistic.Speed, Statistic.SpecialDefense);
        public static readonly Nature Modest = new Nature("Modest", Statistic.SpecialAttack, Statistic.Attack);
        public static readonly Nature Mild = new Nature("Mild", Statistic.SpecialAttack, Statistic.Defense);
        public static readonly Nature Quiet = new Nature("Quiet", Statistic.SpecialAttack, Statistic.Speed);
        public static readonly Nature Bashful = new Nature("Bashful", null, null);
        public static readonly Nature Rash = new Nature("Rash", Statistic.SpecialAttack, Statistic.SpecialDefense);
        public static readonly Nature Calm = new Nature("Calm", Statistic.SpecialDefense, Statistic.Attack);
        public static readonly Nature Gentle = new Nature("Gentle", Statistic.SpecialDefense, Statistic.Defense);
        public static readonly Nature Sassy = new Nature("Sassy", Statistic.SpecialDefense, Statistic.Speed);
        public static readonly Nature Careful = new Nature("Careful", Statistic.SpecialDefense, Statistic.SpecialAttack);
        public static readonly Nature Quirky = new Nature("Quirky", null, null);

        public readonly string Name;
        public readonly Statistic IncreasedStat;
        public readonly Statistic DecreasedStat;

        private Nature(string name, Statistic increasedStat, Statistic decreasedStat)
        {
            Name = name;
            IncreasedStat = increasedStat;
            DecreasedStat = decreasedStat;
        }

        public double Multiplier(Statistic stat)
        {
            if (stat == IncreasedStat) { return Nature.IncreaseMultiplier; }
            if (stat == DecreasedStat) { return Nature.DecreaseMultiplier; }

            return 1.0;
        }
    }
}
