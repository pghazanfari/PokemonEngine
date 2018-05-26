using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common
{
    public delegate void SimpleEventHandler<TSender>(TSender sender);
    public delegate void EventHandler<TSender, TArgs>(TSender sender, TArgs e);
}
