using System;

namespace PokemonEngine.Model.Unique
{
    public class PokemonEventArgs : EventArgs
    {
        public IPokemon Pokemon { get; }
        public PokemonEventArgs(IPokemon pokemon)
        {
            Pokemon = pokemon;
        }
    }

    public class GainExperienceEventArgs : PokemonEventArgs
    {
        public int Experience { get; }
        public GainExperienceEventArgs(IPokemon pokemon, int experience) : base(pokemon)
        {
            Experience = experience;
        }
    }

    public class ExperienceGainedEventArgs : PokemonEventArgs
    {
        public int PreviousExperience { get; }
        public int Amount { get; }
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
        public int Amount { get; }
        public UpdateFriendshipEventArgs(IPokemon pokemon, int amount) : base(pokemon)
        {
            Amount = amount;
        }
    }

    public class FriendshipUpdatedEventArgs : PokemonEventArgs
    {
        public int PreviousAmount { get; }
        public int Amount { get; }
        public FriendshipUpdatedEventArgs(IPokemon pokemon, int previousAmount) : base(pokemon)
        {
            PreviousAmount = previousAmount;
            Amount = pokemon.Friendship - PreviousAmount;
        }
    }

    public class UpdateHPEventArgs : PokemonEventArgs
    {
        public int Amount { get; }
        public UpdateHPEventArgs(IPokemon pokemon, int amount) : base(pokemon)
        {
            Amount = amount;
        }
    }

    public class HPUpdatedEventArgs : PokemonEventArgs
    {
        public int PreviousHP { get; }
        public int Amount { get; }

        public HPUpdatedEventArgs(IPokemon pokemon, int previousHP) : base(pokemon)
        {
            PreviousHP = previousHP;
            Amount = pokemon.HP - PreviousHP;
        }
    }

    public class SwapPokemonEventArgs : EventArgs
    {
        public Party Party { get; }
        public int Slot1 { get; }
        public int Slot2 { get; }

        public IPokemon Pokemon1
        {
            get
            {
                return Party[Slot1];
            }
        }


        public IPokemon Pokemon2
        {
            get
            {
                return Party[Slot2];
            }
        }

        public SwapPokemonEventArgs(Party party, int slot1, int slot2) : base()
        {
            Party = party;
            Slot1 = slot1;
            Slot2 = slot2;
        }
    }

    public class PokemonSwappedEventArgs : EventArgs
    {
        public Party Party { get; }
        public int Slot1 { get; }
        public int Slot2 { get; }

        public IPokemon Pokemon1
        {
            get
            {
                return Party[Slot1];
            }
        }


        public IPokemon Pokemon2
        {
            get
            {
                return Party[Slot2];
            }
        }

        public PokemonSwappedEventArgs(Party party, int slot1, int slot2) : base()
        {
            Party = party;
            Slot1 = slot1;
            Slot2 = slot2;
        }
    }
}
