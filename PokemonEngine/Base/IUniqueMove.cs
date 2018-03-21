using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public interface IUniqueMove : IMove
    {
        int PP { get; }
        int MaxPP { get; }

        //TODO: PP Changes
    }
}
