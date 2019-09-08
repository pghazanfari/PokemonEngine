using System.Collections.Generic;

namespace PokemonEngine.Model
{
    public class Pokemon : IPokemon
    {
        public string Species { get; }
        public IReadOnlyList<PokemonType> Types { get; }
        public IStatistics Stats { get; }
        public ExperienceGroup ExpGroup { get; }
        public MovePool MovePool { get; }
        public IReadOnlyList<Ability> AbilityPool { get; }
        public int Friendship { get; }

        public Pokemon(string species, IList<PokemonType> types, IStatistics stats, ExperienceGroup expGroup, MovePool movePool, IList<Ability> abilityPool, int friendship)
        {
            Species = species;
            Types = new List<PokemonType>(types).AsReadOnly();
            Stats = stats;
            ExpGroup = expGroup;
            MovePool = movePool;
            AbilityPool = new List<Ability>(abilityPool).AsReadOnly();
            Friendship = friendship;
        }
    }
}
