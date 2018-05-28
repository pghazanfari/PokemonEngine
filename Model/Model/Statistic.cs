using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class Statistic
    {
        public const int Min = 0;
        public const int Max = 255;

        public static readonly Statistic HP = new Statistic("HP");
        public static readonly Statistic Attack = new Statistic("Attack");
        public static readonly Statistic Defense = new Statistic("Defense");
        public static readonly Statistic SpecialAttack = new Statistic("SpecialAttack");
        public static readonly Statistic SpecialDefense = new Statistic("SpecialDefense");
        public static readonly Statistic Speed = new Statistic("Speed");

        public static readonly IReadOnlyList<Statistic> All = new List<Statistic>(new Statistic[]
        {
            HP,
            Attack,
            Defense,
            SpecialAttack,
            SpecialDefense,
            Speed
        }).AsReadOnly();

        public readonly string Name;
        private Statistic(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
