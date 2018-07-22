using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle.Messaging
{
    public interface ISubscriber
    {
        void Receive(UseRun runAction);
        void Receive(UseMove useMoveAction);
        void Receive(UseItem useItemAction);
        void Receive(SwapPokemon swapPokemonAction);
        void Receive(Request request);

        void Receive(InflictDamage inflictDamage);
        void Receive(ShiftStatStage shiftStatStage);
        void Receive(WeatherChange weatherChange);
        void Receive(MoveUseFailure moveFailure);

        void Receive(MoveOperation moveOperation);
        void Receive(EffectOperation effectOperation);
    }
}
