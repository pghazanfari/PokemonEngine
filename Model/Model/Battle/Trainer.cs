using System;
using System.Collections.Generic;
using PokemonEngine.Model.Unique;

namespace PokemonEngine.Model.Battle
{
    public class Trainer : ITrainer
    {
        public Unique.ITrainer Base { get; }

        public int NumBattlers { get; }
        private readonly IList<IPokemon> battlers;
        public IReadOnlyList<IPokemon> Battlers { get; }

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
            NumBattlers = numBattlers;
            battlers = new List<IPokemon>(numBattlers);
            for (int i = 0; i < Party.PokemonCount && i < numBattlers; i++)
            {
                battlers.Add(new Pokemon(Party[i]));
            }
            Battlers = (battlers as List<IPokemon>).AsReadOnly();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        Unique.ITrainer Unique.ITrainer.Clone()
        {
            return Clone();
        }

        ITrainer ITrainer.Clone()
        {
            return Clone();
        }

        IParticipant IParticipant.Clone()
        {
            return Clone();
        }

        public Trainer Clone()
        {
            return new Model.Battle.Trainer(Base.Clone(), NumBattlers);
        }
    }
}
