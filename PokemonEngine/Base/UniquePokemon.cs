using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class UniquePokemon : IUniquePokemon, IStatsProvider
    {
        public const int MaxLevel = 100;
        public const int MinLevel = 1;
        public const int MaxFriendship = 255;

        public readonly Pokemon Base;

        #region Base Pokemon Wrapper Methods
        public string Species { get { return Base.Species; } }
        public IReadOnlyList<PokemonType> Types { get { return Base.Types; } }
        public ExperienceGroup ExpGroup { get { return Base.ExpGroup; } }
        public BaseStats BaseStats { get { return Base.BaseStats; } }
        public MoveCapacity MoveSet { get { return Base.MoveSet; } }
        public IReadOnlyList<Ability> PossibleAbilities { get { return Base.PossibleAbilities; } }
        public int BaseFriendship { get { return Base.BaseFriendship; } }
        #endregion

        event EventHandler<ExperienceAddedEventArgs> PreAddExperienceEvent;
        event EventHandler<ExperienceAddedEventArgs> PostAddExperienceEvent;
        event EventHandler<LevelUpEventArgs> PreLevelUpEvent;
        event EventHandler<LevelUpEventArgs> PostLevelUpEvent;
        event EventHandler<FriendshipChangedEventArgs> PreFriendshipChangeEvent;
        event EventHandler<FriendshipChangedEventArgs> PostFriendshipChangeEvent;

        #region Interface Events
        event EventHandler<ExperienceAddedEventArgs> IUniquePokemon.OnAddExperience
        {
            add
            {
                PreAddExperienceEvent += value;
            }
            remove
            {
                PreAddExperienceEvent -= value;
            }
        }
        event EventHandler<ExperienceAddedEventArgs> IUniquePokemon.OnExperienceAdded
        {
            add
            {
                PostAddExperienceEvent += value;
            }
            remove
            {
                PostAddExperienceEvent -= value;
            }
        }

        event EventHandler<LevelUpEventArgs> IUniquePokemon.OnLevelUp
        {
            add
            {
                PreLevelUpEvent += value;
            }
            remove
            {
                PreLevelUpEvent -= value;
            }
        }
        event EventHandler<LevelUpEventArgs> IUniquePokemon.OnLevelledUp
        {
            add
            {
                PostLevelUpEvent += value;
            }
            remove
            {
                PostLevelUpEvent -= value;
            }
        }

        event EventHandler<FriendshipChangedEventArgs> IUniquePokemon.OnFriendshipChange
        {
            add
            {
                PreFriendshipChangeEvent += value;
            }
            remove
            {
                PreFriendshipChangeEvent -= value;
            }
        }
        event EventHandler<FriendshipChangedEventArgs> IUniquePokemon.OnFriendshipChanged
        {
            add
            {
                PostFriendshipChangeEvent += value;
            }
            remove
            {
                PostFriendshipChangeEvent -= value;
            }
        } 
        #endregion

        private readonly IVSet ivs;
        public IVSet IVs { get { return ivs; } }

        private readonly EVSet evs;
        public EVSet EVs { get { return evs; } }

        private readonly Gender gender;
        public Gender Gender { get { return gender; } }

        private readonly MoveSet moves;
        public MoveSet Moves { get { return moves; } }

        public int Friendship { get; private set; }

        public int Level { get; private set; }

        public int Experience { get; private set; }

        public int this[PStat stat] { get { return calculateStat(stat); } }

        public UniquePokemon(Pokemon basePokemon, IVSet ivs, EVSet evs, Gender gender, MoveSet moves, int friendship, int level)
        {
            Base = basePokemon;
            this.ivs = ivs;
            this.evs = evs;
            this.gender = gender;
            this.moves = moves;
            Friendship = friendship;
            Experience = ExpGroup.ExperienceNeededForLevel(level);

            if (friendship < basePokemon.BaseFriendship)
            {
                throw new Exception($"Friendship {friendship} cannot be lower than base friendship {basePokemon.BaseFriendship}");
            }

            if (level < MinLevel || level > MaxLevel)
            {
                throw new Exception($"Level ({level}) must be between {MinLevel} and {MaxLevel} (inclusive)");
            }
            Level = level;
        }

        public int AddExperience(int amount)
        {
            ExperienceAddedEventArgs args = new ExperienceAddedEventArgs(this, Experience, amount);

            if (amount <= 0)
            {
                throw new Exception("Experience may only be increased by a positive number");
            }

            int expNeededForLevelup = ExpGroup.ExperienceNeededForLevel(Level + 1) - Experience;
            if (amount >= expNeededForLevelup)
            {
                PreAddExperienceEvent?.Invoke(this, args);
                Experience += expNeededForLevelup;
                PostAddExperienceEvent?.Invoke(this, args);
                LevelUp();

                int newAmount = amount - expNeededForLevelup;
                if (newAmount > 0)
                {
                    return AddExperience(amount - newAmount);
                }
                return Experience;
            }

            PreAddExperienceEvent?.Invoke(this, args);
            Experience += amount;
            PostAddExperienceEvent?.Invoke(this, args);
            return Experience;
        }

        public int LevelUp()
        {
            LevelUpEventArgs args = new LevelUpEventArgs(this, Level, Level + 1);
            PreLevelUpEvent?.Invoke(this, args);
            Level += 1;
            Experience = ExpGroup.ExperienceNeededForLevel(Level);
            PostLevelUpEvent?.Invoke(this, args);
            return Level;
        }

        public int UpdateFriendship(int offset)
        {
            FriendshipChangedEventArgs args = new FriendshipChangedEventArgs(this, Friendship, Friendship + offset, offset);

            PreFriendshipChangeEvent?.Invoke(this, args);
            Friendship += offset;
            PostFriendshipChangeEvent?.Invoke(this, args);
            return Friendship;
        }

        private int calculateStat(PStat stat)
        {
            int baseVal = (int)Math.Floor((2.0 * BaseStats[stat] + IVs[stat] + Math.Floor(EVs[stat] / 4.0)) / 100.0);

            if (stat == PStat.HP)
            {
                return baseVal + Level + 10;
            }

            return (int)Math.Floor(baseVal * 1.0); // TODO: Account for Nature
        }
    }
}
