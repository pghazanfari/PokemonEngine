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

        private readonly ExperienceGroup expGroup;
        public ExperienceGroup ExpGroup { get { return expGroup; } }

        private readonly Moves possibleMoves;
        public Moves PossibleMoves { get { return possibleMoves; } }

        private readonly BaseStats baseStats;
        public BaseStats BaseStats { get { return baseStats; } }

        private readonly IReadOnlyList<Ability> possibleAbilities;
        public IReadOnlyList<Ability> PossibleAbilities { get { return possibleAbilities; } }

        //TODO: Possible Natures

        private readonly int baseFriendship;
        public int BaseFriendship { get { return baseFriendship; } }

        public Pokemon(string species, IList<PokemonType> types, ExperienceGroup expGroup, Moves possibleMoves, BaseStats baseStats, IList<Ability> possibleAbilities, int baseFriendship)
        {
            this.species = species;
            this.types = new List<PokemonType>(types).AsReadOnly();
            this.expGroup = expGroup;
            this.possibleMoves = possibleMoves;
            this.baseStats = baseStats;
            this.possibleAbilities = new List<Ability>(possibleAbilities).AsReadOnly();
            this.baseFriendship = baseFriendship;
        }
    }
}
