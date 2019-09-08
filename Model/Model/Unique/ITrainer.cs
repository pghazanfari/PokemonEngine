using System;

namespace PokemonEngine.Model.Unique
{
    public interface ITrainer : ICloneable
    {
        string UID { get; }
        Party Party { get; }

        //TODO: Items

        new ITrainer Clone();
    }
}
