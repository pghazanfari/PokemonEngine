using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class IVSet : IStatsProvider
    {
        public const int MaxIV = 31;
        public const int MinIV = 0;

        private readonly IReadOnlyDictionary<PStat, int> ivs;
        public int this[PStat stat] { get { return ivs[stat];  } }

        public IVSet(IDictionary<PStat, int> ivs)
        {
            foreach (PStat stat in Enum.GetValues(typeof(PStat)))
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
            this.ivs = new ReadOnlyDictionary<PStat, int>(ivs);
        }
    }
}
