using PokemonEngine.Model.Battle;
using PokemonEngine.Model.Battle.Actions;
using PokemonEngine.Model.Battle.Messages;
using PokemonEngine.Model.Common;

namespace PokemonEngine.Model
{
    public class Move : IMove
    {
        public const int DefaultPriority = 0;
        public const int DefaultCriticalHitStage = 0;
        public string Name { get; }
        public PokemonType Type { get; }
        public int? Power { get; }
        public DamageType? DamageType { get; }
        public int Accuracy { get; }
        public MoveTarget Target { get; }
        public int BasePP { get; }
        public int MaxPPLimit { get; }
        public int Priority { get; }
        public int CriticalHitStage { get; }


        public Move(string name, PokemonType type, int? power, DamageType? damageType, int accuracy, MoveTarget target, int basePP, int maxPossiblePP, int priority, int criticalHitStage)
        {
            Name = name;
            Type = type;
            Power = power;
            DamageType = damageType;
            Accuracy = accuracy;
            Target = target;
            BasePP = basePP;
            MaxPPLimit = maxPossiblePP;
            Priority = priority;
            CriticalHitStage = criticalHitStage;
        }

        public Move(string name, PokemonType type, int? power, DamageType? damageType, int accuracy, MoveTarget target, int basePP, int maxPossiblePP) : this(name, type, power, damageType, accuracy, target, basePP, maxPossiblePP, DefaultPriority, DefaultCriticalHitStage)
        { }

        public virtual void Use(IBattle battle, UseMove useMoveAction)
        {
            if (!DamageType.HasValue) { return; }

            Battle.IMove move = useMoveAction.Slot.Pokemon.Moves.Find(this);

            InflictMoveDamage message = new InflictMoveDamage(battle, move, useMoveAction.Slot, useMoveAction.HitTargets);
            battle.MessageQueue.AddFirst(message); // Maybe Enqueue?
            
        }
    }
}
