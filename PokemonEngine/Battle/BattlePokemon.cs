using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;
using PokemonEngine.Base.Events;
using PokemonEngine.Util;

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
        public MoveSet<IUniqueMove> Moves { get { return Base.Moves; } }

        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> IUniquePokemon.OnExperienceGain
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
        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> IUniquePokemon.OnExperienceGained
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

        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> IUniquePokemon.OnLevelUp
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
        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> IUniquePokemon.OnLevelledUp
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

        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> IUniquePokemon.OnFriendshipChange
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
        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> IUniquePokemon.OnFriendshipChanged
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

        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> IUniquePokemon.OnHPChange
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
        event PokemonEventHandler<IUniquePokemon, ValueChangeEventArgs> IUniquePokemon.OnHPChanged
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

        private readonly BattleStats battleStats;
        public BattleStats BattleStats { get { return battleStats; } }

        private readonly MoveSet<IBattleMove> battleMoves;
        public MoveSet<IBattleMove> BattleMoves { get { return battleMoves; } }

        public BattlePokemon(IUniquePokemon basePokemon)
        {
            Base = basePokemon;
            battleStats = new BattleStats();

            UniqueList<IBattleMove> list = new UniqueList<IBattleMove>(Base.Moves.Moves.Count);
            foreach (IUniqueMove move in Base.Moves.Moves) {
                if (move == null) { list.Add(null); continue; }
                list.Add(new BattleMove(move));
            }
            battleMoves = new MoveSet<IBattleMove>(list);
        }

        public override bool Equals(object obj)
        {
            if (obj is IUniquePokemon)
            {
                this.UID.Equals((obj as IUniquePokemon).UID);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return UID.GetHashCode();
        }
    }
}
