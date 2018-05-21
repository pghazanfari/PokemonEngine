using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle
{
    public class Orchestrator : ISubscriber
    {
        public readonly IBattle Battle;
        private readonly IProvider<IList<Request>, IList<IAction>> battleActionProvider;

        private readonly IList<Request> battleActionRequests;

        public event SenderOnlyEventHandler<Orchestrator> OnTurnStart;
        public event SenderOnlyEventHandler<Orchestrator> OnTurnEnd;
        public event SenderOnlyEventHandler<Orchestrator> OnMessageBroadcast;

        public event EventHandler<Orchestrator, IList<Request>> OnRequestInput;
        public event EventHandler<Orchestrator, IList<IAction>> OnInputReceived;

        // TODO: Change the second parameter for the post-message events.
        //       The same type is used as a placeholder for now.

        public event EventHandler<Orchestrator, SwapPokemon> OnSwapPokemon;
        public event EventHandler<Orchestrator, SwapPokemon> OnPokemonSwapped;

        public event EventHandler<Orchestrator, Effect> OnEffectExecution;
        public event EventHandler<Orchestrator, Effect> OnEffectExecuted;

        public event EventHandler<Orchestrator, UseItem> OnUseItem;
        public event EventHandler<Orchestrator, UseItem> OnItemUsed;

        public event EventHandler<Orchestrator, UseMove> OnUseMove;
        public event EventHandler<Orchestrator, UseMove> OnMoveUsed;

        public event EventHandler<Orchestrator, Run> OnRun;
        public event EventHandler<Orchestrator, Run> OnRan; //TODO: Change this name 

        public Orchestrator(IBattle battle, IProvider<IList<Request>, IList<IAction>> battleActionProvider)
        {
            this.Battle = battle;
            this.battleActionProvider = battleActionProvider;
            battleActionRequests = new List<Request>();

            battle.MessageQueue.AddSubscriber(this);
        }

        private void broadcast()
        {
            OnMessageBroadcast?.Invoke(this);
            Battle.MessageQueue.Broadcast();
        }

        private void flush()
        {
            while (Battle.MessageQueue.HasNext)
            {
                broadcast();
            }
        }

        public void ExecuteTurn()
        {
            OnTurnStart?.Invoke(this);

            /* First we enqueue BattleActionRequests for all battle slots that are still in play. The 
             * purpose of this is to allow move effects to trigger based on the enqueue'ing of the 
             * BattleActionRequests. This allows effects from moves like Taunt modify which battlers
             * we make BattleActionRequests for.
            */
            foreach (Team team in Battle.Teams)
            {
                foreach (Slot slot in team.Slots)
                {
                    if (slot.IsInPlay)
                    {
                        Battle.MessageQueue.Enqueue(new Request(team, slot.Index));
                    }
                }
            }
            battleActionRequests.Clear();
            flush();

            OnRequestInput?.Invoke(this, battleActionRequests);
            List<IAction> actions = new List<IAction>(battleActionProvider.Provide(battleActionRequests));
            OnInputReceived?.Invoke(this, actions);

            actions.Sort(); // Maybe make it so that the comparator is changeable? That would allow for Trick Room to function easier.
            
            foreach (IAction action in actions)
            {
                Battle.MessageQueue.Enqueue(action);
            }
            flush();

            OnTurnEnd?.Invoke(this);
        }

        public void Receive(Request request)
        {
            Battle.MessageQueue.Enqueue(request);
        }

        public void Receive(SwapPokemon swapPokemonAction)
        {

            OnSwapPokemon?.Invoke(this, swapPokemonAction);
            // TODO
            OnPokemonSwapped?.Invoke(this, swapPokemonAction);
        }

        public void Receive(Effect effect)
        {
            OnEffectExecution?.Invoke(this, effect);

            effect.Execute();

            OnEffectExecuted?.Invoke(this, effect);
        }

        public void Receive(UseItem useItemAction)
        {
            OnUseItem?.Invoke(this, useItemAction);

            // TODO

            OnItemUsed?.Invoke(this, useItemAction);
        }

        public void Receive(UseMove useMoveAction)
        {
            OnUseMove?.Invoke(this, useMoveAction);

            // TODO

            OnMoveUsed?.Invoke(this, useMoveAction);
        }

        public void Receive(Run runAction)
        {
            OnRun?.Invoke(this, runAction);

            // TODO

            OnRan?.Invoke(this, runAction);
        }
    }
}
