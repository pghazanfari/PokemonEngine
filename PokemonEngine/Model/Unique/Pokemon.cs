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
        public const int MinLevel = 1;
        public const int MaxLevel = 100;

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

        public event EventHandler<GainExperienceEventArgs> OnGainExperience;
        public event EventHandler<ExperienceGainedEventArgs> OnExperienceGained;
        public event EventHandler<LevelUpEventArgs> OnLevelUp;
        public event EventHandler<LevelledUpEventArgs> OnLevelledUp;
        public event EventHandler<UpdateFriendshipEventArgs> OnUpdateFriendship;
        public event EventHandler<FriendshipUpdatedEventArgs> OnFriendshipUpdated;
        public event EventHandler<UpdateHPEventArgs> OnUpdateHP;
        public event EventHandler<HPUpdatedEventArgs> OnHPUpdated;

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
            int expNeededForLevelup = ExpGroup.ExperienceNeededForLevel(Level + 1) - Experience;
            if (amount >= expNeededForLevelup)
            {
                int prevExp1 = this.Experience;

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
            int newFriendship = (int)Math.Max(0, Math.Min(MaxFriendship, Friendship + delta));
            int actualDelta = newFriendship - Friendship;

            OnUpdateFriendship?.Invoke(this, new UpdateFriendshipEventArgs(this, actualDelta));
            Friendship += actualDelta;
            OnFriendshipUpdated?.Invoke(this, new FriendshipUpdatedEventArgs(this, prevFriendship));
            return Friendship;
        }

        public int UpdateHP(int delta)
        {
            int prevHP = HP;
            int newHP = Math.Max(MinHP, Math.Min(this[Stat.HP], HP + delta)); // TODO: Allow increasing max hp.
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
                return (int)(baseVal + Level + 10);
            }

            return (int)Math.Floor(Math.Floor(baseVal + 5.0) * Nature.Multiplier(stat)); // TODO: Account for Nature
        }
    }
}
