using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle;
using PokemonEngine.Model.Battle.Actions;

namespace ModelUnitTests.Util
{
    public class NoOpInputProvider : IInputProvider
    {
        public static readonly NoOpInputProvider Instance = new NoOpInputProvider();

        private NoOpInputProvider() { }

        public IList<IAction> ProvideActions(IBattle battle, IList<Request> requests)
        {
            return new List<IAction>();
        }

        public IList<SwapPokemon> ProvideSwapPokemon(IBattle battle, IList<Request> requests)
        {
            return new List<SwapPokemon>();
        }
    }
}
