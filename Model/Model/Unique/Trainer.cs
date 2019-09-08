using System;

namespace PokemonEngine.Model.Unique
{
    public class Trainer : ITrainer
    {
        public string UID { get; }
        public Party Party { get; }

        public Trainer(string uid, Party party)
        {
            UID = uid;
            Party = party;
        }

        public Trainer(Party party) : this(Guid.NewGuid().ToString(), party) { }

        object ICloneable.Clone()
        {
            return Clone();
        }

        ITrainer ITrainer.Clone()
        {
            return Clone();
        }

        public Trainer Clone()
        {
            return new Trainer(UID, Party.Clone());
        }
    }
}
