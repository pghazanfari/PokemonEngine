using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle
{
    public interface IBattle : ISubscriber, IEnumerable<Team>
    {
        Random RNG { get; }
        IReadOnlyList<Team> Teams { get; }
        Queue MessageQueue { get; }
        IReadOnlyList<Effect> Effects { get; }

        // TODO: Change the second parameter for the post-message events.
        //       The same type is used as a placeholder for now.

        event EventHandler<EventArgs> OnTurnStart;
        event EventHandler<EventArgs> OnTurnEnd;
        event EventHandler<EventArgs> OnMessageBroadcast;

        event EventHandler<RequestInputEventArgs> OnRequestInput;
        event EventHandler<InputReceivedEventArgs> OnInputReceived;

        event EventHandler<SwapPokemonEventArgs> OnSwapPokemon;
        event EventHandler<PokemonSwappedEventArgs> OnPokemonSwapped;

        event EventHandler<UseItemEventArgs> OnUseItem;
        event EventHandler<ItemUsedEventArgs> OnItemUsed;

        event EventHandler<UseMoveEventArgs> OnUseMove;
        event EventHandler<MoveUsedEventArgs> OnMoveUsed;

        event EventHandler<UseRunEventArgs> OnUseRun;
        event EventHandler<RunUsedEventArgs> OnRunUsed;

        event EventHandler<InflictMoveDamageEventArgs> OnInflictMoveDamage;
        event EventHandler<MoveDamageInflictedEventArgs> OnMoveDamageInflicted;

        bool RegisterEffect(Effect effect);
        bool DeregisterEffect(Effect effect);

        void ExecuteTurn();
    }
    public static class IBattleImpl
    {
        public static bool IsComplete(this IBattle battle)
        {
            return battle.Teams.Count(x => x.HasLost()) >= battle.Teams.Count - 1;
        }

        public static Team Winner(this IBattle battle)
        {
            if (!battle.IsComplete()) { throw new InvalidOperationException("Battle has not completed yet"); }

            //TODO Account for a draw
            foreach (Team team in battle.Teams)
            {
                if (!team.HasLost())
                {
                    return team;
                }
            }

            throw new InvalidOperationException("Battle has no winner");
        }
    }
}
