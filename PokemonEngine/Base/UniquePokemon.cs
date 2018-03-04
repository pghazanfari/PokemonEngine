using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class UniquePokemon : IUniquePokemon, IPStatsProvider
    {
        public const int MaxLevel = 100;
        public const int MinLevel = 1;
        public const int MaxFriendship = 255;

        public readonly Pokemon Base;

        #region Base Pokemon Wrapper Methods
        public string Species { get { return Base.Species; } }
        public IReadOnlyList<PType> Types { get { return Base.Types; } }
        public PBaseStats BaseStats { get { return Base.BaseStats; } }
        public PMoveCapacity MoveSet { get { return Base.MoveSet; } }
        public IReadOnlyList<PAbility> PossibleAbilities { get { return Base.PossibleAbilities; } }
        public int BaseFriendship { get { return Base.BaseFriendship; } } 
        #endregion

        private readonly PIVSet ivs;
        public PIVSet IVs { get { return ivs; } }

        private readonly PEVSet evs;
        public PEVSet EVs { get { return evs; } }

        private readonly PGender gender;
        public PGender Gender { get { return gender; } }

        private readonly PMoveSet moves;
        public PMoveSet Moves { get { return moves; } }

        public int Friendship { get; private set; }

        public int Level { get; private set; }

        public int this[PStat stat] { get { return calculateStat(stat); } }

        public UniquePokemon(Pokemon basePokemon, PIVSet ivs, PEVSet evs, PGender gender, PMoveSet moves, int friendship, int level)
        {
            Base = basePokemon;
            this.ivs = ivs;
            this.evs = evs;
            this.gender = gender;
            this.moves = moves;
            Friendship = friendship;

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

        public int LevelUp() { return ++Level; }

        public int UpdateFriendship(int offset)
        {
            Friendship += offset;
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
