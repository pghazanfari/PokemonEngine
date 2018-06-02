using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model;

namespace ModelUnitTests.PokemonImpl
{
    public class Bulbasaur : PokemonEngine.Model.Pokemon
    {
        private const string name = "Bulbasaur";
        private const ExperienceGroup experienceGroup = ExperienceGroup.MediumSlow;

        public const int BaseHP = 45;
        public const int BaseAttack = 49;
        public const int BaseDefense = 49;
        public const int BaseSpecialAttack = 65;
        public const int BaseSpecialDefense = 65;
        public const int BaseSpeed = 45;

        public const int BaseFriendship = 70;

        private static readonly List<PokemonType> types = new List<PokemonType> { PokemonType.Grass, PokemonType.Poison };
        private static readonly IStatistics stats = new Statistics(BaseHP, BaseAttack, BaseDefense, BaseSpecialAttack, BaseSpecialDefense, BaseSpeed);
        private static readonly MovePool movePool = new MovePool(
            new List<IMove> { Move.Tackle, Move.Growl },
            new Dictionary<int, IMove>()
        );
        private static readonly List<Ability> abilityPool = new List<Ability>();

        private Bulbasaur() : base(name, types, stats, experienceGroup, movePool, abilityPool, BaseFriendship) { }

        public static readonly Bulbasaur Instance = new Bulbasaur();

        public static PokemonEngine.Model.Unique.IPokemon Construct(string uid, Gender gender, Nature nature, PokemonEngine.Model.Unique.IVSet ivs, PokemonEngine.Model.Unique.EVSet evs, PokemonEngine.Model.Unique.MoveSet<PokemonEngine.Model.Unique.IMove> moves, int friendship, int level)
        {
            return new PokemonEngine.Model.Unique.Pokemon(Instance, uid, gender, nature, ivs, evs, moves, friendship, level);
        }

        public static PokemonEngine.Model.Unique.IPokemon ConstructSimple(int level)
        {
            List<PokemonEngine.Model.Unique.IMove> moves = new List<PokemonEngine.Model.Unique.IMove>();
            for (int i = 0; i < PokemonEngine.Model.Unique.MoveSet<PokemonEngine.Model.Unique.IMove>.MaxNumberOfMoves && i < Instance.MovePool.StarterMoves.Count; i++)
            {
                moves.Add(new PokemonEngine.Model.Unique.Move(Instance.MovePool.StarterMoves[i]));
            }

            return new PokemonEngine.Model.Unique.Pokemon(Instance, Gender.Male, Nature.Adamant, new PokemonEngine.Model.Unique.IVSet(),
                new PokemonEngine.Model.Unique.EVSet(), new PokemonEngine.Model.Unique.MoveSet<PokemonEngine.Model.Unique.IMove>(moves), level);
        }
    }
}
