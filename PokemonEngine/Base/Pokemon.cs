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

        private readonly IReadOnlyList<PType> types;
        public IReadOnlyList<PType> Types { get { return types; } }

        private readonly PMoveSet moveSet;
        public PMoveSet MoveSet { get { return moveSet; } }

        private readonly PBaseStats baseStats;
        public PBaseStats BaseStats { get { return baseStats; } }

        private readonly IReadOnlyList<PAbility> possibleAbilities;
        public IReadOnlyList<PAbility> PossibleAbilities { get { return possibleAbilities; } }

        private readonly int baseFriendship;
        public int BaseFriendship { get { return baseFriendship; } }

        public Pokemon(string species, IList<PType> types, PMoveSet moveSet, PBaseStats baseStats, IList<PAbility> possibleAbilities, int baseFriendship)
        {
            this.species = species;
            this.types = new List<PType>(types).AsReadOnly();
            this.moveSet = moveSet;
            this.baseStats = baseStats;
            this.possibleAbilities = new List<PAbility>(possibleAbilities).AsReadOnly();
            this.baseFriendship = baseFriendship;
        }
    }
}
