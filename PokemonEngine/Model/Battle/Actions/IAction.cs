using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public abstract class IAction : IMessage// TODO: , IComparable<IBattleAction>
    {
        private readonly Team team;
        public Team Team { get { return team; } }

        private readonly Slot slot;
        public Slot Slot { get { return slot; } }

        public IAction(Team team, Slot slot)
        {
            if (!team.Contains(slot)) { throw new ArgumentException("This slot provided is not part of the given team", "slot"); }
            this.team = team;
            this.slot = slot;
        }

        public abstract void Dispatch(ISubscriber receiver);
    }
}
