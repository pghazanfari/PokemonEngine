using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Messaging
{
    public interface IMessageReceiver<T> where T : IMessage<T>
    {
        void Receive(IMessage<T> message);
    }
}
