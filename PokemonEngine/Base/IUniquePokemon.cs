using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base.Events;

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
        int HP { get; }

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
        public static bool Fainted(this IUniquePokemon pokemon)
        {
            return pokemon.HP == 0;
        }
    }
}
