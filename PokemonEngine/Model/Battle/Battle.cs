using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Battle
{
    public class Battle : IBattle
    {
        private readonly IReadOnlyList<Team> teams;
        public IReadOnlyList<Team> Teams { get { return teams; } }

        private readonly Queue messageQueue;
        public Queue MessageQueue { get { return messageQueue; } }

        public Battle(IList<Team> teams, IProvider<IAction, Request> provider)
        {
            if (teams == null) { throw new ArgumentNullException("teams"); }
            if (teams.ContainsNull()) { throw new ArgumentException("A BattleTeam in a Battle cannot be null");  }
            if (teams.ContainsDuplicates()) { throw new ArgumentException("A battle cannot contain duplicate teams"); }
            if (teams.AnyOverlaps()) { throw new ArgumentException("2 or more teams contain overlapping trainers"); }
            if (teams.Count < 2) { throw new ArgumentException("There must be at least 2 teams in a battle"); }

            //Going to attempt to not enforce this
            //if (teams.Any(x => x.SlotCount != teams[0].SlotCount)) throw new ArgumentException("All teams must have the same number of slots for the battle");

            this.teams = new List<Team>(teams).AsReadOnly();

            messageQueue = new Queue();
        }
    }
}

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Requests;

namespace PokemonEngine.Model.Battle
{
    public class Battle : IBattle
    {
        //TODO: Change these to better names
        private enum BattlePhase { TURN_START, REQUEST_ACTIONS, PERFORM_ACTIONS, TURN_END}

        private readonly IReadOnlyList<BattleTeam> teams;
        public IReadOnlyList<BattleTeam> Teams { get { return teams; } }

        private BattlePhase phase;
        private readonly BattleMessageQueue queue;
        private readonly IList<BattleActionRequest> actionRequests;
        private readonly IProvider<BattleActionRequest, IBattleAction> provider;

        public bool IsTurnComplete { get { return phase == BattlePhase.TURN_END; } }

        public Battle(IList<BattleTeam> teams, IProvider<IBattleAction, BattleActionRequest> provider)
        {
            if (provider == null) { throw new ArgumentNullException("provider");  }
            this.provider = provider;

            if (teams == null) { throw new ArgumentNullException("teams"); }
            if (teams.ContainsNull()) { throw new ArgumentException("A BattleTeam in a Battle cannot be null");  }
            if (teams.ContainsDuplicates()) { throw new ArgumentException("A battle cannot contain duplicate teams"); }
            if (teams.AnyOverlaps()) { throw new ArgumentException("2 or more teams contain overlapping trainers"); }
            if (teams.Count < 2) { throw new ArgumentException("There must be at least 2 teams in a battle"); }

            //Going to attempt to not enforce this
            //if (teams.Any(x => x.SlotCount != teams[0].SlotCount)) throw new ArgumentException("All teams must have the same number of slots for the battle");

            this.teams = new List<BattleTeam>(teams).AsReadOnly();

            queue = new BattleMessageQueue();
            queue.AddSubscriber(this);

            actionRequests = new List<BattleActionRequest>();
            phase = BattlePhase.TURN_START;
        }

        public void ResetTurn()
        {
            phase = BattlePhase.TURN_START;
            actionRequests.Clear();
        }

        private void turnStart()
        {
            foreach (BattleTeam team in teams)
            {
                foreach (BattleSlot slot in team)
                {
                    if (slot.IsInPlay)
                    {
                        BattleActionRequest request = new BattleActionRequest(team, slot.SlotNumber);
                        queue.Enqueue(request);
                    }
                }
            }

            phase = BattlePhase.REQUEST_ACTIONS;
        }

        private void requestActions()
        {
            queue.Flush();

            List<IBattleAction> list = new List<IBattleAction>(actionRequests.Count);
            foreach (BattleActionRequest request in actionRequests)
            {
                list.Add(provider.Provide(request));
            }
            list.Sort();
            foreach (IBattleAction action in list)
            {
                queue.Enqueue(action);
            }

            phase = BattlePhase.PERFORM_ACTIONS;
        }

        private void performActions()
        {
            queue.Flush();
            phase = BattlePhase.TURN_END;
        }

        public bool ProgressTurn()
        {
            if (IsTurnComplete) { return true; }

            switch(phase)
            {
                case BattlePhase.TURN_START:
                    turnStart();
                    break;
                case BattlePhase.REQUEST_ACTIONS:
                    requestActions();
                    break;
                case BattlePhase.PERFORM_ACTIONS:
                    performActions();
                    break;
                case BattlePhase.TURN_END:
                    break;
            }

            return false;
        }

        public void Receive(Run run)
        {
            if (phase == BattlePhase.PERFORM_ACTIONS)
            {

            }

            throw new NotImplementedException();
        }

        public void Receive(UseMove useMove)
        {
            if (phase == BattlePhase.PERFORM_ACTIONS)
            {

            }
            throw new NotImplementedException();
        }

        public void Receive(UseItem useItemAction)
        {
            if (phase == BattlePhase.PERFORM_ACTIONS)
            {

            }
            throw new NotImplementedException();
        }

        public void Receive(SwapPokemon swapPokemonAction)
        {
            switch (phase)
            {
                case BattlePhase.TURN_START:
                    break;
                case BattlePhase.PERFORM_ACTIONS:
                    break;
            }
            throw new NotImplementedException();
        }

        public void Receive(BattleActionRequest request)
        {
            if (phase == BattlePhase.REQUEST_ACTIONS)
            {
                actionRequests.Add(request);
            }
            throw new NotImplementedException();
        }
    }
}
 
*/
