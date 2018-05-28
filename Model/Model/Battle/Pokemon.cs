using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Unique;

namespace PokemonEngine.Model.Battle
{
    public class Pokemon : IPokemon
    {
        public readonly Unique.IPokemon Base;

        #region Base Pokemon Wrapper Methods
        public string Species { get { return Base.Species; } }
        public IReadOnlyList<PokemonType> Types { get { return Base.Types; } }
        public IStatisticSet BaseStats { get { return Base.BaseStats; } }
        public ExperienceGroup ExpGroup { get { return Base.ExpGroup; } }
        public MovePool MovePool { get { return Base.MovePool; } }
        public IReadOnlyList<Ability> AbilityPool { get { return Base.AbilityPool; } }
        public int Friendship { get { return Base.Friendship; } }
        
        public Gender Gender { get { return Base.Gender; } }
        public Nature Nature { get { return Base.Nature; } }
        public string UID {  get { return Base.UID; } }
        IStatisticSet Unique.IPokemon.Stats { get { return Base.Stats; } }
        public IVSet IVs { get { return Base.IVs; } }
        public EVSet EVs { get { return Base.EVs; } }
        public int Level { get { return Base.Level; } }
        public int Experience { get { return Base.Experience; } }
        public int HP { get { return Base.HP; } }
        public MoveSet<Unique.IMove> Moves { get { return Base.Moves; } }

        event EventHandler<GainExperienceEventArgs> Unique.IPokemon.OnGainExperience
        {
            add
            {
                Base.OnGainExperience += value;
            }
            remove
            {
                Base.OnGainExperience -= value;
            }
        }
        event EventHandler<ExperienceGainedEventArgs> Unique.IPokemon.OnExperienceGained
        {
            add
            {
                Base.OnExperienceGained += value;
            }
            remove
            {
                Base.OnExperienceGained -= value;
            }
        }

        event EventHandler<LevelUpEventArgs> Unique.IPokemon.OnLevelUp
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
        event EventHandler<LevelledUpEventArgs> Unique.IPokemon.OnLevelledUp
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

        event EventHandler<UpdateFriendshipEventArgs> Unique.IPokemon.OnUpdateFriendship
        {
            add
            {
                Base.OnUpdateFriendship += value;
            }
            remove
            {
                Base.OnUpdateFriendship -= value;
            }
        }
        event EventHandler<FriendshipUpdatedEventArgs> Unique.IPokemon.OnFriendshipUpdated
        {
            add
            {
                Base.OnFriendshipUpdated += value;
            }
            remove
            {
                Base.OnFriendshipUpdated -= value;
            }
        }

        event EventHandler<UpdateHPEventArgs> Unique.IPokemon.OnUpdateHP
        {
            add
            {
                Base.OnUpdateHP += value;
            }
            remove
            {
                Base.OnUpdateHP -= value;
            }
        }
        event EventHandler<HPUpdatedEventArgs> Unique.IPokemon.OnHPUpdated
        {
            add
            {
                Base.OnHPUpdated += value;
            }
            remove
            {
                Base.OnHPUpdated -= value;
            }
        }

        public int GainExperience(int amount)
        {
            return Base.GainExperience(amount);
        }
        public int LevelUp()
        {
            return Base.LevelUp();
        }
        public int UpdateFriendship(int delta)
        {
            return Base.UpdateFriendship(delta);
        }
        public int UpdateHP(int delta)
        {
            return Base.UpdateHP(delta);
        }
        #endregion

        private readonly StatisticSet stats;
        public StatisticSet Stats { get { return stats; } }

        private readonly MoveSet<IMove> moves;
        MoveSet<IMove> IPokemon.Moves
        {
            get
            {
                return moves;
            }
        }

        public Pokemon(Unique.IPokemon basePokemon)
        {
            Base = basePokemon;
            stats = new StatisticSet(this);

            IList<IMove> list = new List<IMove>(Base.Moves.Count);
            foreach (Unique.IMove move in Base.Moves) {
                if (move == null) { list.Add(null); continue; }
                list.Add(new Move(move));
            }
            moves = new MoveSet<IMove>(list);
        }

        public override bool Equals(object obj)
        {
            if (obj is Unique.IPokemon)
            {
                this.UID.Equals((obj as Unique.IPokemon).UID);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return UID.GetHashCode();
        }
    }
}
