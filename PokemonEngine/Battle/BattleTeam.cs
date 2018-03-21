using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;

namespace PokemonEngine.Battle
{
    public class BattleTeam
    {
        private readonly IReadOnlyList<IBattleTrainer> slotMappings;
        private readonly IReadOnlyList<int> slotIndexMappings;

        public int SlotCount { get { return slotMappings.Count; } }

        private readonly IReadOnlyList<IBattleTrainer> uniqueTrainers;
        public IReadOnlyList<IBattleTrainer> UniqueTrainers { get { return uniqueTrainers; } }

        public IReadOnlyList<IBattleTrainer> Trainers { get { return slotMappings; } }

        private readonly IReadOnlyList<IBattlePokemon> pokemon;
        public IReadOnlyList<IBattlePokemon> Pokemon { get { return pokemon; } }

        public BattleTeam(IList<ITrainer> slotMappings)
        {
            if (slotMappings == null) { throw new Exception("Slot mappings must be a non-null list of ITrainer(s)");  }
            if (slotMappings.Any(x => x == null)){ throw new Exception("A slot cannot be mapped to a 'null' ITrainer"); }

            Dictionary<ITrainer, int> trainerPokemonCounts = new Dictionary<ITrainer, int>();
            List<int> slotIndexMap = new List<int>(slotMappings.Count);
            foreach (ITrainer trainer in slotMappings) 
            {
                if (!trainerPokemonCounts.ContainsKey(trainer)) { trainerPokemonCounts[trainer] = 0; }
                slotIndexMap.Add(trainerPokemonCounts[trainer]);
                trainerPokemonCounts[trainer]++;
            }
            slotIndexMappings = slotIndexMap.AsReadOnly();

            List<IBattleTrainer> slotMap = new List<IBattleTrainer>(slotMappings.Count);
            Dictionary<ITrainer, IBattleTrainer> trainerMap = new Dictionary<ITrainer, IBattleTrainer>(trainerPokemonCounts.Count);
            foreach (ITrainer trainer in slotMappings)
            {
                if (!trainerMap.ContainsKey(trainer))
                {
                    trainerMap[trainer] = new BattleTrainer(trainer, trainerPokemonCounts[trainer]);
                }
                slotMap.Add(trainerMap[trainer]);
            }

            uniqueTrainers = slotMap.Distinct().ToList().AsReadOnly();
            this.slotMappings = slotMap.AsReadOnly();

            pokemon = new BattlePokemonSlotMappingList(this.slotMappings, this.slotIndexMappings);
        }

        public IBattlePokemon PokemonAt(int i)
        {
            return Pokemon[i];
        }

        public IBattleTrainer TrainerAt(int i)
        {
            return slotMappings[i];
        }

        public bool Overlaps(BattleTeam other)
        {
            return uniqueTrainers.Any(x => other.uniqueTrainers.Contains(x));
        }

        private class BattlePokemonSlotMappingList : IReadOnlyList<IBattlePokemon>
        {
            private IReadOnlyList<IBattleTrainer> slotMappings;
            private IReadOnlyList<int> slotIndexMappings;

            public BattlePokemonSlotMappingList(IReadOnlyList<IBattleTrainer> slotMappings, IReadOnlyList<int> slotIndexMappings)
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
}
