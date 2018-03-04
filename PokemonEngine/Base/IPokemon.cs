using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public interface IPokemon
    {
        string Species { get; }
        IReadOnlyList<PType> Types { get; }
        PMoveSet MoveSet { get; }
        PBaseStats BaseStats { get; }
        IReadOnlyList<PAbility> PossibleAbilities { get; }
        int BaseFriendship { get; }
    }
}
