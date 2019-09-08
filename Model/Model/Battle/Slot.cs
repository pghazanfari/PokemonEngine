namespace PokemonEngine.Model.Battle
{
    public class Slot
    {
        public Team Team { get; }
        public IParticipant Participant { get; }
        public int SlotNumber { get; }
        public int Index { get; }

        public IPokemon Pokemon { get { return Participant.Battlers[Index];  } }

        public bool IsInPlay { get { return Pokemon != null && !Pokemon.HasFainted(); } }

        public Slot(Team team, IParticipant participant, int slotNumber, int index)
        {
            Team = team;
            Participant = participant;
            SlotNumber = slotNumber;
            Index = index;
        }
    }
}
