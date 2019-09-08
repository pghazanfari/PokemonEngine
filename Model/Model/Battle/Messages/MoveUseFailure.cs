using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public class MoveUseFailure : IMessage
    {
        public IMove Move { get; }

        public MoveUseFailure(IMove move)
        {
            Move = move;
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }

        public void Dispose()
        {
            //Do Nothing
        }
    }
}
