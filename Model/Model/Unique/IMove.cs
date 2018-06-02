using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
