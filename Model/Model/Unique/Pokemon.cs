using System;
using System.Collections.Generic;

namespace PokemonEngine.Model.Unique
{
    public class Pokemon : IPokemon
    {
        public const int MinLevel = 1;
        public const int MaxLevel = 100;

        public const int MinFriendship = 0;
        public const int MaxFriendship = 255;

        public const int MinHP = 0;

        public Model.Pokemon Base { get; }

        #region Base Pokemon Wrapper Methods
        public string Species { get { return Base.Species; } }
        public IReadOnlyList<PokemonType> Types { get { return Base.Types; } }
        IStatistics Model.IPokemon.Stats { get { return Base.Stats; } }
        public ExperienceGroup ExpGroup { get { return Base.ExpGroup; } }
        public MovePool MovePool { get { return Base.MovePool; } }
        public IReadOnlyList<Ability> AbilityPool { get { return Base.AbilityPool; } }
        #endregion

        public event EventHandler<GainExperienceEventArgs> OnGainExperience;
        public event EventHandler<ExperienceGainedEventArgs> OnExperienceGained;
        public event EventHandler<LevelUpEventArgs> OnLevelUp;
        public event EventHandler<LevelledUpEventArgs> OnLevelledUp;
        public event EventHandler<UpdateFriendshipEventArgs> OnUpdateFriendship;
        public event EventHandler<FriendshipUpdatedEventArgs> OnFriendshipUpdated;
        public event EventHandler<UpdateHPEventArgs> OnUpdateHP;
        public event EventHandler<HPUpdatedEventArgs> OnHPUpdated;
        public string UID { get; }
        public IStatistics Stats { get; }
        public Ability Ability { get; }
        public IVSet IVs { get; }
        public EVSet EVs { get; }
        public Gender Gender { get; }
        public Nature Nature { get; }
        public MoveSet<IMove> Moves { get; }

        public int HP { get; private set; }

        public int Friendship { get; private set; }

        public int Level { get; private set; }

        public int Experience { get; private set; }

        public Pokemon(Model.Pokemon basePokemon, string uid, Gender gender, Nature nature, Ability ability, IVSet ivs, EVSet evs, MoveSet<IMove> moves, int friendship, int level)
        {
            Base = basePokemon;
            Stats = new Statistics(this);
            Gender = gender;
            Nature = nature;
            UID = uid;
            Ability = ability;
            IVs = ivs;
            EVs = evs;
            Moves = moves;
            Friendship = friendship;
            Experience = ExpGroup.ExperienceNeededForLevel(level);

            if (friendship < basePokemon.Friendship)
            {
                throw new Exception($"Friendship {friendship} cannot be lower than base friendship {basePokemon.Friendship}");
            }

            if (level < MinLevel || level > MaxLevel)
            {
                throw new Exception($"Level ({level}) must be between {MinLevel} and {MaxLevel} (inclusive)");
            }
            Level = level;
            HP = Stats[Statistic.HP];
        }

        public Pokemon(Model.Pokemon basePokemon, Gender gender, Nature nature, Ability ability, IVSet ivs, EVSet evs, MoveSet<IMove> moves, int level) : 
            this(basePokemon, Guid.NewGuid().ToString(), gender, nature, ability, ivs, evs, moves, basePokemon.Friendship, level) { }

        public int GainExperience(int amount)
        {
            int expNeededForLevelup = ExpGroup.ExperienceNeededForLevel(Level + 1) - Experience;
            if (amount >= expNeededForLevelup)
            {
                int prevExp1 = Experience;

                OnGainExperience?.Invoke(this, new GainExperienceEventArgs(this, expNeededForLevelup));
                Experience += expNeededForLevelup;
                OnExperienceGained?.Invoke(this, new ExperienceGainedEventArgs(this, prevExp1));
                LevelUp();

                int newAmount = amount - expNeededForLevelup;
                if (newAmount > 0)
                {
                    return GainExperience(amount - newAmount);
                }
                return Experience;
            }

            int prevExp2 = Experience;
            OnGainExperience?.Invoke(this, new GainExperienceEventArgs(this, amount));
            Experience += amount;
            OnExperienceGained?.Invoke(this, new ExperienceGainedEventArgs(this, prevExp2));
            return Experience;
        }

        public int LevelUp()
        {
            OnLevelUp?.Invoke(this, new LevelUpEventArgs(this));
            Level += 1;
            Experience = ExpGroup.ExperienceNeededForLevel(Level);
            OnLevelledUp?.Invoke(this, new LevelledUpEventArgs(this));
            return Level;
        }

        public int UpdateFriendship(int delta)
        {
            int prevFriendship = Friendship;
            int newFriendship = Math.Max(0, Math.Min(MaxFriendship, Friendship + delta));
            int actualDelta = newFriendship - Friendship;

            OnUpdateFriendship?.Invoke(this, new UpdateFriendshipEventArgs(this, actualDelta));
            Friendship += actualDelta;
            OnFriendshipUpdated?.Invoke(this, new FriendshipUpdatedEventArgs(this, prevFriendship));
            return Friendship;
        }

        public int UpdateHP(int delta)
        {
            int prevHP = HP;
            int newHP = Math.Max(MinHP, Math.Min(Stats[Statistic.HP], HP + delta)); // TODO: Allow increasing max hp.
            int actualDelta = newHP - HP;

            OnUpdateHP?.Invoke(this, new UpdateHPEventArgs(this, actualDelta));
            HP = newHP;
            OnHPUpdated?.Invoke(this, new HPUpdatedEventArgs(this, prevHP));
            return HP;
        }

        public override bool Equals(object obj)
        {
            if (obj is IPokemon)
            {
                return UID.Equals((obj as IPokemon).UID);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return UID.GetHashCode();
        }
        
        object ICloneable.Clone()
        {
            return Clone();
        }
        
        IPokemon IPokemon.Clone()
        {
            return Clone();
        }

        public Pokemon Clone()
        {
            return new Pokemon(Base, Gender, Nature, Ability, IVs.Clone() as IVSet, EVs.Clone() as EVSet, Moves.Clone(), Level);
        }
    }
}
