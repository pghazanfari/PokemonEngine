using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class UseRun : IAction
    {
        public UseRun(Slot slot) : base(slot) { }

        public override int Priority
        {
            get
            {
                return 6; // TODO: Find what the actual priority is. I can't find it anywhere online.
            }
        }

        public override void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
