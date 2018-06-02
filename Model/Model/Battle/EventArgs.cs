using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle
{
    public class EventArgs : System.EventArgs
    {
        public readonly IBattle Battle;

        public EventArgs(IBattle battle)
        {
            Battle = battle;
        }
    }

    #region Battle
    public class BattleEndEventArgs : EventArgs
    {
        public readonly Team Winner;
        public BattleEndEventArgs(IBattle battle, Team winner) : base(battle)
        {
            Winner = winner;
        }
    }

    public class RequestInputEventArgs : EventArgs
    {
        public readonly IList<Request> Requests;
        public RequestInputEventArgs(IBattle battle, IList<Request> requests) : base(battle)
        {
            Requests = requests;
        }
    }

    public class InputReceivedEventArgs : EventArgs
    {
        public readonly IList<Request> Requests;
        public readonly IList<IAction> Inputs;
        public InputReceivedEventArgs(IBattle battle, IList<Request> requests, IList<IAction> inputs) : base(battle)
        {
            Requests = requests;
            Inputs = inputs;
        }
    }

    public class SwapPokemonEventArgs : EventArgs
    {
        public readonly SwapPokemon Action;
        public SwapPokemonEventArgs(IBattle battle, SwapPokemon action) : base(battle)
        {
            Action = action;
        }
    }

    public class PokemonSwappedEventArgs : EventArgs
    {
        // TODO: Add anything else that is needed
        public readonly SwapPokemon Action;
        public PokemonSwappedEventArgs(IBattle battle, SwapPokemon action) : base(battle)
        {
            Action = action;
        }
    }

    public class UseItemEventArgs : EventArgs
    {
        public readonly UseItem Action;
        public UseItemEventArgs(IBattle battle, UseItem action) : base(battle)
        {
            Action = action;
        }
    }

    public class ItemUsedEventArgs : EventArgs
    {
        // TODO: Add anything else
        public readonly UseItem Action;
        public ItemUsedEventArgs(IBattle battle, UseItem action) : base(battle)
        {
            Action = action;
        }
    }

    public class UseMoveEventArgs : EventArgs
    {
        public readonly UseMove Action;
        public UseMoveEventArgs(IBattle battle, UseMove action) : base(battle)
        {
            Action = action;
        }
    }

    public class MoveUsedEventArgs : EventArgs
    {
        // TODO: Add anything else
        public readonly UseMove Action;
        public MoveUsedEventArgs(IBattle battle, UseMove action) : base(battle)
        {
            Action = action;
        }
    }

    public class UseRunEventArgs : EventArgs
    {
        public readonly UseRun Action;
        public UseRunEventArgs(IBattle battle, UseRun action) : base(battle)
        {
            Action = action;
        }
    }

    public class RunUsedEventArgs : EventArgs
    {
        // TODO: Add anything else
        public readonly UseRun Action;
        public RunUsedEventArgs(IBattle battle, UseRun action) : base(battle)
        {
            Action = action;
        }
    }

    public class InflictMoveDamageEventArgs : EventArgs
    {
        public InflictMoveDamage Action;
        public InflictMoveDamageEventArgs(IBattle battle, InflictMoveDamage action) : base(battle)
        {
            Action = action;
        }
    }

    public class MoveDamageInflictedEventArgs : EventArgs
    {
        // TODO: Add anything else
        public readonly InflictMoveDamage Action;
        public MoveDamageInflictedEventArgs(IBattle battle, InflictMoveDamage action) : base(battle)
        {
            Action = action;
        }
    }

    public class ShiftStatStageEventArgs : EventArgs
    {
        public readonly ShiftStatStage Action;
        public ShiftStatStageEventArgs(IBattle battle, ShiftStatStage action) : base(battle)
        {
            Action = action;
        }
    }

    public class StatStageShiftedEventArgs : EventArgs
    {
        public readonly ShiftStatStage Action;
        public StatStageShiftedEventArgs(IBattle battle, ShiftStatStage action) : base(battle)
        {
            Action = action;
        }
    }

    public class PerformMoveOperationEventArgs : EventArgs
    {
        public readonly MoveOperation Operation;
        public PerformMoveOperationEventArgs(IBattle battle, MoveOperation operation) : base(battle)
        {
            Operation = operation;
        }
    }

    public class MoveOperationPerformedEventArgs : EventArgs
    {
        public readonly MoveOperation Operation;
        public MoveOperationPerformedEventArgs(IBattle battle, MoveOperation operation) : base(battle)
        {
            Operation = operation;
        }
    }
    
    public class PerformEffectOperationEventArgs : EventArgs
    {
        public readonly EffectOperation Operation;

        public PerformEffectOperationEventArgs(IBattle battle, EffectOperation operation) : base(battle)
        {
            Operation = operation;
        }
    }

    public class EffectOperationPerformedEventArgs : EventArgs
    {
        public readonly EffectOperation Operation;

        public EffectOperationPerformedEventArgs(IBattle battle, EffectOperation operation) : base(battle)
        {
            Operation = operation;
        }
    }
    #endregion

    #region Pokemon
    public class PokemonEventArgs : EventArgs
    {
        public readonly IPokemon Pokemon;
        public PokemonEventArgs(IBattle battle, IPokemon pokemon) : base(battle)
        {
            Pokemon = pokemon;
        }
    }

    public class UpdateStatisticStageEventArgs : PokemonEventArgs
    {
        public readonly Statistic Stat;
        public readonly int Levels;
        public UpdateStatisticStageEventArgs(IBattle battle, IPokemon pokemon, Statistic stat, int levels) : base(battle, pokemon)
        {
            Stat = stat;
            Levels = levels;
        }
    }

    public class StatisticStageUpdatedEventArgs : PokemonEventArgs
    {
        public readonly Statistic Stat;
        public readonly int PreviousLevel;
        public readonly int NumLevels;

        public StatisticStageUpdatedEventArgs(IBattle battle, IPokemon pokemon, Statistic stat, int previousLevel) : base(battle, pokemon)
        {
            Stat = stat;
            PreviousLevel = previousLevel;
            NumLevels = pokemon.Stats.Stage(stat) - PreviousLevel;
        }
    }
    #endregion
}
