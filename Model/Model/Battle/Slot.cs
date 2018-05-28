using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Unique;

namespace PokemonEngine.Model.Battle
{
    public class Slot
    {
        private readonly Team team;
        public Team Team { get { return team; } }

        private readonly IParticipant participant;
        public IParticipant Participant { get { return participant; } }

        private readonly int slotNumber;
        public int SlotNumber { get { return slotNumber; } }

        private readonly int index;
        public int Index { get { return index; } }

        public IPokemon Pokemon { get { return participant.Battlers[index];  } }

        public bool IsInPlay { get { return Pokemon != null && !Pokemon.HasFainted(); } }

        public Slot(Team team, IParticipant participant, int slotNumber, int index)
        {
            this.team = team;
            this.participant = participant;
            this.slotNumber = slotNumber;
            this.index = index;
        }
    }
}
