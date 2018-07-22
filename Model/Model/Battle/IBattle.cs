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
        int TurnCounter { get; }
        IComparer<IAction> ActionComparer { get; set; }
        Weather SurroundingWeather { get; }
        Weather CurrentWeather { get; }

        event EventHandler<EventArgs> OnBattleStart;
        event EventHandler<BattleEndEventArgs> OnBattleEnd;

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

        event EventHandler<InflictDamageEventArgs> OnInflictDamage;
        event EventHandler<DamageInflictedEventArgs> OnDamageInflicted;

        event EventHandler<MoveUseFailureEventArgs> OnMoveUseFailure;
        event EventHandler<MoveUseFailedEventArgs> OnMoveUseFailed;

        event EventHandler<ShiftStatStageEventArgs> OnShiftStatStage;
        event EventHandler<StatStageShiftedEventArgs> OnStatStageShifted;

        event EventHandler<ChangeWeatherEventArgs> OnChangeWeather;
        event EventHandler<WeatherChangedEventArgs> OnWeatherChanged;
        event EventHandler<WeatherCompletedEventArgs> OnWeatherCompleted;

        event EventHandler<PerformMoveOperationEventArgs> OnPerformMoveOperation;
        event EventHandler<MoveOperationPerformedEventArgs> OnMoveOperationPerformed;

        event EventHandler<PerformEffectOperationEventArgs> OnPerformEffectOperation;
        event EventHandler<EffectOperationPerformedEventArgs> OnEffectOperationPerformed;

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
