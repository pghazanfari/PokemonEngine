using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Unique
{
    public class IVSet : ICloneable
    {
        public const int MaxIV = 31;
        public const int MinIV = 0;

        private readonly IReadOnlyDictionary<Statistic, int> ivs;
        public int this[Statistic stat] { get { return ivs[stat];  } }

        public IVSet(IDictionary<Statistic, int> ivs)
        {
            foreach (Statistic stat in Statistic.All)
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
            this.ivs = new ReadOnlyDictionary<Statistic, int>(ivs);
        }

        public IVSet(int iv) : this(Enumerable.ToDictionary(Statistic.All, x => x, x => iv)) { }
        public IVSet() : this(0) { }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public IVSet Clone()
        {
            return new IVSet(this.ivs.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }
    }
}
