using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PokemonEngine.Model.Events;
using PokemonEngine.Model.Common;
using PokemonEngine.Model.Unique;

namespace PokemonEngine.Model.Battle
{
    public class Pokemon : Unique.IPokemon
    {
        public readonly Unique.IPokemon Base;

        #region Base Pokemon Wrapper Methods
        public string Species { get { return Base.Species; } }
        public IReadOnlyList<PokemonType> Types { get { return Base.Types; } }
        public ExperienceGroup ExpGroup { get { return Base.ExpGroup; } }
        public BaseStats BaseStats { get { return Base.BaseStats; } }
        public Moves PossibleMoves { get { return Base.PossibleMoves; } }
        public IReadOnlyList<Ability> PossibleAbilities { get { return Base.PossibleAbilities; } }
        public int BaseFriendship { get { return Base.BaseFriendship; } }
        
        public Gender Gender { get { return Base.Gender; } }
        public Nature Nature { get { return Base.Nature; } }
        public string UID {  get { return Base.UID; } }
        public IVSet IVs { get { return Base.IVs; } }
        public EVSet EVs { get { return Base.EVs; } }
        public int Level { get { return Base.Level; } }
        public int Friendship { get { return Base.Friendship; } }
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

        private readonly Stats battleStats;
        public Stats BattleStats { get { return battleStats; } }

        private readonly MoveSet<IMove> battleMoves;
        public MoveSet<IMove> BattleMoves { get { return battleMoves; } }

        public int this[Model.Stat stat]
        {
            get
            {
                return Base[stat];
            }
        }

        public Pokemon(Unique.IPokemon basePokemon)
        {
            Base = basePokemon;
            battleStats = new Stats();

            IList<IMove> list = new List<IMove>(Base.Moves.Moves.Count);
            foreach (Unique.IMove move in Base.Moves.Moves) {
                if (move == null) { list.Add(null); continue; }
                list.Add(new Move(move));
            }
            battleMoves = new MoveSet<IMove>(list);
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
