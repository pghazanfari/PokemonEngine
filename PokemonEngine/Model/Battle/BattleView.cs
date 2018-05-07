using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class BattleView
    {
        public readonly IBattle Battle;

        // Probably want a better name for this.
        public IBattleParticipant Owner { get { return team[slot].Participant; } }

        private readonly BattleTeam team;
        public BattleTeam Team { get { return team; } }

        private readonly int slot;
        public int Slot { get { return slot; } }

        public IBattlePokemon Pokemon { get { return team[slot].Pokemon; } }

        public BattleView(IBattle battle, BattleTeam team, int slot)
        {
            Battle = battle;
            this.team = team;
            this.slot = slot;
        }
    }
}
