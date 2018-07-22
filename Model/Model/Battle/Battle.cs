using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;
using PokemonEngine.Model.Battle.Weathers;

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

        private readonly Weather surroundingWeather;
        public Weather SurroundingWeather { get { return surroundingWeather; } }

        // CurrentWeather will be invoked manually before events in order to ensure
        // that the CurrentWeather's effect always applies.
        public Weather CurrentWeather { get; private set; }

        public event EventHandler<EventArgs> OnBattleStart;
        public event EventHandler<BattleEndEventArgs> OnBattleEnd;

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

        public event EventHandler<InflictDamageEventArgs> OnInflictDamage;
        public event EventHandler<DamageInflictedEventArgs> OnDamageInflicted;

        public event EventHandler<MoveUseFailureEventArgs> OnMoveUseFailure;
        public event EventHandler<MoveUseFailedEventArgs> OnMoveUseFailed;

        public event EventHandler<ShiftStatStageEventArgs> OnShiftStatStage;
        public event EventHandler<StatStageShiftedEventArgs> OnStatStageShifted;

        public event EventHandler<ChangeWeatherEventArgs> OnChangeWeather;
        public event EventHandler<WeatherChangedEventArgs> OnWeatherChanged;
        public event EventHandler<WeatherCompletedEventArgs> OnWeatherCompleted;

        public event EventHandler<PerformMoveOperationEventArgs> OnPerformMoveOperation;
        public event EventHandler<MoveOperationPerformedEventArgs> OnMoveOperationPerformed;

        public event EventHandler<PerformEffectOperationEventArgs> OnPerformEffectOperation;
        public event EventHandler<EffectOperationPerformedEventArgs> OnEffectOperationPerformed;

        public IInputProvider InputProvider { get; set; }
        public IComparer<IAction> ActionComparer { get; set; }

        private readonly EventArgs BattleArgs;
        private readonly IList<Request> actionRequests;

        private enum State { START, IN_PROGRESS, END };
        private State currentState;

        public int TurnCounter { get; private set; }

        public Battle(Random rng, IInputProvider inputProvider, Model.Weather weather, IEnumerable<Team> teams)
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
            surroundingWeather = weather;
            CurrentWeather = surroundingWeather;

            effects = new List<Effect>();
            roEffects = (effects as List<Effect>).AsReadOnly();
            
            actionRequests = new List<Request>();

            messageQueue = new Messaging.Queue();
            messageQueue.AddSubscriber(this);

            ActionComparer = new Actions.Comparer(RNG);
            currentState = State.START;
            TurnCounter = 1;

            BattleArgs = new EventArgs(this);
        }

        public Battle(IInputProvider inputProvider, Model.Weather weather, IEnumerable<Team> teams) : this(new Random(), inputProvider, weather, teams) { }
        public Battle(IInputProvider inputProvider, Model.Weather weather, params Team[] teams) : this(inputProvider, weather, teams as IEnumerable<Team>) { }
        public Battle(Random rng, IInputProvider inputProvider, Model.Weather weather, params Team[] teams) : this(rng, inputProvider, weather, teams as IEnumerable<Team>) { }

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
            while (messageQueue.HasNext) broadcast();
        }

        private void broadcast()
        {
            EventArgs args = new EventArgs(this);
            CurrentWeather.OnMessageBroadcast(this, args);
            OnMessageBroadcast?.Invoke(this, args);

            messageQueue.Broadcast();
        }

        public bool RegisterEffect(Effect effect)
        {
            if (effects.Contains(effect)) { return false; }
            effects.Add(effect);

            OnBattleStart += effect.OnBattleStart;
            OnBattleEnd += effect.OnBattleEnd;
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
            OnInflictDamage += effect.OnInflictDamage;
            OnDamageInflicted += effect.OnDamageInflicted;
            OnMoveUseFailure += effect.OnMoveUseFailure;
            OnMoveUseFailed += effect.OnMoveUseFailed;
            OnShiftStatStage += effect.OnShiftStatStage;
            OnStatStageShifted += effect.OnStatStageShifted;
            OnChangeWeather += effect.OnChangeWeather;
            OnWeatherChanged += effect.OnWeatherChanged;
            OnWeatherCompleted += effect.OnWeatherCompleted;
            OnPerformMoveOperation += effect.OnPerformMoveOperation;
            OnMoveOperationPerformed += effect.OnMoveOperationPerformed;
            OnPerformEffectOperation += effect.OnPerformEffectOperation;
            OnEffectOperationPerformed += effect.OnEffectOperationPerformed;

            return true;
        }

        public bool DeregisterEffect(Effect effect)
        {
            OnBattleStart -= effect.OnBattleStart;
            OnBattleEnd -= effect.OnBattleEnd;
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
            OnInflictDamage -= effect.OnInflictDamage;
            OnDamageInflicted -= effect.OnDamageInflicted;
            OnMoveUseFailure -= effect.OnMoveUseFailure;
            OnMoveUseFailed -= effect.OnMoveUseFailed;
            OnShiftStatStage -= effect.OnShiftStatStage;
            OnStatStageShifted -= effect.OnStatStageShifted;
            OnChangeWeather -= effect.OnChangeWeather;
            OnWeatherChanged -= effect.OnWeatherChanged;
            OnWeatherCompleted -= effect.OnWeatherCompleted;
            OnPerformMoveOperation -= effect.OnPerformMoveOperation;
            OnMoveOperationPerformed -= effect.OnMoveOperationPerformed;
            OnPerformEffectOperation -= effect.OnPerformEffectOperation;
            OnEffectOperationPerformed -= effect.OnEffectOperationPerformed;

            return effects.Remove(effect);
        }

        public void ExecuteTurn()
        {
            

            if (currentState == State.END) { return; } // Don't do anything if battle is over

            if (currentState == State.START)
            {
                CurrentWeather.OnBattleStart(this, BattleArgs);
                OnBattleStart?.Invoke(this, BattleArgs);

                currentState = State.IN_PROGRESS;
            }

            CurrentWeather.OnTurnStart(this, BattleArgs);
            OnTurnStart?.Invoke(this, BattleArgs);

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

            RequestInputEventArgs requestInputEventArgs = new RequestInputEventArgs(this, actionRequests);
            CurrentWeather.OnRequestInput(this, requestInputEventArgs);
            OnRequestInput?.Invoke(this, requestInputEventArgs);

            List<IAction> actions = new List<IAction>(InputProvider.ProvideActions(this, actionRequests));

            InputReceivedEventArgs inputReceivedEventArgs = new InputReceivedEventArgs(this, actionRequests, actions);
            CurrentWeather.OnInputReceived(this, inputReceivedEventArgs);
            OnInputReceived?.Invoke(this, inputReceivedEventArgs);

            actions.Sort(ActionComparer);

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

            CurrentWeather.DecrementTurnCounter();
            if (CurrentWeather.IsComplete)
            {
                WeatherCompletedEventArgs weatherCompletedEventArgs = new WeatherCompletedEventArgs(this, CurrentWeather);
                CurrentWeather.OnWeatherCompleted(this, weatherCompletedEventArgs);
                OnWeatherCompleted?.Invoke(this, weatherCompletedEventArgs);

                MessageQueue.AddFirst(new WeatherChange(SurroundingWeather, -1));
                broadcast();
            }

            CurrentWeather.OnTurnEnd(this, BattleArgs);
            OnTurnEnd?.Invoke(this, BattleArgs);

            /*
             * Why is this loop run after OnTurnEnd is called? 
             * We need to be able to process events that occur just before the end of the turn.
             */
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

            if (this.IsComplete())
            {
                BattleEndEventArgs battleEndEventArgs = new BattleEndEventArgs(this, this.Winner());
                CurrentWeather.OnBattleEnd(this, battleEndEventArgs);
                OnBattleEnd?.Invoke(this, battleEndEventArgs);

                currentState = State.END;
            }

            TurnCounter += 1;
        }

        public void Receive(Request request)
        {
            actionRequests.Add(request);
        }

        public void Receive(SwapPokemon swapPokemonAction)
        {
            IPokemon swappedPokemon = swapPokemonAction.Slot.Pokemon;

            SwapPokemonEventArgs swapPokemonEventArgs = new SwapPokemonEventArgs(this, swapPokemonAction);
            CurrentWeather.OnSwapPokemon(this, swapPokemonEventArgs);
            OnSwapPokemon?.Invoke(this, swapPokemonEventArgs);

            // TODO

            PokemonSwappedEventArgs pokemonSwappedEventArgs = new PokemonSwappedEventArgs(this, swapPokemonAction, swappedPokemon);
            CurrentWeather.OnPokemonSwapped(this, pokemonSwappedEventArgs);
            OnPokemonSwapped?.Invoke(this, pokemonSwappedEventArgs);
        }

        public void Receive(UseItem useItemAction)
        {
            UseItemEventArgs useItemEventArgs = new UseItemEventArgs(this, useItemAction);
            CurrentWeather.OnUseItem(this, useItemEventArgs);
            OnUseItem?.Invoke(this, useItemEventArgs);

            // TODO

            ItemUsedEventArgs itemUsedEventArgs = new ItemUsedEventArgs(this, useItemAction);
            CurrentWeather.OnItemUsed(this, itemUsedEventArgs);
            OnItemUsed?.Invoke(this, itemUsedEventArgs);
        }

        public void Receive(UseMove useMoveAction)
        {
            if (useMoveAction.Slot.Pokemon.HasFainted()) { return; }

            UseMoveEventArgs useMoveEventArgs = new UseMoveEventArgs(this, useMoveAction);
            CurrentWeather.OnUseMove(this, useMoveEventArgs);
            OnUseMove?.Invoke(this, useMoveEventArgs);

            useMoveAction.Move.Use(this, useMoveAction);

            MoveUsedEventArgs moveUsedEventArgs = new MoveUsedEventArgs(this, useMoveAction);
            CurrentWeather.OnMoveUsed(this, moveUsedEventArgs);
            OnMoveUsed?.Invoke(this, moveUsedEventArgs);
        }

        public void Receive(UseRun runAction)
        {
            UseRunEventArgs useRunEventArgs = new UseRunEventArgs(this, runAction);
            CurrentWeather.OnUseRun(this, useRunEventArgs);
            OnUseRun?.Invoke(this, useRunEventArgs);

            // TODO

            RunUsedEventArgs runUsedEventArgs = new RunUsedEventArgs(this, runAction);
            CurrentWeather.OnRunUsed(this, runUsedEventArgs);
            OnRunUsed?.Invoke(this, runUsedEventArgs);
        }

        public void Receive(InflictDamage inflictDamage)
        {
            InflictDamageEventArgs inflictDamageEventArgs = new InflictDamageEventArgs(this, inflictDamage);
            CurrentWeather.OnInflictDamage(this, inflictDamageEventArgs);
            OnInflictDamage?.Invoke(this, inflictDamageEventArgs);

            inflictDamage.Apply();

            DamageInflictedEventArgs damageInflictedEventArgs = new DamageInflictedEventArgs(this, inflictDamage);
            CurrentWeather.OnDamageInflicted(this, damageInflictedEventArgs);
            OnDamageInflicted?.Invoke(this, damageInflictedEventArgs);
        }

        public void Receive(MoveUseFailure moveFailure)
        {
            MoveUseFailureEventArgs moveUseFailureEventArgs = new MoveUseFailureEventArgs(this, moveFailure);
            CurrentWeather.OnMoveUseFailure(this, moveUseFailureEventArgs);
            OnMoveUseFailure?.Invoke(this, moveUseFailureEventArgs);

            // TODO

            MoveUseFailedEventArgs moveUseFailedEventArgs = new MoveUseFailedEventArgs(this, moveFailure);
            CurrentWeather.OnMoveUseFailed(this, moveUseFailedEventArgs);
            OnMoveUseFailed?.Invoke(this, moveUseFailedEventArgs);
        }

        public void Receive(ShiftStatStage shiftStatStage)
        {
            ShiftStatStageEventArgs shiftStatStageEventArgs = new ShiftStatStageEventArgs(this, shiftStatStage);
            CurrentWeather.OnShiftStatStage(this, shiftStatStageEventArgs);
            OnShiftStatStage?.Invoke(this, shiftStatStageEventArgs);

            shiftStatStage.Apply();

            StatStageShiftedEventArgs statStageShiftedEventArgs = new StatStageShiftedEventArgs(this, shiftStatStage);
            CurrentWeather.OnStatStageShifted(this, statStageShiftedEventArgs);
            OnStatStageShifted?.Invoke(this, statStageShiftedEventArgs);
        }

        public void Receive(WeatherChange weatherChange)
        {
            Weather previousWeather = CurrentWeather;

            if (SurroundingWeather == weatherChange.Weather)
            {
                if (CurrentWeather != SurroundingWeather)
                {
                    ChangeWeatherEventArgs changeWeatherEventArgs = new ChangeWeatherEventArgs(this, weatherChange);
                    CurrentWeather.OnChangeWeather(this, changeWeatherEventArgs);
                    OnChangeWeather?.Invoke(this, changeWeatherEventArgs);

                    CurrentWeather = SurroundingWeather;

                    WeatherChangedEventArgs weatherChangedEventArgs = new WeatherChangedEventArgs(this, previousWeather, CurrentWeather);
                    CurrentWeather.OnWeatherChanged(this, weatherChangedEventArgs);
                    OnWeatherChanged?.Invoke(this, weatherChangedEventArgs);
                }
            }

            bool change = CurrentWeather == weatherChange.Weather;


            if (change)
            {
                ChangeWeatherEventArgs changeWeatherEventArgs = new ChangeWeatherEventArgs(this, weatherChange);
                CurrentWeather.OnChangeWeather(this, changeWeatherEventArgs);
                OnChangeWeather?.Invoke(this, changeWeatherEventArgs);
            }

            CurrentWeather.Reset(weatherChange.TurnCount);

            if (change)
            {
                WeatherChangedEventArgs weatherChangedEventArgs = new WeatherChangedEventArgs(this, previousWeather, CurrentWeather);
                CurrentWeather.OnWeatherChanged(this, weatherChangedEventArgs);
                OnWeatherChanged?.Invoke(this, weatherChangedEventArgs);
            }
        }

        public void Receive(MoveOperation moveOperation)
        {
            PerformMoveOperationEventArgs performMoveOperationEventArgs = new PerformMoveOperationEventArgs(this, moveOperation);
            CurrentWeather.OnPerformMoveOperation(this, performMoveOperationEventArgs);
            OnPerformMoveOperation?.Invoke(this, performMoveOperationEventArgs);

            moveOperation.PerformOperation(this);

            MoveOperationPerformedEventArgs moveOperationPerformedEventArgs = new MoveOperationPerformedEventArgs(this, moveOperation);
            CurrentWeather.OnMoveOperationPerformed(this, moveOperationPerformedEventArgs);
            OnMoveOperationPerformed?.Invoke(this, moveOperationPerformedEventArgs);
        }

        public void Receive(EffectOperation effectOperation)
        {
            PerformEffectOperationEventArgs performEffectOperationEventArgs= new PerformEffectOperationEventArgs(this, effectOperation);
            CurrentWeather.OnPerformEffectOperation(this, performEffectOperationEventArgs);
            OnPerformEffectOperation?.Invoke(this, performEffectOperationEventArgs);

            effectOperation.PerformOperation(this);

            EffectOperationPerformedEventArgs effectOperationPerformedEventArgs = new EffectOperationPerformedEventArgs(this, effectOperation);
            CurrentWeather.OnEffectOperationPerformed(this, effectOperationPerformedEventArgs);
            OnEffectOperationPerformed?.Invoke(this, effectOperationPerformedEventArgs);
        }
    }
}