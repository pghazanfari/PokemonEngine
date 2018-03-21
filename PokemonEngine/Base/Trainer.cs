using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class Trainer : ITrainer
    {
        private readonly Party party;
        public Party Party
        {
            get
            {
                return party;
            }
        }

        public Trainer(Party party)
        {
            this.party = party;
        }
    }
}
