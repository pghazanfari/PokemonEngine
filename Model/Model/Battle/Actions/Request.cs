using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class Request : IMessage
    {
        public Slot Slot { get; }

        public Request(Slot slot)
        {
            Slot = slot;  
        }

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
