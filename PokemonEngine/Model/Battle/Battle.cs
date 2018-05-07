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
        private enum BattlePhase { TURN_START, REQUEST_ACTIONS, PERFORM_ACTIONS, TURN_END}

        private readonly IReadOnlyList<BattleTeam> teams;
        public IReadOnlyList<BattleTeam> Teams { get { return teams; } }

        private BattlePhase phase;

        public bool IsTurnComplete { get { return phase == BattlePhase.TURN_END; } }

        private BattleMessageQueue queue;
        private IList<BattleActionRequest> requests;

        public Battle(IList<BattleTeam> teams)
        {
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

            requests = new List<BattleActionRequest>();
            phase = BattlePhase.TURN_START;
        }

        public void ResetTurn()
        {
            phase = BattlePhase.TURN_START;
            requests.Clear();
        }

        private void turnStart()
        {
            //TODO: 
            //  Generate BattleActionRequest for each BattleSlot BattleParticipant still in the battle.
            //  Put BattleActionRequests into the BattleMessageQueue in the order of Slot #

            phase = BattlePhase.REQUEST_ACTIONS;
        }

        private void requestActions()
        {
            // TODO:
            //  Pull from Queue until it is empty and collct all BattleActionRequests
            //  Execute every BattleActionRequest to get the BattleAction.
            //  Sort every BattleAction then add to the Queue

            phase = BattlePhase.PERFORM_ACTIONS;
        }

        private void performActions()
        {
            //TODO:
            // Pull and execute each BattleAction from the Queue

            phase = BattlePhase.TURN_END;
        }

        public bool Progress()
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
                requests.Add(request);
            }
            throw new NotImplementedException();
        }
    }
}
