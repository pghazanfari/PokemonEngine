using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common
{
    public delegate void EventHandler<TSender, TArgs>(TSender sender, TArgs e);
    public delegate void SenderOnlyEventHandler<TSender>(TSender sender);
}
