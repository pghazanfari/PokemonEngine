using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class Team : IEnumerable<Slot>
    {
        private readonly IReadOnlyList<Slot> slots;
        public IReadOnlyList<Slot> Slots { get { return slots; } }

        private readonly IReadOnlyList<IParticipant> participants;
        public IReadOnlyList<IParticipant> Participants { get { return participants; } }

        public Slot this[int i] { get { return slots[i]; } }

        public Team(IList<IParticipant> slotMappings)
        {
            if (slotMappings == null) { throw new Exception("Slot mappings must be a non-null list of IBattleParticipant(s)");  }
            if (slotMappings.Any(x => x == null)){ throw new Exception("A slot cannot be mapped to a 'null' IBattleParticipant"); }

            List<Slot> battleSlots = new List<Slot>(slotMappings.Count);
            Dictionary<IParticipant, int> trainerPokemonCounts = new Dictionary<IParticipant, int>();

            for (int i = 0; i < slotMappings.Count; i++)
            {
                if (!trainerPokemonCounts.ContainsKey(slotMappings[i])) { trainerPokemonCounts[slotMappings[i]] = 0; }
                battleSlots.Add(new Slot(slotMappings[i], i, trainerPokemonCounts[slotMappings[i]]++));
            }
            this.slots = battleSlots.AsReadOnly();
            participants = new List<IParticipant>(trainerPokemonCounts.Keys).AsReadOnly();
        }

        public bool Overlaps(Team other)
        {
            return participants.Any(x => other.participants.Contains(x));
        }

        public IEnumerator<Slot> GetEnumerator()
        {
            return slots.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return slots.GetEnumerator();
        }
    }

    public static class BattleTeamImpl
    {
        public static bool AnyOverlaps(this IList<Team> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i].Overlaps(list[j])) { return true; }
                }
            }
            return false;
        }
        
        public static bool HasLost(this Team team)
        {
            return team.Participants.All(x => x.HasLost());
        }
    }
}
