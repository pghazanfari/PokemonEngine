using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Messages
{
    public class ShiftStatStage : IMessage
    {
        public IPokemon Pokemon { get; }
        public Statistic Stat { get; }
        public int Delta { get; }

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
