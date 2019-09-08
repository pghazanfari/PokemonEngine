using System.Collections.Generic;

using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Battle
{
    public interface IInputProvider
    {
        IList<IAction> ProvideActions(IBattle battle, IList<Request> requests);
        IList<SwapPokemon> ProvideSwapPokemon(IBattle battle, IList<Request> requests);
    }
}
