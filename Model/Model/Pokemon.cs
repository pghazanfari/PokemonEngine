using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class Pokemon : IPokemon
    {
        private readonly string species;
        public string Species { get { return species; } }

        private readonly IReadOnlyList<PokemonType> types;
        public IReadOnlyList<PokemonType> Types { get { return types; } }

        private readonly IStatistics stats;
        public IStatistics Stats { get { return stats; } }

        private readonly ExperienceGroup expGroup;
        public ExperienceGroup ExpGroup { get { return expGroup; } }

        private readonly MovePool movePool;
        public MovePool MovePool { get { return movePool; } }

        private readonly IReadOnlyList<Ability> possibleAbilities;
        public IReadOnlyList<Ability> AbilityPool { get { return possibleAbilities; } }

        //TODO: Possible Natures

        private readonly int friendship;
        public int Friendship { get { return friendship; } }

        public Pokemon(string species, IList<PokemonType> types, IStatistics stats, ExperienceGroup expGroup, MovePool movePool, IList<Ability> abilityPool, int friendship)
        {
            this.species = species;
            this.types = new List<PokemonType>(types).AsReadOnly();
            this.stats = stats;
            this.expGroup = expGroup;
            this.movePool = movePool;
            this.possibleAbilities = new List<Ability>(abilityPool).AsReadOnly();
            this.friendship = friendship;
        }
    }
}
