using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle
{
    public class Battle : IBattle
    {
        public Random RNG { get; }
        public IReadOnlyList<Team> Teams { get; }
        public Messaging.Queue MessageQueue { get; }

        private readonly IList<Effect> effects;
        public IReadOnlyList<Effect> Effects { get; }
        public Weather SurroundingWeather { get; }

        // CurrentWeather will be invoked manually before events in order to ensure
        // that the CurrentWeather's effect always applies.
        private Weather currentWeather;
        public Weather CurrentWeather
        {
            get
            {
                return currentWeather;
            }
            set
            {
                DeregisterEffect(currentWeather);
                currentWeather = value;
                RegisterEffect(currentWeather);
            }
        }

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

            Teams = new List<Team>(teams).AsReadOnly();
            InputProvider = inputProvider;
            RNG = rng;
            SurroundingWeather = weather;

            effects = new List<Effect>();
            Effects = (effects as List<Effect>).AsReadOnly();
            
            actionRequests = new List<Request>();

            MessageQueue = new Messaging.Queue();
            MessageQueue.AddSubscriber(this);

            ActionComparer = new Actions.Comparer(RNG);
            currentState = State.START;
            TurnCounter = 1;

            CurrentWeather = SurroundingWeather;

            BattleArgs = new EventArgs(this);
        }

        public Battle(IInputProvider inputProvider, Model.Weather weather, IEnumerable<Team> teams) : this(new Random(), inputProvider, weather, teams) { }
        public Battle(IInputProvider inputProvider, Model.Weather weather, params Team[] teams) : this(inputProvider, weather, teams as IEnumerable<Team>) { }
        public Battle(Random rng, IInputProvider inputProvider, Model.Weather weather, params Team[] teams) : this(rng, inputProvider, weather, teams as IEnumerable<Team>) { }

        public IEnumerator<Team> GetEnumerator()
        {
            return Teams.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Teams.GetEnumerator();
        }

        private void Flush()
        {
            while (MessageQueue.HasNext) Broadcast();
        }

        private void Broadcast()
        {
            EventArgs args = new EventArgs(this);
            OnMessageBroadcast?.Invoke(this, args);

            MessageQueue.Broadcast();
        }

        public bool RegisterEffect(Effect effect)
        {
            if (effect == null || effects.Contains(effect)) { return false; }
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
            if (effect == null) return false;

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
                //Add effects of all Pokemon Abilities in battle
                foreach (Team team in Teams)
                {
                    foreach (Slot slot in team)
                    {
                        RegisterEffect(slot.Pokemon.Ability.newEffect(this, slot.Pokemon));
                    }
                }

                OnBattleStart?.Invoke(this, BattleArgs);

                currentState = State.IN_PROGRESS;
            }

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
                        MessageQueue.Enqueue(new Request(slot));
                    }
                }
            }
            actionRequests.Clear();
            Flush();

            RequestInputEventArgs requestInputEventArgs = new RequestInputEventArgs(this, actionRequests);
            OnRequestInput?.Invoke(this, requestInputEventArgs);

            List<IAction> actions = new List<IAction>(InputProvider.ProvideActions(this, actionRequests));

            InputReceivedEventArgs inputReceivedEventArgs = new InputReceivedEventArgs(this, actionRequests, actions);
            OnInputReceived?.Invoke(this, inputReceivedEventArgs);

            actions.Sort(ActionComparer);

            foreach (IAction action in actions)
            {
                MessageQueue.Enqueue(action);
            }

            while (true)
            {
                Flush();

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
                foreach (SwapPokemon swapPokemonAction in swapPokemonActions) { MessageQueue.Enqueue(swapPokemonAction); }
            }

            CurrentWeather.DecrementTurnCounter();
            if (CurrentWeather.IsComplete)
            {
                WeatherCompletedEventArgs weatherCompletedEventArgs = new WeatherCompletedEventArgs(this, CurrentWeather);
                OnWeatherCompleted?.Invoke(this, weatherCompletedEventArgs);

                MessageQueue.AddFirst(new WeatherChange(SurroundingWeather, -1));
                Broadcast();
            }

            OnTurnEnd?.Invoke(this, BattleArgs);

            /*
             * Why is this loop run after OnTurnEnd is called? 
             * We need to be able to process events that occur just before the end of the turn.
             */
            while (true)
            {
                Flush();

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
                foreach (SwapPokemon swapPokemonAction in swapPokemonActions) { MessageQueue.Enqueue(swapPokemonAction); }
            }

            if (this.IsComplete())
            {
                BattleEndEventArgs battleEndEventArgs = new BattleEndEventArgs(this, this.Winner());
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
            OnSwapPokemon?.Invoke(this, swapPokemonEventArgs);

            // TODO

            PokemonSwappedEventArgs pokemonSwappedEventArgs = new PokemonSwappedEventArgs(this, swapPokemonAction, swappedPokemon);
            OnPokemonSwapped?.Invoke(this, pokemonSwappedEventArgs);
        }

        public void Receive(UseItem useItemAction)
        {
            UseItemEventArgs useItemEventArgs = new UseItemEventArgs(this, useItemAction);
            OnUseItem?.Invoke(this, useItemEventArgs);

            // TODO

            ItemUsedEventArgs itemUsedEventArgs = new ItemUsedEventArgs(this, useItemAction);
            OnItemUsed?.Invoke(this, itemUsedEventArgs);
        }

        public void Receive(UseMove useMoveAction)
        {
            if (useMoveAction.Slot.Pokemon.HasFainted()) { return; }

            UseMoveEventArgs useMoveEventArgs = new UseMoveEventArgs(this, useMoveAction);
            OnUseMove?.Invoke(this, useMoveEventArgs);

            useMoveAction.Move.Use(this, useMoveAction);

            MoveUsedEventArgs moveUsedEventArgs = new MoveUsedEventArgs(this, useMoveAction);
            OnMoveUsed?.Invoke(this, moveUsedEventArgs);
        }

        public void Receive(UseRun runAction)
        {
            UseRunEventArgs useRunEventArgs = new UseRunEventArgs(this, runAction);
            OnUseRun?.Invoke(this, useRunEventArgs);

            // TODO

            RunUsedEventArgs runUsedEventArgs = new RunUsedEventArgs(this, runAction);
            OnRunUsed?.Invoke(this, runUsedEventArgs);
        }

        public void Receive(InflictDamage inflictDamage)
        {
            InflictDamageEventArgs inflictDamageEventArgs = new InflictDamageEventArgs(this, inflictDamage);
            OnInflictDamage?.Invoke(this, inflictDamageEventArgs);

            inflictDamage.Apply();

            DamageInflictedEventArgs damageInflictedEventArgs = new DamageInflictedEventArgs(this, inflictDamage);
            OnDamageInflicted?.Invoke(this, damageInflictedEventArgs);
        }

        public void Receive(MoveUseFailure moveFailure)
        {
            MoveUseFailureEventArgs moveUseFailureEventArgs = new MoveUseFailureEventArgs(this, moveFailure);
            OnMoveUseFailure?.Invoke(this, moveUseFailureEventArgs);

            // TODO

            MoveUseFailedEventArgs moveUseFailedEventArgs = new MoveUseFailedEventArgs(this, moveFailure);
            OnMoveUseFailed?.Invoke(this, moveUseFailedEventArgs);
        }

        public void Receive(ShiftStatStage shiftStatStage)
        {
            ShiftStatStageEventArgs shiftStatStageEventArgs = new ShiftStatStageEventArgs(this, shiftStatStage);
            OnShiftStatStage?.Invoke(this, shiftStatStageEventArgs);

            shiftStatStage.Apply();

            StatStageShiftedEventArgs statStageShiftedEventArgs = new StatStageShiftedEventArgs(this, shiftStatStage);
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
                    OnChangeWeather?.Invoke(this, changeWeatherEventArgs);

                    CurrentWeather = SurroundingWeather;

                    WeatherChangedEventArgs weatherChangedEventArgs = new WeatherChangedEventArgs(this, previousWeather, CurrentWeather);
                    OnWeatherChanged?.Invoke(this, weatherChangedEventArgs);
                }
            }

            bool change = CurrentWeather == weatherChange.Weather;


            if (change)
            {
                ChangeWeatherEventArgs changeWeatherEventArgs = new ChangeWeatherEventArgs(this, weatherChange);
                OnChangeWeather?.Invoke(this, changeWeatherEventArgs);
            }

            CurrentWeather.Reset(weatherChange.TurnCount);

            if (change)
            {
                WeatherChangedEventArgs weatherChangedEventArgs = new WeatherChangedEventArgs(this, previousWeather, CurrentWeather);
                OnWeatherChanged?.Invoke(this, weatherChangedEventArgs);
            }
        }

        public void Receive(MoveOperation moveOperation)
        {
            PerformMoveOperationEventArgs performMoveOperationEventArgs = new PerformMoveOperationEventArgs(this, moveOperation);
            OnPerformMoveOperation?.Invoke(this, performMoveOperationEventArgs);

            moveOperation.PerformOperation(this);

            MoveOperationPerformedEventArgs moveOperationPerformedEventArgs = new MoveOperationPerformedEventArgs(this, moveOperation);
            OnMoveOperationPerformed?.Invoke(this, moveOperationPerformedEventArgs);
        }

        public void Receive(EffectOperation effectOperation)
        {
            PerformEffectOperationEventArgs performEffectOperationEventArgs= new PerformEffectOperationEventArgs(this, effectOperation);
            OnPerformEffectOperation?.Invoke(this, performEffectOperationEventArgs);

            effectOperation.PerformOperation(this);

            EffectOperationPerformedEventArgs effectOperationPerformedEventArgs = new EffectOperationPerformedEventArgs(this, effectOperation);
            OnEffectOperationPerformed?.Invoke(this, effectOperationPerformedEventArgs);
        }
    }
}