using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PokemonEngine.Model.Util;
using System.Collections.ObjectModel;

namespace PokemonEngine.Model.Battle
{
    public class Battle : IBattle
    {
        private readonly IReadOnlyList<BattleTeam> teams;
        public IReadOnlyList<BattleTeam> Teams { get { return teams; } }

        private readonly Dictionary<IBattleParticipant, Queue<IBattleAction>> queuedActions;
        private readonly IReadOnlyDictionary<IBattleParticipant, Queue<IBattleAction>> roQueuedActions;
        public IReadOnlyDictionary<IBattleParticipant, Queue<IBattleAction>> QueuedActions { get { return roQueuedActions; } }

        public Battle(IList<BattleTeam> teams)
        {
            if (teams == null) { throw new ArgumentNullException("teams"); }
            if (teams.ContainsNull()) { throw new ArgumentException("A BattleTeam in a Battle cannot be null");  }
            if (teams.ContainsDuplicates()) { throw new ArgumentException("A battle cannot contain duplicate teams"); }
            if (teams.AnyOverlaps()) { throw new ArgumentException("2 or more teams contain overlapping trainers"); }
            
            //Going to attempt to not enforce this
            //if (teams.Any(x => x.SlotCount != teams[0].SlotCount)) throw new ArgumentException("All teams must have the same number of slots for the battle");

            this.teams = new List<BattleTeam>(teams).AsReadOnly();

            queuedActions = new Dictionary<IBattleParticipant, Queue<IBattleAction>>();
            foreach (BattleTeam team in teams)
            {
                foreach (IBattleParticipant participant in team.Participants)
                {
                    queuedActions[participant] = new Queue<IBattleAction>();
                }
            }
            roQueuedActions = queuedActions.AsReadOnly();
        }

        public void EnqueueAction(IBattleParticipant trainer, IBattleAction action)
        {
            //TODO
        }
    }
}
