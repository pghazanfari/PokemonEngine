using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelUnitTests.PokemonImpl;
using PokemonEngine.Model.Battle;
using PokemonEngine.Model.Unique;

namespace ModelUnitTests
{
    public class Helper
    {
        private Helper() { }

        public static PokemonEngine.Model.Unique.ITrainer ConstructTrainer(String name, int level, int numBulbasaurs)
        {
            List<PokemonEngine.Model.Unique.IPokemon> list = new List<PokemonEngine.Model.Unique.IPokemon>(numBulbasaurs);
            for (int i = 0; i < numBulbasaurs; i++) { list.Add(Bulbasaur.ConstructSimple(level)); }
            Party party = new Party(list);
            return new PokemonEngine.Model.Unique.Trainer(name, party);
        }

        public static Team ConstructTeam(string name, int level, int numBulbasaurs)
        {
            List<PokemonEngine.Model.Battle.IParticipant> list = new List<IParticipant>(numBulbasaurs);
            for (int i = 0; i < numBulbasaurs; i++)
            {
                list.Add(new PokemonEngine.Model.Battle.Trainer(ConstructTrainer(name, level, numBulbasaurs), 1));
            }
            return new Team(list);
        }

    }
}
