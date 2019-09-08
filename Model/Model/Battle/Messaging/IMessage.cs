using System;

namespace PokemonEngine.Model.Battle.Messaging
{
    public interface IMessage : IDisposable
    {
        void Dispatch(ISubscriber receiver);
    }
}
