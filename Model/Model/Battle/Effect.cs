﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle
{
    public abstract class Effect
    {
        public void OnBattleStart(object sender, EventArgs args) { }
        public void OnBattleEnd(object sender, BattleEndEventArgs args) { }

        public void OnTurnStart(object sender, EventArgs args) { }
        public void OnTurnEnd(object sender, EventArgs args) { }
        public void OnMessageBroadcast(object sender, EventArgs args) { }

        public void OnRequestInput(object sender, RequestInputEventArgs args) { }
        public void OnInputReceived(object sender, InputReceivedEventArgs args) { }

        public void OnSwapPokemon(object sender, SwapPokemonEventArgs args) { }
        public void OnPokemonSwapped(object sender, PokemonSwappedEventArgs args) { }

        public void OnUseItem(object sender, UseItemEventArgs args) { }
        public void OnItemUsed(object sender, ItemUsedEventArgs args) { }

        public void OnUseMove(object sender, UseMoveEventArgs args) { }
        public void OnMoveUsed(object sender, MoveUsedEventArgs args) { }

        public void OnUseRun(object sender, UseRunEventArgs args) { }
        public void OnRunUsed(object sender, RunUsedEventArgs args) { }

        public void OnInflictMoveDamage(object sender, InflictDamageEventArgs args) { }
        public void OnMoveDamageInflicted(object sender, DamageInflictedEventArgs args) { }

        public void OnShiftStatStage(object sender, ShiftStatStageEventArgs args) { }
        public void OnStatStageShifted(object sender, StatStageShiftedEventArgs args) { }

        public void OnChangeWeather(object sender, ChangeWeatherEventArgs args) { }
        public void OnWeatherChanged(object sender, WeatherChangedEventArgs args) { }
        public void OnWeatherCompleted(object sender, WeatherCompletedEventArgs args) { }

        public void OnPerformMoveOperation(object sender, PerformMoveOperationEventArgs args) { }
        public void OnMoveOperationPerformed(object sender, MoveOperationPerformedEventArgs args) { }

        public void OnPerformEffectOperation(object sender, PerformEffectOperationEventArgs args) { }
        public void OnEffectOperationPerformed(object sender, EffectOperationPerformedEventArgs args) { }
    }
}
