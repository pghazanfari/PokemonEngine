using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;

namespace PokemonEngine.Battle
{
    public class BattlePokemon : IBattlePokemon
    {
        public readonly IUniquePokemon Base;

        #region Base Pokemon Wrapper Methods
        public string Species { get { return Base.Species; } }
        public IReadOnlyList<PokemonType> Types { get { return Base.Types; } }
        public ExperienceGroup ExpGroup { get { return Base.ExpGroup; } }
        public BaseStats BaseStats { get { return Base.BaseStats; } }
        public MoveCapacity MoveSet { get { return Base.MoveSet; } }
        public IReadOnlyList<Ability> PossibleAbilities { get { return Base.PossibleAbilities; } }
        public int BaseFriendship { get { return Base.BaseFriendship; } }
        
        public Gender Gender { get { return Base.Gender; } }
        public Nature Nature { get { return Base.Nature; } }
        public IVSet IVs { get { return Base.IVs; } }
        public EVSet EVs { get { return Base.EVs; } }
        public int Level { get { return Base.Level; } }
        public int Friendship { get { return Base.Friendship; } }
        public int Experience { get { return Base.Experience; } }

        event EventHandler<ExperienceAddedEventArgs> IUniquePokemon.OnAddExperience
        {
            add
            {
                Base.OnAddExperience += value;
            }
            remove
            {
                Base.OnAddExperience -= value;
            }
        }
        event EventHandler<ExperienceAddedEventArgs> IUniquePokemon.OnExperienceAdded
        {
            add
            {
                Base.OnExperienceAdded += value;
            }
            remove
            {
                Base.OnExperienceAdded -= value;
            }
        }

        event EventHandler<LevelUpEventArgs> IUniquePokemon.OnLevelUp
        {
            add
            {
                Base.OnLevelUp += value;
            }
            remove
            {
                Base.OnLevelUp -= value;
            }
        }
        event EventHandler<LevelUpEventArgs> IUniquePokemon.OnLevelledUp
        {
            add
            {
                Base.OnLevelledUp += value;
            }
            remove
            {
                Base.OnLevelledUp -= value;
            }
        }

        event EventHandler<FriendshipChangedEventArgs> IUniquePokemon.OnFriendshipChange
        {
            add
            {
                Base.OnFriendshipChange += value;
            }
            remove
            {
                Base.OnFriendshipChange -= value;
            }
        }
        event EventHandler<FriendshipChangedEventArgs> IUniquePokemon.OnFriendshipChanged
        {
            add
            {
                Base.OnFriendshipChanged += value;
            }
            remove
            {
                Base.OnFriendshipChanged -= value;
            }
        }

        public int AddExperience(int amount)
        {
            return Base.AddExperience(amount);
        }
        public int LevelUp()
        {
            return Base.LevelUp();
        }
        public int UpdateFriendship(int offset)
        {
            return Base.UpdateFriendship(offset);
        }
        #endregion

        private readonly BattleStats battleStats;
        public BattleStats BattleStats { get {return battleStats; } }

        public BattlePokemon(IUniquePokemon basePokemon)
        {
            Base = basePokemon;
            battleStats = new BattleStats();
        }
    }
}
