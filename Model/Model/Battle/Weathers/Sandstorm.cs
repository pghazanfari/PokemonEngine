using System.Collections.Generic;
using System.Linq;

using PokemonEngine.Model.Common;
using PokemonEngine.Model.Unique;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle.Weathers
{
    public class Sandstorm : Weather
    {
        private class SandstormModifier : IModifier
        {
            private readonly Sandstorm owner;

            public float Factor
            {
                get
                {
                    return SpecialDefenseModifierFactor;
                }
            }

            public SandstormModifier(Sandstorm owner)
            {
                this.owner = owner;
            }

            public void Dispose()
            {
                IEnumerable<KeyValuePair<IPokemon, IModifier>> enumerable = owner.modifiers.Where(x => x.Value == this);
                if (enumerable.Any())
                {
                    owner.modifiers.Remove(enumerable.First().Key);
                }
            }
        }

        public const int SpecialDefenseModifierLevel = 0;
        public const float SpecialDefenseModifierFactor = 1.5f;
        public static readonly PokemonType BuffedType = PokemonType.Rock;

        private readonly IDictionary<IPokemon, IModifier> modifiers;

        public Sandstorm() : base()
        {
            modifiers = new Dictionary<IPokemon, IModifier>();
        }
        public Sandstorm(int turnCount) : base(turnCount)
        {
            modifiers = new Dictionary<IPokemon, IModifier>();
        }

        public override void OnTurnEnd(object sender, EventArgs args)
        {
            InflictSandstormDamage msg = new InflictSandstormDamage(args.Battle, this);
            args.Battle.MessageQueue.Enqueue(msg);
        }

        public override void OnBattleStart(object sender, EventArgs args)
        {
            foreach (Team team in args.Battle)
            {
                foreach (Slot slot in team)
                {
                    if (slot.Pokemon.Types.Contains(BuffedType))
                    {
                        IModifier modifier = new SandstormModifier(this);
                        slot.Pokemon.Stats.Modifiers[Statistic.SpecialDefense].AddModifier(SpecialDefenseModifierLevel, modifier);
                        modifiers.Add(slot.Pokemon, modifier);
                    }
                }
            }
        }

        public override void OnPokemonSwapped(object sender, PokemonSwappedEventArgs args)
        {
            if (modifiers.ContainsKey(args.SwappedPokemon))
            {
                modifiers[args.SwappedPokemon].Dispose();
                modifiers.Remove(args.SwappedPokemon);
            }
            
            if (args.Action.Slot.Pokemon.Types.Contains(BuffedType))
            {
                IModifier modifier = new SandstormModifier(this);
                args.Action.Slot.Pokemon.Stats.Modifiers[Statistic.SpecialDefense].AddModifier(SpecialDefenseModifierLevel, modifier);
                modifiers.Add(args.Action.Slot.Pokemon, modifier);
            }
        }

        public override void OnChangeWeather(object sender, ChangeWeatherEventArgs args)
        {
            if (args.Battle.CurrentWeather == this)
            {
                modifiers.Values.ForEach(x => x.Dispose());
                modifiers.Clear();
            }
        }

        private class InflictSandstormDamage : InflictDamage.Typed<Weather>
        {
            readonly IReadOnlyList<PokemonType> ProtectedTypes = new List<PokemonType> {
            PokemonType.Rock, PokemonType.Ground, PokemonType.Steel }.AsReadOnly();

            private IBattle battle;

            public override int this[Slot slot]
            {
                get
                {
                    return (int)(slot.Pokemon.MaxHP() / 16.0f);
                }
            }

            public override Weather Source { get; }

            public override IEnumerable<Slot> Targets
            {
                get
                {
                    return battle.SelectMany(x => x.Where(y => y.IsInPlay && !y.Pokemon.Types.Intersect(ProtectedTypes).Any()));
                }
            }

            public InflictSandstormDamage(IBattle battle, Sandstorm sandstorm)
            {
                this.battle = battle;
                Source = sandstorm;
            }
        }
    }
}
