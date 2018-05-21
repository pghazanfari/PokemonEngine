using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Unique
{
    public class IVSet
    {
        public const int MaxIV = 31;
        public const int MinIV = 0;

        private readonly IReadOnlyDictionary<Stat, int> ivs;
        public int this[Stat stat] { get { return ivs[stat];  } }

        public IVSet(IDictionary<Stat, int> ivs)
        {
            foreach (Stat stat in Enum.GetValues(typeof(Stat)))
            {
                if (!ivs.ContainsKey(stat))
                {
                    throw new Exception($"IVs are missing stat {stat.ToString()}");
                }
                if (ivs[stat] < MinIV || ivs[stat] > MaxIV)
                {
                    throw new Exception($"{stat.ToString()} must be >= ${MinIV} and <= ${MaxIV}");
                }
            }
            this.ivs = new ReadOnlyDictionary<Stat, int>(ivs);
        }
    }
}
