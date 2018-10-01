using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public class ShiftStatStage : IMessage
    {
        public readonly IPokemon Pokemon;
        public readonly Statistic Stat;
        public readonly int Delta;

        public ShiftStatStage(IPokemon pokemon, Statistic stat, int delta)
        {
            Pokemon = pokemon;
            Stat = stat;
            Delta = delta;
        }

        public void Apply()
        {
            Pokemon.Stats.ShiftStage(Stat, Delta);
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
