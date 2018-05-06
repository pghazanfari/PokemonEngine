using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Messaging
{
    public interface IMessage<T> where T : IMessage<T>
    {
        T Message { get; }
        void Dispatch(IMessageReceiver<T> receiver);
    }
}
