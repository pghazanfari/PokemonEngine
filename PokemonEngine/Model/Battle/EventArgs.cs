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
}
