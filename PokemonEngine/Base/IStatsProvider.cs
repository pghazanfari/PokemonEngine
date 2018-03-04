using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public enum PStat { HP, Attack, Defense, SpecialAttack, SpecialDefense, Speed }
    public interface IStatsProvider
    {
        int this[PStat stat] { get;  }
    }
}
