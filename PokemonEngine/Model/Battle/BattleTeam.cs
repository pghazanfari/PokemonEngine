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
        private readonly IReadOnlyList<IBattleParticipant> slotMappings;
        private readonly IReadOnlyList<int> slotIndexMappings;

        public int SlotCount { get { return slotMappings.Count; } }

        private readonly IReadOnlyList<IBattleParticipant> participants;
        public IReadOnlyList<IBattleParticipant> Participants { get { return participants; } }

        public IReadOnlyList<IBattleParticipant> SlotMappings { get { return slotMappings; } }

        private readonly IReadOnlyList<IBattlePokemon> pokemon;
        public IReadOnlyList<IBattlePokemon> Pokemon { get { return pokemon; } }

        public BattleTeam(IList<IBattleParticipant> slotMappings)
        {
            if (slotMappings == null) { throw new Exception("Slot mappings must be a non-null list of IBattleParticipant(s)");  }
            if (slotMappings.Any(x => x == null)){ throw new Exception("A slot cannot be mapped to a 'null' IBattleParticipant"); }

            Dictionary<IBattleParticipant, int> trainerPokemonCounts = new Dictionary<IBattleParticipant, int>();
            List<int> slotIndexMap = new List<int>(slotMappings.Count);
            foreach (IBattleParticipant trainer in slotMappings) 
            {
                if (!trainerPokemonCounts.ContainsKey(trainer)) { trainerPokemonCounts[trainer] = 0; }
                slotIndexMap.Add(trainerPokemonCounts[trainer]);
                trainerPokemonCounts[trainer]++;
            }
            slotIndexMappings = slotIndexMap.AsReadOnly();

            this.slotMappings = new List<IBattleParticipant>(slotMappings).AsReadOnly();
            participants = this.slotMappings.Distinct().ToList().AsReadOnly();

            pokemon = new BattlePokemonSlotMappingList(this.slotMappings, slotIndexMappings);
        }

        public IBattlePokemon PokemonAt(int i)
        {
            return Pokemon[i];
        }

        public IBattleParticipant ParticipantAt(int i)
        {
            return slotMappings[i];
        }

        public bool Overlaps(BattleTeam other)
        {
            return participants.Any(x => other.participants.Contains(x));
        }

        public bool Contains(IBattleParticipant participant)
        {
            return participants.Contains(participant);
        }

        private class BattlePokemonSlotMappingList : IReadOnlyList<IBattlePokemon>
        {
            private IReadOnlyList<IBattleParticipant> slotMappings;
            private IReadOnlyList<int> slotIndexMappings;

            public BattlePokemonSlotMappingList(IReadOnlyList<IBattleParticipant> slotMappings, IReadOnlyList<int> slotIndexMappings)
            {
                this.slotMappings = slotMappings;
                this.slotIndexMappings = slotIndexMappings;
            }

            public IBattlePokemon this[int index]
            {
                get
                {
                    return slotMappings[index].BattlingPokemon[slotIndexMappings[index]];
                }
            }

            public int Count
            {
                get
                {
                    return slotMappings.Count;
                }
            }

            public IEnumerator<IBattlePokemon> GetEnumerator()
            {
                throw new NotImplementedException(); //TODO
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException(); //TODO
            }
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
