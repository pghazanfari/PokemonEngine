using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Events;
using PokemonEngine.Model.Common;

namespace PokemonEngine.Model.Unique
{
    public interface IPokemon : Model.IPokemon
    {
        string UID { get; }

        Gender Gender { get; }
        Nature Nature { get; }
        IVSet IVs { get; }
        EVSet EVs { get; }
        int Level { get; }
        int Friendship { get; }
        int Experience { get; }
        int HP { get; }
        MoveSet<IMove> Moves { get; }

        event EventHandler<GainExperienceEventArgs> OnGainExperience;
        event EventHandler<ExperienceGainedEventArgs> OnExperienceGained;
        int GainExperience(int amount);

        event EventHandler<LevelUpEventArgs> OnLevelUp;
        event EventHandler<LevelledUpEventArgs> OnLevelledUp;
        int LevelUp();

        event EventHandler<UpdateFriendshipEventArgs> OnUpdateFriendship;
        event EventHandler<FriendshipUpdatedEventArgs> OnFriendshipUpdated;
        int UpdateFriendship(int delta);

        event EventHandler<UpdateHPEventArgs> OnUpdateHP;
        event EventHandler<HPUpdatedEventArgs> OnHPUpdated;
        int UpdateHP(int delta);
    }
    
    public static class IUniquePokemonImpl
    {
        public static bool HasFainted(this Unique.IPokemon pokemon)
        {
            return pokemon.HP == 0;
        }
    }
}
