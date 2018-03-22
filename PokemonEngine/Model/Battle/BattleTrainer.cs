using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PokemonEngine.Model.Battle
{
    public class BattleTrainer : IBattleParticipant
    {
        public readonly Model.IBattleParticipant Base;

        #region Base Wrapper Methods
        public string UID { get { return Base.UID; } }
        public Party Party { get { return Base.Party; } } 
        #endregion

        private readonly IList<IBattlePokemon> battlingPokemon;
        private readonly IReadOnlyList<IBattlePokemon> roBattlingPokemon;
        public IReadOnlyList<IBattlePokemon> BattlingPokemon { get { return roBattlingPokemon; } }

        public BattleTrainer(Model.IBattleParticipant baseTrainer, int numCombatants)
        {
            Base = baseTrainer;
            battlingPokemon = new List<IBattlePokemon>(numCombatants);
            for (int i = 0; i < numCombatants && i < baseTrainer.Party.PartySize; i++)
            {
                battlingPokemon.Add(new BattlePokemon(baseTrainer.Party[i]));
            }
            roBattlingPokemon = (battlingPokemon as List<IBattlePokemon>).AsReadOnly();
        }

        public BattleTrainer(Model.IBattleParticipant baseTrainer) : this(baseTrainer, 1) { }

        public void SwapPokemon(IBattlePokemon inPlayPokemon, IUniquePokemon partyPokemon)
        {
            if (!BattlingPokemon.Contains(inPlayPokemon)) { throw new Exception("Battle pokemon is not currently in play"); }
            if (!Party.Contains(partyPokemon)) { throw new Exception("Party pokemon is not part of this trainer's pokemon"); }
            if (BattlingPokemon.Contains(partyPokemon)) { throw new Exception("Party pokemon is already in the battle");  }

            int index = battlingPokemon.IndexOf(inPlayPokemon);
            battlingPokemon[index] = new BattlePokemon(partyPokemon);
        }
    }
}
