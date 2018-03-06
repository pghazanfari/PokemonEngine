using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public interface IStatsProvider
    {
        int this[Stat stat] { get;  }
    }
}
