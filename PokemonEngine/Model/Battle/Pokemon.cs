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

        event EventHandler<Unique.IPokemon, ValueChangeEventArgs> Unique.IPokemon.OnExperienceGain
        {
            add
            {
                Base.OnExperienceGain += value;
            }
            remove
            {
                Base.OnExperienceGain -= value;
            }
        }
        event EventHandler<Unique.IPokemon, ValueChangeEventArgs> Unique.IPokemon.OnExperienceGained
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

        event EventHandler<Unique.IPokemon, ValueChangeEventArgs> Unique.IPokemon.OnLevelUp
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
        event EventHandler<Unique.IPokemon, ValueChangeEventArgs> Unique.IPokemon.OnLevelledUp
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

        event EventHandler<Unique.IPokemon, ValueChangeEventArgs> Unique.IPokemon.OnFriendshipChange
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
        event EventHandler<Unique.IPokemon, ValueChangeEventArgs> Unique.IPokemon.OnFriendshipChanged
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

        event EventHandler<Unique.IPokemon, ValueChangeEventArgs> Unique.IPokemon.OnHPChange
        {
            add
            {
                Base.OnHPChange += value;
            }
            remove
            {
                Base.OnHPChange -= value;
            }
        }
        event EventHandler<Unique.IPokemon, ValueChangeEventArgs> Unique.IPokemon.OnHPChanged
        {
            add
            {
                Base.OnHPChanged += value;
            }
            remove
            {
                Base.OnHPChanged -= value;
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
        public int ChangeFriendship(int delta)
        {
            return Base.ChangeFriendship(delta);
        }
        public int ChangeHP(int delta)
        {
            return Base.ChangeHP(delta);
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
