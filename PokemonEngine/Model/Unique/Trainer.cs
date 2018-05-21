using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Unique
{
    public class Trainer : ITrainer
    {
        private readonly String uid;
        public string UID { get { return uid; } }

        private readonly Party party;
        public Party Party { get { return party; } }

        public Trainer(String uid, Party party)
        {
            this.uid = uid;
            this.party = party;
        }

    }
}
