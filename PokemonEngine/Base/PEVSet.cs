using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class PEVSet : IPStatsProvider
    {
        public const int MaxEV = 255;
        public const int MinEV = 0;
        public const int MaxTotalEV = 510;

        public const int MaxBattlePointsPerPokemon = 3;
        public const int MinBattlePointsPerPokemon = 1;

        private readonly IDictionary<PStat, int> evs;
        public int this[PStat stat] { get { return evs[stat]; } }

        public int Total { get; private set; }

        public PEVSet(IDictionary<PStat, int> evs)
        {
            int Total = 0;
            foreach (PStat stat in Enum.GetValues(typeof(PStat)))
            {
                if (!evs.ContainsKey(stat))
                {
                    throw new Exception($"EVs are missing stat {stat.ToString()}");
                }
                if (evs[stat] < MinEV || evs[stat] > MaxEV)
                {
                    throw new Exception($"{stat.ToString()} must be >= ${MinEV} and <= ${MaxEV}");
                }
                Total += evs[stat];
            }
            if (Total > MaxTotalEV)
            {
                throw new Exception($"Sum of all EVs must be less than {MaxTotalEV}: {Total}");
            }
            this.evs = new Dictionary<PStat, int>(evs);
        }

        public void Add(PStat stat, int battlePoints)
        {
            if (battlePoints < MinBattlePointsPerPokemon || battlePoints > MaxBattlePointsPerPokemon)
            {
                throw new Exception($"EVs can only be increased by a value >= {MinBattlePointsPerPokemon} and <= {MaxBattlePointsPerPokemon}");
            }
            if (Total + battlePoints > MaxTotalEV)
            {
                throw new Exception($"Max EV for any stat is {MaxTotalEV}");
            }

            evs[stat] += battlePoints;
            Total += battlePoints;
        }
    }
}
