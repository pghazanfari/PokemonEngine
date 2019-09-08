using System;

namespace PokemonEngine.Model.Unique
{
    public interface IMove : Model.IMove, ICloneable
    {
        int PP { get; }
        int MaxPP { get; }

        //TODO: PP Changes

        new IMove Clone();
    }
}
