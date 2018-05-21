using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Unique
{
    public interface ITrainer
    {
        string UID { get; }
        Party Party { get; }

        //TODO: Items
    }
}
