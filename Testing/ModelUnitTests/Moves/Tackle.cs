using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model;

namespace ModelUnitTests.Moves
{
    public class MTackle : PokemonEngine.Model.Move
    {
        private const string TackleName = "Tackle";
        private static readonly PokemonType TackleType = PokemonType.Normal;
        private const int TacklePower = 40;
        private const PokemonEngine.Model.DamageType TackleDamageType = PokemonEngine.Model.DamageType.Physical;
        private const MoveTarget TackleMoveTarget = MoveTarget.AnyFoe;
        private const int TackleBasePP = 35;
        private const int TackleMaxPP = 56;

        private MTackle() : base("Tackle", PokemonType.Normal, TacklePower, TackleDamageType, TackleMoveTarget, TackleBasePP, TackleMaxPP) { }

        public static readonly MTackle Instance = new MTackle();
    }
}
