using System;
using System.Collections.Generic;

namespace PokemonEngine.Model.Battle
{
    public class Statistic
    {
        public static readonly Statistic Attack = new Statistic("Attack");
        public static readonly Statistic Defense = new Statistic("Defense");
        public static readonly Statistic SpecialAttack = new Statistic("SpecialAttack");
        public static readonly Statistic SpecialDefense = new Statistic("SpecialDefense");
        public static readonly Statistic Speed = new Statistic("Speed");
        public static readonly Statistic Evasiveness = new Statistic("Evasiveness");
        public static readonly Statistic Accuracy = new Statistic("Accuracy");

        public static readonly IReadOnlyList<Statistic> All = new List<Statistic>(new Statistic[]
        {
            Attack,
            Defense,
            SpecialAttack,
            SpecialDefense,
            Speed,
            Evasiveness,
            Accuracy
        }).AsReadOnly();

        public string Name { get; }
        private Statistic(string name)
        {
            Name = name;
        }

        public static implicit operator Statistic(Model.Statistic stat)
        {
            if (stat == Model.Statistic.Attack) return Attack;
            if (stat == Model.Statistic.Defense) return Defense;
            if (stat == Model.Statistic.SpecialAttack) return SpecialAttack;
            if (stat == Model.Statistic.SpecialDefense) return SpecialDefense;
            if (stat == Model.Statistic.Speed) return Speed;

            throw new Exception($"Can't convert normal Statistic {stat.ToString()} to a battle Statistic");
        }

        public static implicit operator Model.Statistic(Statistic stat)
        {
            if (stat == Attack) return Model.Statistic.Attack; 
            if (stat == Defense) return Model.Statistic.Defense; 
            if (stat == SpecialAttack) return Model.Statistic.SpecialAttack; 
            if (stat == SpecialDefense) return Model.Statistic.SpecialDefense; 
            if (stat == Speed) return Model.Statistic.Speed;

            throw new Exception($"Can't convert battle Statistic {stat.ToString()} to a normal Statistic");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
