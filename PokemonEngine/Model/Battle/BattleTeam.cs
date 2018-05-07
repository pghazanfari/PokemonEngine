using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class BattleTeam
    {
        private readonly IReadOnlyList<BattleSlot> slots;
        public IReadOnlyList<BattleSlot> Slots { get { return slots; } }

        private readonly IReadOnlyList<IBattleParticipant> participants;
        public IReadOnlyList<IBattleParticipant> Participants { get { return participants; } }

        public BattleSlot this[int i] { get { return slots[i]; } }

        public BattleTeam(IList<IBattleParticipant> slotMappings)
        {
            if (slotMappings == null) { throw new Exception("Slot mappings must be a non-null list of IBattleParticipant(s)");  }
            if (slotMappings.Any(x => x == null)){ throw new Exception("A slot cannot be mapped to a 'null' IBattleParticipant"); }

            List<BattleSlot> battleSlots = new List<BattleSlot>(slotMappings.Count);
            Dictionary<IBattleParticipant, int> trainerPokemonCounts = new Dictionary<IBattleParticipant, int>();

            for (int i = 0; i < slotMappings.Count; i++)
            {
                if (!trainerPokemonCounts.ContainsKey(slotMappings[i])) { trainerPokemonCounts[slotMappings[i]] = 0; }
                battleSlots.Add(new BattleSlot(slotMappings[i], i, trainerPokemonCounts[slotMappings[i]]++));
            }
            this.slots = battleSlots.AsReadOnly();
            participants = new List<IBattleParticipant>(trainerPokemonCounts.Keys).AsReadOnly();
        }

        public bool Overlaps(BattleTeam other)
        {
            return participants.Any(x => other.participants.Contains(x));
        }
    }

    public static class BattleTeamImpl
    {
        public static bool AnyOverlaps(this IList<BattleTeam> list)
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
    }
}
