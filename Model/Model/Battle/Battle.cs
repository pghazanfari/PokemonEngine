using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;
using System.Collections.ObjectModel;
using System.Collections;

namespace PokemonEngine.Model.Battle
{
    public class Battle : IBattle
    {
        private readonly Random rng;
        public Random RNG { get { return rng; } }

        private readonly IReadOnlyList<Team> teams;
        public IReadOnlyList<Team> Teams { get { return teams; } }

        private readonly Messaging.Queue messageQueue;
        public Messaging.Queue MessageQueue { get { return messageQueue; } }

        private readonly IList<Effect> effects;
        private readonly IReadOnlyList<Effect> roEffects;
        public IReadOnlyList<Effect> Effects
        {
            get
            {
                return roEffects;
            }
        }

        public event EventHandler<EventArgs> OnTurnStart;
        public event EventHandler<EventArgs> OnTurnEnd;
        public event EventHandler<EventArgs> OnMessageBroadcast;

        public event EventHandler<RequestInputEventArgs> OnRequestInput;
        public event EventHandler<InputReceivedEventArgs> OnInputReceived;

        public event EventHandler<SwapPokemonEventArgs> OnSwapPokemon;
        public event EventHandler<PokemonSwappedEventArgs> OnPokemonSwapped;

        public event EventHandler<UseItemEventArgs> OnUseItem;
        public event EventHandler<ItemUsedEventArgs> OnItemUsed;

        public event EventHandler<UseMoveEventArgs> OnUseMove;
        public event EventHandler<MoveUsedEventArgs> OnMoveUsed;

        public event EventHandler<UseRunEventArgs> OnUseRun;
        public event EventHandler<RunUsedEventArgs> OnRunUsed;

        public event EventHandler<InflictMoveDamageEventArgs> OnInflictMoveDamage;
        public event EventHandler<MoveDamageInflictedEventArgs> OnMoveDamageInflicted;

        public IBattleInputProvider InputProvider { get; set; }

        private readonly IList<Request> actionRequests;

        public Battle(Random rng, IBattleInputProvider inputProvider, IEnumerable<Team> teams)
        {
            if (teams == null) { throw new ArgumentNullException("teams"); }
            if (teams.ContainsNull()) { throw new ArgumentException("A BattleTeam in a Battle cannot be null");  }
            if (teams.ContainsDuplicates()) { throw new ArgumentException("A battle cannot contain duplicate teams"); }
            if (teams.AnyOverlaps()) { throw new ArgumentException("2 or more teams contain overlapping trainers"); }
            if (teams.Count() < 2) { throw new ArgumentException("There must be at least 2 teams in a battle"); }

            //Going to attempt to not enforce this
            //if (teams.Any(x => x.SlotCount != teams[0].SlotCount)) throw new ArgumentException("All teams must have the same number of slots for the battle");

            this.teams = new List<Team>(teams).AsReadOnly();
            InputProvider = inputProvider;

            this.rng = rng;

            effects = new List<Effect>();
            roEffects = (effects as List<Effect>).AsReadOnly();
            
            actionRequests = new List<Request>();

            messageQueue = new Messaging.Queue();
            messageQueue.AddSubscriber(this);
        }

        public Battle(IBattleInputProvider inputProvider, IEnumerable<Team> teams) : this(new Random(), inputProvider, teams) { }
        public Battle(IBattleInputProvider inputProvider, params Team[] teams) : this(inputProvider, teams as IEnumerable<Team>) { }

        public IEnumerator<Team> GetEnumerator()
        {
            return teams.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return teams.GetEnumerator();
        }

        private void flush()
        {
            while (messageQueue.HasNext)
            {
                OnMessageBroadcast?.Invoke(this, new EventArgs(this));
                messageQueue.Broadcast();
            }
        }

        public bool RegisterEffect(Effect effect)
        {
            if (effects.Contains(effect)) { return false; }
            effects.Add(effect);

            OnTurnStart += effect.OnTurnStart;
            OnTurnEnd += effect.OnTurnEnd;
            OnMessageBroadcast += effect.OnMessageBroadcast;
            OnRequestInput += effect.OnRequestInput;
            OnInputReceived += effect.OnInputReceived;
            OnSwapPokemon += effect.OnSwapPokemon;
            OnPokemonSwapped += effect.OnPokemonSwapped;
            OnUseItem += effect.OnUseItem;
            OnItemUsed += effect.OnItemUsed;
            OnUseMove += effect.OnUseMove;
            OnMoveUsed += effect.OnMoveUsed;
            OnUseRun += effect.OnUseRun;
            OnRunUsed += effect.OnRunUsed;
            OnInflictMoveDamage += effect.OnInflictMoveDamage;
            OnMoveDamageInflicted += effect.OnMoveDamageInflicted;

            return true;
        }

        public bool DeregisterEffect(Effect effect)
        {
            OnTurnStart -= effect.OnTurnStart;
            OnTurnEnd -= effect.OnTurnEnd;
            OnMessageBroadcast -= effect.OnMessageBroadcast;
            OnRequestInput -= effect.OnRequestInput;
            OnInputReceived -= effect.OnInputReceived;
            OnSwapPokemon -= effect.OnSwapPokemon;
            OnPokemonSwapped -= effect.OnPokemonSwapped;
            OnUseItem -= effect.OnUseItem;
            OnItemUsed -= effect.OnItemUsed;
            OnUseMove -= effect.OnUseMove;
            OnMoveUsed -= effect.OnMoveUsed;
            OnUseRun -= effect.OnUseRun;
            OnRunUsed -= effect.OnRunUsed;
            OnInflictMoveDamage -= effect.OnInflictMoveDamage;
            OnMoveDamageInflicted -= effect.OnMoveDamageInflicted;

            return effects.Remove(effect);
        }

        public void ExecuteTurn()
        {
            OnTurnStart?.Invoke(this, new EventArgs(this));

            /* First we enqueue BattleActionRequests for all battle slots that are still in play. The 
             * purpose of this is to allow move effects to trigger based on the enqueue'ing of the 
             * BattleActionRequests. This allows effects from moves like Taunt modify which battlers
             * we make BattleActionRequests for.
            */
            foreach (Team team in Teams)
            {
                foreach (Slot slot in team)
                {
                    if (slot.IsInPlay)
                    {
                        messageQueue.Enqueue(new Request(slot));
                    }
                }
            }
            actionRequests.Clear();
            flush();

            OnRequestInput?.Invoke(this, new RequestInputEventArgs(this, actionRequests));

            List<IAction> actions = new List<IAction>(InputProvider.ProvideActions(this, actionRequests));

            OnInputReceived?.Invoke(this, new InputReceivedEventArgs(this, actionRequests, actions));

            //actions.Sort(); // Maybe make it so that the comparator is changeable? That would allow for Trick Room to function easier.

            foreach (IAction action in actions)
            {
                messageQueue.Enqueue(action);
            }

            while (true)
            {
                flush();

                List<Request> requests = new List<Request>();
                foreach (Team team in Teams)
                {
                    foreach (Slot slot in team)
                    {
                        if (slot.Pokemon.HasFainted() && !slot.Participant.HasLost())
                        {
                            requests.Add(new Request(slot));
                        }
                    }
                }
                if (requests.Count == 0) { break; }
                IList<SwapPokemon> swapPokemonActions = InputProvider.ProvideSwapPokemon(this, requests);
                foreach (SwapPokemon swapPokemonAction in swapPokemonActions) { messageQueue.Enqueue(swapPokemonAction); }
            }


            OnTurnEnd?.Invoke(this, new EventArgs(this));
        }

        public void Receive(Request request)
        {
            actionRequests.Add(request);
        }

        public void Receive(SwapPokemon swapPokemonAction)
        {

            OnSwapPokemon?.Invoke(this, new SwapPokemonEventArgs(this, swapPokemonAction));

            // TODO
            OnPokemonSwapped?.Invoke(this, new PokemonSwappedEventArgs(this, swapPokemonAction));
        }

        public void Receive(UseItem useItemAction)
        {
            OnUseItem?.Invoke(this, new UseItemEventArgs(this, useItemAction));

            // TODO

            OnItemUsed?.Invoke(this, new ItemUsedEventArgs(this, useItemAction));
        }

        public void Receive(UseMove useMoveAction)
        {
            if (useMoveAction.Slot.Pokemon.HasFainted()) { return; }

            OnUseMove?.Invoke(this, new UseMoveEventArgs(this, useMoveAction));

            useMoveAction.Move.Use(this, useMoveAction);

            OnMoveUsed?.Invoke(this, new MoveUsedEventArgs(this, useMoveAction));
        }

        public void Receive(UseRun runAction)
        {
            OnUseRun?.Invoke(this, new UseRunEventArgs(this, runAction));

            // TODO

            OnRunUsed?.Invoke(this, new RunUsedEventArgs(this, runAction));
        }

        public void Receive(InflictMoveDamage inflictMoveDamage)
        {

            OnInflictMoveDamage?.Invoke(this, new InflictMoveDamageEventArgs(this, inflictMoveDamage));

            inflictMoveDamage.Apply();

            OnMoveDamageInflicted?.Invoke(this, new MoveDamageInflictedEventArgs(this, inflictMoveDamage));
        }
    }
}