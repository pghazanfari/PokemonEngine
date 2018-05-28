using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class View
    {
        public readonly IBattle Battle;

        // Probably want a better name for this.
        public IParticipant Owner { get { return team[slot].Participant; } }

        private readonly Team team;
        public Team Team { get { return team; } }

        private readonly int slot;
        public int Slot { get { return slot; } }

        public IPokemon Pokemon { get { return team[slot].Pokemon; } }

        public View(IBattle battle, Team team, int slot)
        {
            Battle = battle;
            this.team = team;
            this.slot = slot;
        }
    }
}
