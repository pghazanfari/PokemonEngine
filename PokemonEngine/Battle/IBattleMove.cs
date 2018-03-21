using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base;

namespace PokemonEngine.Battle
{
    public interface IBattleMove : IUniqueMove
    {
        bool IsDisabled { get; }
    }
}
