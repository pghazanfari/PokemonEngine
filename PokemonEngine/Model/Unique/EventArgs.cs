using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Unique
{
    public class PokemonEventArgs : EventArgs
    {
        public readonly IPokemon Pokemon;
        public PokemonEventArgs(IPokemon pokemon)
        {
            Pokemon = pokemon;
        }
    }

    public class GainExperienceEventArgs : PokemonEventArgs
    {
        public readonly int Experience;
        public GainExperienceEventArgs(IPokemon pokemon, int experience) : base(pokemon)
        {
            Experience = experience;
        }
    }

    public class ExperienceGainedEventArgs : PokemonEventArgs
    {
        public readonly int PreviousExperience;
        public readonly int Amount;
        public ExperienceGainedEventArgs(IPokemon pokemon, int previousExperience) : base(pokemon)
        {
            PreviousExperience = previousExperience;
            Amount = pokemon.Experience - PreviousExperience;
        }
    }

    public class LevelUpEventArgs : PokemonEventArgs
    {
        public LevelUpEventArgs(IPokemon pokemon) : base(pokemon)
        {

        }
    }

    public class LevelledUpEventArgs : PokemonEventArgs
    {
        public LevelledUpEventArgs(IPokemon pokemon) : base(pokemon)
        {
        }
    }

    public class UpdateFriendshipEventArgs : PokemonEventArgs
    {
        public readonly int Amount;
        public UpdateFriendshipEventArgs(IPokemon pokemon, int amount) : base(pokemon)
        {
            Amount = amount;
        }
    }

    public class FriendshipUpdatedEventArgs : PokemonEventArgs
    {
        public readonly int PreviousAmount;
        public readonly int Amount;
        public FriendshipUpdatedEventArgs(IPokemon pokemon, int previousAmount) : base(pokemon)
        {
            PreviousAmount = previousAmount;
            Amount = (int)(pokemon.Friendship - PreviousAmount);
        }
    }

    public class UpdateHPEventArgs : PokemonEventArgs
    {
        public readonly int Amount;
        public UpdateHPEventArgs(IPokemon pokemon, int amount) : base(pokemon)
        {
            Amount = amount;
        }
    }

    public class HPUpdatedEventArgs : PokemonEventArgs
    {
        public readonly int PreviousHP;
        public readonly int Amount;

        public HPUpdatedEventArgs(IPokemon pokemon, int previousHP) : base(pokemon)
        {
            PreviousHP = previousHP;
            Amount = (int)(pokemon.HP - PreviousHP);
        }
    }

}
