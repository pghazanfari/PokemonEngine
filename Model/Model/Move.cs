using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model
{
    public class Move : IMove
    {
        public const int DefaultPriority = 0;
        public const int DefaultCriticalHitStage = 0;

        private readonly string name;
        public string Name { get { return name; }  }

        private readonly PokemonType type;
        public PokemonType Type { get { return type; } }

        private readonly int? power;
        public int? Power { get { return power; }  }

        private readonly DamageType? damageType;
        public DamageType? DamageType { get { return damageType; } }

        private MoveTarget target;
        public MoveTarget Target { get { return target; } }

        private readonly int basePP;
        public int BasePP { get { return basePP; } }

        private readonly int maxPossiblePP;
        public int MaxPPLimit { get { return maxPossiblePP; } }

        private readonly int priority;
        public int Priority
        {
            get
            {
                return priority;
            }
        }

        private readonly int criticalHitStage;
        public int CriticalHitStage
        {
            get
            {
                return criticalHitStage;
            }
        }


        public Move(string name, PokemonType type, int? power, DamageType? damageType, MoveTarget target, int basePP, int maxPossiblePP, int priority, int criticalHitStage)
        {
            this.name = name;
            this.type = type;
            this.power = power;
            this.damageType = damageType;
            this.target = target;
            this.basePP = basePP;
            this.maxPossiblePP = maxPossiblePP;
            this.priority = priority;
            this.criticalHitStage = criticalHitStage;
        }

        public Move(string name, PokemonType type, int? power, DamageType? damageType, MoveTarget target, int basePP, int maxPossiblePP) : this(name, type, power, damageType, target, basePP, maxPossiblePP, DefaultPriority, DefaultCriticalHitStage)
        { }

        public virtual void Use(IBattle battle, UseMove useMoveAction)
        {
            if (!damageType.HasValue) { return; }

            InflictMoveDamage message = new InflictMoveDamage(battle, this, useMoveAction.Slot, useMoveAction.Targets);
            battle.MessageQueue.AddFirst(message); // Maybe Enqueue?
        }
    }
}
