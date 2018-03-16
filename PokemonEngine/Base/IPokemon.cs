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
        IReadOnlyList<PokemonType> Types { get; }
        ExperienceGroup ExpGroup { get; }
        Moves PossibleMoves { get; }
        BaseStats BaseStats { get; }
        IReadOnlyList<Ability> PossibleAbilities { get; }
        int BaseFriendship { get; }
        //TODO: Natures
    }
}
