using System.Collections.Generic;
using System.Linq;

using PokemonEngine.Model.Unique;
using PokemonEngine.Model.Battle.Messages;

namespace PokemonEngine.Model.Battle.Weathers
{
    public class Hail : Weather
    {
        public Hail() : base() { }
        public Hail(int turnCount) : base(turnCount) { }

        public override void OnTurnEnd(object sender, EventArgs args)
        {
            InflictHailDamage msg = new InflictHailDamage(args.Battle, this);
            args.Battle.MessageQueue.Enqueue(msg);
        }

        private class InflictHailDamage : InflictDamage.Typed<Weather>
        {
            private IBattle battle;

            public override int this[Slot slot]
            {
                get
                {
                    return (int)(slot.Pokemon.MaxHP() / 16.0f);
                }
            }

            private readonly Weather source;
            public override Weather Source
            {
                get
                {
                    return source;
                }
            }

            public override IEnumerable<Slot> Targets
            {
                get
                {
                    return battle.SelectMany(x => x.Where(y => y.IsInPlay && !y.Pokemon.Types.Contains(PokemonType.Ice)));
                }
            }

            public InflictHailDamage(IBattle battle, Hail hail)
            {
                this.battle = battle;
                source = hail;
            }
        }
    }
}
