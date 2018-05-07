using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common
{
    public interface IProvider<T, U>
    {
        T Provide(U request);
    }
}
