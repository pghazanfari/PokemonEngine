using System.Collections.Generic;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle
{
    public class EventArgs : System.EventArgs
    {
        public IBattle Battle { get; }

        public EventArgs(IBattle battle)
        {
            Battle = battle;
        }
    }

    #region Battle
    public class BattleEndEventArgs : EventArgs
    {
        public Team Winner { get; }
        public BattleEndEventArgs(IBattle battle, Team winner) : base(battle)
        {
            Winner = winner;
        }
    }

    public class RequestInputEventArgs : EventArgs
    {
        public IList<Request> Requests { get; }
        public RequestInputEventArgs(IBattle battle, IList<Request> requests) : base(battle)
        {
            Requests = requests;
        }
    }

    public class InputReceivedEventArgs : EventArgs
    {
        public IList<Request> Requests { get; }
        public IList<IAction> Inputs { get; }
        public InputReceivedEventArgs(IBattle battle, IList<Request> requests, IList<IAction> inputs) : base(battle)
        {
            Requests = requests;
            Inputs = inputs;
        }
    }

    public class SwapPokemonEventArgs : EventArgs
    {
        public SwapPokemon Action { get; }
        public SwapPokemonEventArgs(IBattle battle, SwapPokemon action) : base(battle)
        {
            Action = action;
        }
    }

    public class PokemonSwappedEventArgs : EventArgs
    {
        // TODO: Add anything else that is needed
        public SwapPokemon Action { get; }
        public IPokemon SwappedPokemon { get; }
        public PokemonSwappedEventArgs(IBattle battle, SwapPokemon action, IPokemon swappedPokemon) : base(battle)
        {
            Action = action;
            SwappedPokemon = swappedPokemon;
        }
    }

    public class UseItemEventArgs : EventArgs
    {
        public UseItem Action { get; }
        public UseItemEventArgs(IBattle battle, UseItem action) : base(battle)
        {
            Action = action;
        }
    }

    public class ItemUsedEventArgs : EventArgs
    {
        // TODO: Add anything else
        public UseItem Action { get; }
        public ItemUsedEventArgs(IBattle battle, UseItem action) : base(battle)
        {
            Action = action;
        }
    }

    public class UseMoveEventArgs : EventArgs
    {
        public UseMove Action { get; }
        public UseMoveEventArgs(IBattle battle, UseMove action) : base(battle)
        {
            Action = action;
        }
    }

    public class MoveUsedEventArgs : EventArgs
    {
        // TODO: Add anything else
        public UseMove Action { get; }
        public MoveUsedEventArgs(IBattle battle, UseMove action) : base(battle)
        {
            Action = action;
        }
    }

    public class UseRunEventArgs : EventArgs
    {
        public UseRun Action { get; }
        public UseRunEventArgs(IBattle battle, UseRun action) : base(battle)
        {
            Action = action;
        }
    }

    public class RunUsedEventArgs : EventArgs
    {
        // TODO: Add anything else
        public UseRun Action { get; }
        public RunUsedEventArgs(IBattle battle, UseRun action) : base(battle)
        {
            Action = action;
        }
    }

    public class InflictDamageEventArgs : EventArgs
    {
        public InflictDamage Action;
        public InflictDamageEventArgs(IBattle battle, InflictDamage action) : base(battle)
        {
            Action = action;
        }
    }

    public class DamageInflictedEventArgs : EventArgs
    {
        // TODO: Add anything else
        public InflictDamage Action { get; }
        public DamageInflictedEventArgs(IBattle battle, InflictDamage action) : base(battle)
        {
            Action = action;
        }
    }

    public class MoveUseFailureEventArgs : EventArgs
    {
        public MoveUseFailure Action { get; }
        public MoveUseFailureEventArgs(IBattle battle, MoveUseFailure action) : base(battle)
        {
            Action = action;
        }
    }

    public class MoveUseFailedEventArgs : EventArgs
    {
        public MoveUseFailure Action { get; }
        public MoveUseFailedEventArgs(IBattle battle, MoveUseFailure action) : base(battle)
        {
            Action = action;
        }
    }

    public class ShiftStatStageEventArgs : EventArgs
    {
        public ShiftStatStage Action { get; }
        public ShiftStatStageEventArgs(IBattle battle, ShiftStatStage action) : base(battle)
        {
            Action = action;
        }
    }

    public class StatStageShiftedEventArgs : EventArgs
    {
        public ShiftStatStage Action { get; }
        public StatStageShiftedEventArgs(IBattle battle, ShiftStatStage action) : base(battle)
        {
            Action = action;
        }
    }

    public class ChangeWeatherEventArgs : EventArgs
    {
        public WeatherChange Action { get; }
        public ChangeWeatherEventArgs(IBattle battle, WeatherChange action) : base(battle)
        {
            Action = action;
        }
    }

    public class WeatherChangedEventArgs : EventArgs
    {
        public Weather PreviousWeather { get; }
        public Weather Weather { get; }

        public WeatherChangedEventArgs(IBattle battle, Weather previousWeather, Weather weather) : base(battle)
        {
            PreviousWeather = previousWeather;
            Weather = weather;
        }
    }

    public class WeatherCompletedEventArgs : EventArgs
    {
        public Weather Weather { get; }

        public WeatherCompletedEventArgs(IBattle battle, Weather weather) : base(battle)
        {
            Weather = weather;
        }
    }

    public class PerformMoveOperationEventArgs : EventArgs
    {
        public MoveOperation Operation { get; }
        public PerformMoveOperationEventArgs(IBattle battle, MoveOperation operation) : base(battle)
        {
            Operation = operation;
        }
    }

    public class MoveOperationPerformedEventArgs : EventArgs
    {
        public MoveOperation Operation { get; }
        public MoveOperationPerformedEventArgs(IBattle battle, MoveOperation operation) : base(battle)
        {
            Operation = operation;
        }
    }
    
    public class PerformEffectOperationEventArgs : EventArgs
    {
        public EffectOperation Operation { get; }

        public PerformEffectOperationEventArgs(IBattle battle, EffectOperation operation) : base(battle)
        {
            Operation = operation;
        }
    }

    public class EffectOperationPerformedEventArgs : EventArgs
    {
        public EffectOperation Operation { get; }

        public EffectOperationPerformedEventArgs(IBattle battle, EffectOperation operation) : base(battle)
        {
            Operation = operation;
        }
    }
    #endregion

    #region Pokemon
    public class PokemonEventArgs : EventArgs
    {
        public IPokemon Pokemon { get; }
        public PokemonEventArgs(IBattle battle, IPokemon pokemon) : base(battle)
        {
            Pokemon = pokemon;
        }
    }

    public class UpdateStatisticStageEventArgs : PokemonEventArgs
    {
        public Statistic Stat { get; }
        public int Levels { get; }
        public UpdateStatisticStageEventArgs(IBattle battle, IPokemon pokemon, Statistic stat, int levels) : base(battle, pokemon)
        {
            Stat = stat;
            Levels = levels;
        }
    }

    public class StatisticStageUpdatedEventArgs : PokemonEventArgs
    {
        public Statistic Stat { get; }
        public int PreviousLevel { get; }
        public int NumLevels { get; }

        public StatisticStageUpdatedEventArgs(IBattle battle, IPokemon pokemon, Statistic stat, int previousLevel) : base(battle, pokemon)
        {
            Stat = stat;
            PreviousLevel = previousLevel;
            NumLevels = pokemon.Stats.Stage(stat) - PreviousLevel;
        }
    }
    #endregion
}
