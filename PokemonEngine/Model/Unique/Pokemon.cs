using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Events;
using PokemonEngine.Model.Common;

namespace PokemonEngine.Model.Unique
{
    public class Pokemon : IPokemon
    {
        public const int MaxLevel = 100;
        public const int MinLevel = 1;

        public const int MinFriendship = 0;
        public const int MaxFriendship = 255;

        public const int MinHP = 0;

        public readonly Model.IPokemon Base;

        #region Base Pokemon Wrapper Methods
        public string Species { get { return Base.Species; } }
        public IReadOnlyList<PokemonType> Types { get { return Base.Types; } }
        public ExperienceGroup ExpGroup { get { return Base.ExpGroup; } }
        public BaseStats BaseStats { get { return Base.BaseStats; } }
        public Moves PossibleMoves { get { return Base.PossibleMoves; } }
        public IReadOnlyList<Ability> PossibleAbilities { get { return Base.PossibleAbilities; } }
        public int BaseFriendship { get { return Base.BaseFriendship; } }
        #endregion

        event EventHandler<IPokemon, ValueChangeEventArgs> PreExperienceGainEvent;
        event EventHandler<IPokemon, ValueChangeEventArgs> PostExperienceGainEvent;
        event EventHandler<IPokemon, ValueChangeEventArgs> PreLevelUpEvent;
        event EventHandler<IPokemon, ValueChangeEventArgs> PostLevelUpEvent;
        event EventHandler<IPokemon, ValueChangeEventArgs> PreFriendshipChangeEvent;
        event EventHandler<IPokemon, ValueChangeEventArgs> PostFriendshipChangeEvent;
        event EventHandler<IPokemon, ValueChangeEventArgs> PreHPChangeEvent;
        event EventHandler<IPokemon, ValueChangeEventArgs> PostHPChangeEvent;

        #region Interface Events
        event EventHandler<IPokemon, ValueChangeEventArgs> IPokemon.OnExperienceGain
        {
            add
            {
                PreExperienceGainEvent += value;
            }
            remove
            {
                PreExperienceGainEvent -= value;
            }
        }
        event EventHandler<IPokemon, ValueChangeEventArgs> IPokemon.OnExperienceGained
        {
            add
            {
                PostExperienceGainEvent += value;
            }
            remove
            {
                PostExperienceGainEvent -= value;
            }
        }

        event EventHandler<IPokemon, ValueChangeEventArgs> IPokemon.OnLevelUp
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
        event EventHandler<IPokemon, ValueChangeEventArgs> IPokemon.OnLevelledUp
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

        event EventHandler<IPokemon, ValueChangeEventArgs> IPokemon.OnFriendshipChange
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
        event EventHandler<IPokemon, ValueChangeEventArgs> IPokemon.OnFriendshipChanged
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

        event EventHandler<IPokemon, ValueChangeEventArgs> IPokemon.OnHPChange
        {
            add
            {
                PreHPChangeEvent += value;
            }
            remove
            {
                PreHPChangeEvent -= value;
            }
        }
        event EventHandler<IPokemon, ValueChangeEventArgs> IPokemon.OnHPChanged
        {
            add
            {
                PostHPChangeEvent += value;
            }
            remove
            {
                PostHPChangeEvent -= value;
            }
        }
        #endregion

        private readonly string uid;
        public string UID { get { return uid; } }

        private readonly IVSet ivs;
        public IVSet IVs { get { return ivs; } }

        private readonly EVSet evs;
        public EVSet EVs { get { return evs; } }

        private readonly Gender gender;
        public Gender Gender { get { return gender; } }

        private readonly Nature nature;
        public Nature Nature { get { return nature; } }

        private readonly MoveSet<IMove> moves;
        public MoveSet<IMove> Moves { get { return moves; } }

        public int HP { get; private set; }

        public int Friendship { get; private set; }

        public int Level { get; private set; }

        public int Experience { get; private set; }

        public int this[Stat stat] { get { return calculateStat(stat); } }

        public Pokemon(Model.IPokemon basePokemon, string uid, Gender gender, Nature nature, IVSet ivs, EVSet evs, MoveSet<IMove> moves, int friendship, int level)
        {
            Base = basePokemon;
            this.gender = gender;
            this.nature = nature;
            this.uid = uid;
            this.ivs = ivs;
            this.evs = evs;
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
            HP = this[Stat.HP];
        }

        public Pokemon(Model.Pokemon basePokemon, Gender gender, Nature nature, IVSet ivs, EVSet evs, MoveSet<IMove> moves, int friendship, int level) : 
            this(basePokemon, Guid.NewGuid().ToString(), gender, nature, ivs, evs, moves, friendship, level) { }

        public int GainExperience(int amount)
        {
            ValueChangeEventArgs args = new ValueChangeEventArgs(Experience, amount);

            if (amount <= 0)
            {
                throw new Exception("Experience may only be increased by a positive number");
            }

            int expNeededForLevelup = ExpGroup.ExperienceNeededForLevel(Level + 1) - Experience;
            if (amount >= expNeededForLevelup)
            {
                PreExperienceGainEvent?.Invoke(this, args);
                Experience += expNeededForLevelup;
                PostExperienceGainEvent?.Invoke(this, args);
                LevelUp();

                int newAmount = amount - expNeededForLevelup;
                if (newAmount > 0)
                {
                    return GainExperience(amount - newAmount);
                }
                return Experience;
            }

            PreExperienceGainEvent?.Invoke(this, args);
            Experience += amount;
            PostExperienceGainEvent?.Invoke(this, args);
            return Experience;
        }

        public int LevelUp()
        {
            ValueChangeEventArgs args = new ValueChangeEventArgs(Level, 1);
            PreLevelUpEvent?.Invoke(this, args);
            Level += 1;
            Experience = ExpGroup.ExperienceNeededForLevel(Level);
            PostLevelUpEvent?.Invoke(this, args);
            return Level;
        }

        public int ChangeFriendship(int delta)
        {
            int newFriendship = Math.Max(0, Math.Min(MaxFriendship, Friendship + delta));
            ValueChangeEventArgs args = new ValueChangeEventArgs(Friendship, Friendship - newFriendship);

            PreFriendshipChangeEvent?.Invoke(this, args);
            Friendship = newFriendship;
            PostFriendshipChangeEvent?.Invoke(this, args);
            return Friendship;
        }

        public int ChangeHP(int delta)
        {
            int newHP = Math.Max(MinHP, Math.Min(this[Stat.HP], HP + delta)); // TODO: Allow increasing max hp.
            ValueChangeEventArgs args = new ValueChangeEventArgs(HP, newHP - HP);

            PreHPChangeEvent?.Invoke(this, args);
            HP = newHP;
            PostHPChangeEvent?.Invoke(this, args);
            return HP;
        }

        public override bool Equals(object obj)
        {
            if (obj is IPokemon)
            {
                return this.UID.Equals((obj as IPokemon).UID);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return UID.GetHashCode();
        }

        private int calculateStat(Stat stat)
        {
            int baseVal = (int)Math.Floor((2.0 * BaseStats[stat] + IVs[stat] + Math.Floor(EVs[stat] / 4.0)) * Level / 100.0);

            if (stat == Stat.HP)
            {
                return baseVal + Level + 10;
            }

            return (int)Math.Floor(Math.Floor(baseVal + 5.0) * Nature.Multiplier(stat)); // TODO: Account for Nature
        }
    }
}
