using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class BattleSlot
    {
        private readonly IBattleParticipant participant;
        public IBattleParticipant Participant { get { return participant; } }

        private readonly int slot;
        public int Slot { get { return slot; } }

        private readonly int index;
        public int Index { get { return index; } }

        public IBattlePokemon Pokemon { get { return participant.BattlingPokemon[index];  } }

        public BattleSlot(IBattleParticipant participant, int slot, int index)
        {
            this.participant = participant;
            this.slot = slot;
            this.index = index;
        }
    }
}
