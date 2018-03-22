using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Events;

namespace PokemonEngine.Model
{
    public interface IUniquePokemon : IPokemon
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
        MoveSet<IUniqueMove> Moves { get; }

        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> OnExperienceGain;
        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> OnExperienceGained;
        int GainExperience(int amount);

        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> OnLevelUp;
        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> OnLevelledUp;
        int LevelUp();

        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> OnFriendshipChange;
        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> OnFriendshipChanged;
        int ChangeFriendship(int delta);

        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> OnHPChange;
        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> OnHPChanged;
        int ChangeHP(int delta);
    }
    
    public static class IUniquePokemonImpl
    {
        public static bool HasFainted(this IUniquePokemon pokemon)
        {
            return pokemon.HP == 0;
        }
    }
}
