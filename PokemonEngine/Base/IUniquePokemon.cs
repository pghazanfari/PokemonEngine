using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public interface IUniquePokemon : IPokemon
    {
        Gender Gender { get; }
        Nature Nature { get; }
        IVSet IVs { get; }
        EVSet EVs { get; }
        int Level { get; }
        int Friendship { get; }
        int Experience { get; }

        event EventHandler<ExperienceAddedEventArgs> OnAddExperience;
        event EventHandler<ExperienceAddedEventArgs> OnExperienceAdded;
        int AddExperience(int amount);

        event EventHandler<LevelUpEventArgs> OnLevelUp;
        event EventHandler<LevelUpEventArgs> OnLevelledUp;
        int LevelUp();

        event EventHandler<FriendshipChangedEventArgs> OnFriendshipChange;
        event EventHandler<FriendshipChangedEventArgs> OnFriendshipChanged;
        int UpdateFriendship(int offset);
    }

    public class LevelUpEventArgs : EventArgs
    {
        public readonly IUniquePokemon Pokemon;
        public readonly int FromLevel;
        public readonly int ToLevel;
        public LevelUpEventArgs(IUniquePokemon pokemon, int fromLevel, int toLevel) : base()
        {
            this.Pokemon = pokemon;
            FromLevel = fromLevel;
            ToLevel = toLevel;
        }
    }

    public class FriendshipChangedEventArgs : EventArgs
    {
        public readonly IUniquePokemon Pokemon;
        public readonly int FromFriendship;
        public readonly int ToFriendship;
        public readonly int Offset;

        public FriendshipChangedEventArgs(IUniquePokemon pokemon, int fromFriendship, int toFriendship, int offset) : base()
        {
            this.Pokemon = pokemon;
            FromFriendship = fromFriendship;
            ToFriendship = toFriendship;
            Offset = offset;
        }
    }

    public class ExperienceAddedEventArgs : EventArgs
    {
        public readonly IUniquePokemon Pokemon;
        public readonly int Experience;
        public readonly int AmountToAdd;

        public ExperienceAddedEventArgs(IUniquePokemon pokemon, int experience, int amountToAdd)
        {
            Pokemon = pokemon;
            Experience = experience;
            AmountToAdd = amountToAdd;
        }
    }
}
