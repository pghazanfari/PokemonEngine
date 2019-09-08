using System;

namespace PokemonEngine.Model.Battle
{
    public interface IModifier : IDisposable
    {
        float Factor { get; }
    }
}
