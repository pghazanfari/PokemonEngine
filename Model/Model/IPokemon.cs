using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public interface IPokemon
    {
        string Species { get; }
        IReadOnlyList<PokemonType> Types { get; }
        IStatistics Stats { get; }
        ExperienceGroup ExpGroup { get; }
        MovePool MovePool { get; }
        IReadOnlyList<Ability> AbilityPool { get; }
        int Friendship { get; }
        //TODO: Natures
    }
}
