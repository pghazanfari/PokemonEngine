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

        event EventHandler<IPokemon, ValueChangeEventArgs> OnExperienceGain;
        event EventHandler<IPokemon, ValueChangeEventArgs> OnExperienceGained;
        int GainExperience(int amount);

        event EventHandler<IPokemon, ValueChangeEventArgs> OnLevelUp;
        event EventHandler<IPokemon, ValueChangeEventArgs> OnLevelledUp;
        int LevelUp();

        event EventHandler<IPokemon, ValueChangeEventArgs> OnFriendshipChange;
        event EventHandler<IPokemon, ValueChangeEventArgs> OnFriendshipChanged;
        int ChangeFriendship(int delta);

        event EventHandler<IPokemon, ValueChangeEventArgs> OnHPChange;
        event EventHandler<IPokemon, ValueChangeEventArgs> OnHPChanged;
        int ChangeHP(int delta);
    }
    
    public static class IUniquePokemonImpl
    {
        public static bool HasFainted(this IPokemon pokemon)
        {
            return pokemon.HP == 0;
        }
    }
}
