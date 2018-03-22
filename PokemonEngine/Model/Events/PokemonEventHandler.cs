using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Events
{
    public delegate void EventHandler<TSender, TArgs>(TSender sender, TArgs e)
        where TArgs : EventArgs;

    public delegate void PokemonEventHandler<TSender, TArgs>(TSender sender, TArgs e) 
        where TSender : IPokemon 
        where TArgs : EventArgs;
}
