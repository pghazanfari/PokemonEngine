using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class BattleView : IBattle
    {
        public readonly IBattle Base;

        #region Base Wrapper Methods
        public IReadOnlyList<BattleTeam> Teams { get { return Base.Teams; } } 
        #endregion

        // Probably want a better name for this.
        private readonly IBattleParticipant owner;
        public IBattleParticipant Owner { get { return owner; } }

        private readonly BattleTeam team;
        public BattleTeam Team { get { return team; } }

        private readonly int slot;
        public int Slot { get { return slot; } }

        public IBattlePokemon Pokemon { get { return team.PokemonAt(slot); } }

        public BattleView(IBattle battle, IBattleParticipant owner, int slot)
        {
            //TODO: Verify owner is in the battle
            Base = battle;
            this.owner = owner;
            this.slot = slot;
            foreach (BattleTeam team in battle.Teams)
            {
                if (team.Contains(owner))
                {
                    this.team = team;
                    break;
                }
            }
        }
    }
}
