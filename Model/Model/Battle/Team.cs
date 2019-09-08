using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokemonEngine.Model.Battle
{
    public class Team : IEnumerable<Slot>
    {
        public IReadOnlyList<Slot> Slots { get; }
        public IReadOnlyList<IParticipant> Participants { get; }

        public Slot this[int i] { get { return Slots[i]; } }

        public Team(params IParticipant[] slotMappings) : this(new List<IParticipant>(slotMappings)) { }

        public Team(IList<IParticipant> slotMappings)
        {
            if (slotMappings == null) { throw new Exception("Slot mappings must be a non-null list of IBattleParticipant(s)");  }
            if (slotMappings.Any(x => x == null)){ throw new Exception("A slot cannot be mapped to a 'null' IBattleParticipant"); }

            List<Slot> battleSlots = new List<Slot>(slotMappings.Count);
            Dictionary<IParticipant, int> trainerPokemonCounts = new Dictionary<IParticipant, int>();

            for (int i = 0; i < slotMappings.Count; i++)
            {
                if (!trainerPokemonCounts.ContainsKey(slotMappings[i])) { trainerPokemonCounts[slotMappings[i]] = 0; }
                battleSlots.Add(new Slot(this, slotMappings[i], i, trainerPokemonCounts[slotMappings[i]]++));
            }
            Slots = battleSlots.AsReadOnly();
            Participants = new List<IParticipant>(trainerPokemonCounts.Keys).AsReadOnly();
        }

        public bool Overlaps(Team other)
        {
            return Participants.Any(x => other.Participants.Contains(x));
        }

        public IEnumerator<Slot> GetEnumerator()
        {
            return Slots.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Slots.GetEnumerator();
        }
    }

    public static class BattleTeamImpl
    {
        public static bool AnyOverlaps(this IEnumerable<Team> enumerable)
        {
            List<Team> list = new List<Team>(enumerable);
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
