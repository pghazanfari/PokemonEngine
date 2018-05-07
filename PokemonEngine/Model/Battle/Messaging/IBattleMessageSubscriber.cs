using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Requests;
using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Battle.Messaging
{
    public interface IBattleMessageSubscriber
    {
        void Receive(Run runAction);
        void Receive(UseMove useMoveAction);
        void Receive(UseItem useItemAction);
        void Receive(SwapPokemon swapPokemonAction);
        void Receive(BattleActionRequest request);
    }
}
