namespace PokemonEngine.Model.Battle
{
    public abstract class Effect
    {
        public virtual void OnBattleStart(object sender, EventArgs args) { }
        public virtual void OnBattleEnd(object sender, BattleEndEventArgs args) { }

        public virtual void OnTurnStart(object sender, EventArgs args) { }
        public virtual void OnTurnEnd(object sender, EventArgs args) { }
        public virtual void OnMessageBroadcast(object sender, EventArgs args) { }

        public virtual void OnRequestInput(object sender, RequestInputEventArgs args) { }
        public virtual void OnInputReceived(object sender, InputReceivedEventArgs args) { }

        public virtual void OnSwapPokemon(object sender, SwapPokemonEventArgs args) { }
        public virtual void OnPokemonSwapped(object sender, PokemonSwappedEventArgs args) { }

        public virtual void OnUseItem(object sender, UseItemEventArgs args) { }
        public virtual void OnItemUsed(object sender, ItemUsedEventArgs args) { }

        public virtual void OnUseMove(object sender, UseMoveEventArgs args) { }
        public virtual void OnMoveUsed(object sender, MoveUsedEventArgs args) { }

        public virtual void OnUseRun(object sender, UseRunEventArgs args) { }
        public virtual void OnRunUsed(object sender, RunUsedEventArgs args) { }

        public virtual void OnInflictDamage(object sender, InflictDamageEventArgs args) { }
        public virtual void OnDamageInflicted(object sender, DamageInflictedEventArgs args) { }

        public virtual void OnMoveUseFailure(object sender, MoveUseFailureEventArgs args) { }
        public virtual void OnMoveUseFailed(object sender, MoveUseFailedEventArgs args) { }

        public virtual void OnShiftStatStage(object sender, ShiftStatStageEventArgs args) { }
        public virtual void OnStatStageShifted(object sender, StatStageShiftedEventArgs args) { }

        public virtual void OnChangeWeather(object sender, ChangeWeatherEventArgs args) { }
        public virtual void OnWeatherChanged(object sender, WeatherChangedEventArgs args) { }
        public virtual void OnWeatherCompleted(object sender, WeatherCompletedEventArgs args) { }

        public virtual void OnPerformMoveOperation(object sender, PerformMoveOperationEventArgs args) { }
        public virtual void OnMoveOperationPerformed(object sender, MoveOperationPerformedEventArgs args) { }

        public virtual void OnPerformEffectOperation(object sender, PerformEffectOperationEventArgs args) { }
        public virtual void OnEffectOperationPerformed(object sender, EffectOperationPerformedEventArgs args) { }
    }
}
