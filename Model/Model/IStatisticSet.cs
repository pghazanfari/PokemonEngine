using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public interface IStatisticSet
    {
        int this[Statistic stat] { get; }
    }
}
