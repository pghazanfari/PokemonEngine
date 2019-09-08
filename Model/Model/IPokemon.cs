using System.Collections.Generic;

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
