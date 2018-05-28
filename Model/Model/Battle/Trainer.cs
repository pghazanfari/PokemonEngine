using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonEngine.Model.Unique;

namespace PokemonEngine.Model.Battle
{
    public class Trainer : ITrainer
    {
        public readonly Unique.ITrainer Base;

        private IList<IPokemon> battlers;
        private readonly IReadOnlyList<IPokemon> roBattlers;
        public IReadOnlyList<IPokemon> Battlers
        {
            get
            {
                return roBattlers;
            }
        }

        public Party Party
        {
            get
            {
                return Base.Party;
            }
        }

        public string UID
        {
            get
            {
                return Base.UID;
            }
        }

        public Trainer(Unique.ITrainer baseTrainer, int numBattlers)
        {
            Base = baseTrainer;
            battlers = new List<IPokemon>(numBattlers);
            for (int i = 0; i < Party.PokemonCount && i < numBattlers; i++)
            {
                battlers.Add(new Pokemon(Party[i]));
            }
            roBattlers = (battlers as List<IPokemon>).AsReadOnly();
        }
    }
}
