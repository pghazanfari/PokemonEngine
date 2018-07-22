using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public interface IModifier : IDisposable
    {
        event EventHandler<IModifier> OnDispose;
        event EventHandler<IModifier> OnDisposed;

        float Factor { get; }
    }
}
