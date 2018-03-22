using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PokemonEngine.Model.Battle
{
    public interface IBattleMove : IUniqueMove
    {
        bool IsDisabled { get; }
    }
}
