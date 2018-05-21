using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle
{
    public interface IBattle
    {
        IReadOnlyList<Team> Teams { get; }
        Queue MessageQueue { get; }
    }
    public static class IBattleImpl
    {
        public static bool IsComplete(this IBattle battle)
        {
            int numLosers = 0;
            foreach (Team team in battle.Teams)
            {
                if (team.HasLost()) { numLosers++; }
            }
            return numLosers == battle.Teams.Count - 1;
        }

        public static Team Winner(this IBattle battle)
        {
            if (!battle.IsComplete()) { throw new InvalidOperationException("Battle has not completed yet"); }

            foreach (Team team in battle.Teams)
            {
                if (!team.HasLost())
                {
                    return team;
                }
            }

            throw new InvalidOperationException("Battle has no winner");
        }
    }
}
