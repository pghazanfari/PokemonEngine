using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model;
using PokemonEngine.Model.Battle;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace ModelUnitTests.MoveImpl
{
    public class Growl : PokemonEngine.Model.Move
    {
        private const string name = "Growl";
        private static readonly PokemonType type = PokemonType.Normal;
        private static readonly int? power = null;
        private static readonly PokemonEngine.Model.DamageType? damageType = null;
        private const MoveTarget moveTarget = MoveTarget.AnyFoe;
        private const int basePP = 40;
        private const int maxPP = 64;

        private Growl() : base(name, type, power, damageType, moveTarget, basePP, maxPP) { }

        public override void Use(IBattle battle, UseMove useMoveAction)
        {
            ShiftStatStage shift = new ShiftStatStage(useMoveAction.Targets[0].Pokemon, PokemonEngine.Model.Battle.Statistic.Attack, -1);
            battle.MessageQueue.AddFirst(shift);
        }

        public static readonly Growl Instance = new Growl();
    }
}
