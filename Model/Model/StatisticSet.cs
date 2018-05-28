using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class StatisticSet : IStatisticSet
    {
        private readonly IReadOnlyDictionary<Statistic, int> statistics;
        public virtual int this[Statistic stat]
        {
            get
            {
                return statistics[stat];
            }
        }

        public StatisticSet(IDictionary<Statistic, int> statistics)
        {
            Dictionary<Statistic, int> dict = new Dictionary<Statistic, int>(Statistic.All.Count);
            foreach (Statistic stat in Statistic.All)
            {
                if (!statistics.ContainsKey(stat)) { throw new ArgumentException($"Dictionary is missing statistic {stat.ToString()}");  }
                int value = statistics[stat];
                if (value < Statistic.Min || value > Statistic.Max) { throw new ArgumentException($"Statistic {stat.ToString()} has a value that is not within the valid range {Statistic.Min} - {Statistic.Max}");  }
                dict[stat] = value;
            }
            this.statistics = new ReadOnlyDictionary<Statistic, int>(dict);
        }

        public StatisticSet(int hp, int attack, int defense, int specialAttack, int specialDefense, int speed) : this(paramsToDict(hp, attack, defense, specialAttack, specialDefense, speed)) { }

        private static IDictionary<Statistic, int> paramsToDict(int hp, int attack, int defense, int specialAttack, int specialDefense, int speed)
        {
            Dictionary<Statistic, int> dict = new Dictionary<Statistic, int>(6);
            dict[Statistic.HP] = hp;
            dict[Statistic.Attack] = attack;
            dict[Statistic.Defense] = defense;
            dict[Statistic.SpecialAttack] = specialAttack;
            dict[Statistic.SpecialDefense] = specialDefense;
            dict[Statistic.Speed] = speed;
            return dict;
        }
    }
}
