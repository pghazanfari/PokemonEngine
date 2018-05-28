using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model;
using ModelUnitTests.Moves;

namespace ModelUnitTests.Pokemons
{
    public class Bulbasaur : PokemonEngine.Model.Pokemon
    {
        private const int BaseHP = 45;
        private const int BaseAttack = 49;
        private const int BaseDefense = 49;
        private const int BaseSpecialAttack = 65;
        private const int BaseSpecialDefense = 65;
        private const int BaseSpeed = 45;

        private const int BaseFriendship = 70;

        private static readonly List<PokemonType> BulbasaurTypes = new List<PokemonType> { PokemonType.Grass, PokemonType.Poison };
        private static readonly IStatisticSet BulbasaurStats = new StatisticSet(BaseHP, BaseAttack, BaseDefense, BaseSpecialAttack, BaseSpecialDefense, BaseSpeed);
        private static readonly MovePool BulbasaurMovePool = new MovePool(
            new List<IMove> { Move.Tackle },
            new Dictionary<int, IMove>()
        );
        private static readonly List<Ability> BulbasaurAbilityPool = new List<Ability>();

        private Bulbasaur() : base("Bulbasaur", BulbasaurTypes, BulbasaurStats, ExperienceGroup.Slow, BulbasaurMovePool, BulbasaurAbilityPool, BaseFriendship) { }

        public static readonly Bulbasaur Instance = new Bulbasaur();
    }
}
