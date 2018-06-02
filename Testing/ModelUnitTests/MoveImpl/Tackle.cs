using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model;

namespace ModelUnitTests.MoveImpl
{
    public class Tackle : PokemonEngine.Model.Move
    {
        private const string name = "Tackle";
        private static readonly PokemonType type = PokemonType.Normal;
        private const int power = 40;
        private const PokemonEngine.Model.DamageType damageType = PokemonEngine.Model.DamageType.Physical;
        private const MoveTarget moveTarget = MoveTarget.AnyFoe;
        private const int basePP = 35;
        private const int maxPP = 56;

        private Tackle() : base(name, type, power, damageType, moveTarget, basePP, maxPP) { }

        public static readonly Tackle Instance = new Tackle();
    }
}
