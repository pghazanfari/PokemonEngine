using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Messaging
{
    public interface IBattleMessage
    {
        void Dispatch(IBattleMessageSubscriber receiver);
    }
}
