using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Unique;

namespace PokemonEngine.Model.Battle
{
    public interface IPokemon : Unique.IPokemon
    {
        new Statistics Stats { get; }
        new MoveSet<IMove> Moves { get; }

        new IPokemon Clone();
    }

    public static class IPokemonImpl
    {
        public static bool HasFainted(this IPokemon pokemon)
        {
            return pokemon.HP == 0;
        }
    }
}
