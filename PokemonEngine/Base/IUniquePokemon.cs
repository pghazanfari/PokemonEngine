using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public interface IUniquePokemon : IPokemon
    {
        PGender Gender { get; }
        PIVSet IVs { get; }
        PEVSet EVs { get; }
        int Level { get; }
        int Friendship { get; }

        int LevelUp();
        int UpdateFriendship(int offset);
    }
}
