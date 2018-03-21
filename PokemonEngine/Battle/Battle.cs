using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;

namespace PokemonEngine.Battle
{
    public class Battle : IBattle
    {
        private readonly IReadOnlyList<BattleTeam> teams;
        public IReadOnlyList<BattleTeam> Teams { get { return teams; } }

        public Battle(IList<BattleTeam> teams)
        {
            // This can probably be optimized
            if (teams.Any(x => teams.Any(y => x != y && x.Overlaps(y))))
            {
                throw new Exception("2 or more teams contain overlapping trainers");
            }

            this.teams = new List<BattleTeam>(teams).AsReadOnly();
        }
    }
}
