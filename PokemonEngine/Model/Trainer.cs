using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class Trainer : IBattleParticipant
    {
        private readonly string uid;
        public string UID { get { return uid; } }

        private readonly Party party;
        public Party Party
        {
            get
            {
                return party;
            }
        }

        public Trainer(string uid, Party party)
        {
            this.uid = uid;
            this.party = party;
        }

        public Trainer(Party party) : this(Guid.NewGuid().ToString(), party) { }
    }
}
