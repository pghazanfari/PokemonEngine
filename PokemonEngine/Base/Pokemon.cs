using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class Pokemon : IPokemon
    {
        private readonly string species;
        public string Species { get { return species; } }

        private readonly IReadOnlyList<PokemonType> types;
        public IReadOnlyList<PokemonType> Types { get { return types; } }

        private readonly ExperienceGroup expGroup;
        public ExperienceGroup ExpGroup { get { return expGroup; } }

        private readonly MoveCapacity moveSet;
        public MoveCapacity MoveSet { get { return moveSet; } }

        private readonly BaseStats baseStats;
        public BaseStats BaseStats { get { return baseStats; } }

        private readonly IReadOnlyList<Ability> possibleAbilities;
        public IReadOnlyList<Ability> PossibleAbilities { get { return possibleAbilities; } }

        private readonly int baseFriendship;
        public int BaseFriendship { get { return baseFriendship; } }

        public Pokemon(string species, IList<PokemonType> types, MoveCapacity moveSet, BaseStats baseStats, IList<Ability> possibleAbilities, int baseFriendship)
        {
            this.species = species;
            this.types = new List<PokemonType>(types).AsReadOnly();
            this.moveSet = moveSet;
            this.baseStats = baseStats;
            this.possibleAbilities = new List<Ability>(possibleAbilities).AsReadOnly();
            this.baseFriendship = baseFriendship;
        }
    }
}
