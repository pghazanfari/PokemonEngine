using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class SwapPokemon : IAction
    {
        private readonly Slot user;
        public Slot User { get { return user; } }

        public SwapPokemon(Slot user)
        {
            this.user = user;
        }

        public void Dispatch(ISubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}
