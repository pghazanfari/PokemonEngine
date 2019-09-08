using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public abstract class EffectOperation : IMessage
    {
        public Effect Effect { get; }

        public EffectOperation(Effect effect)
        {
            Effect = effect;
        }

        public abstract void PerformOperation(IBattle battle);

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }

        public void Dispose()
        {
            // Do Nothing
        }
    }
}
