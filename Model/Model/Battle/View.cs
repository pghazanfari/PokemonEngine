namespace PokemonEngine.Model.Battle
{
    public class View
    {
        public IBattle Battle { get; }

        // Probably want a better name for this.
        public IParticipant Owner { get { return Team[Slot].Participant; } }
        public Team Team { get; }
        public int Slot { get; }

        public IPokemon Pokemon { get { return Team[Slot].Pokemon; } }

        public View(IBattle battle, Team team, int slot)
        {
            Battle = battle;
            Team = team;
            Slot = slot;
        }
    }
}
