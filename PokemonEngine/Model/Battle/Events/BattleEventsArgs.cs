using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Events
{
    public delegate void BattleEventHandler(IBattle battle, BattleEventsArgs args);
    public class BattleEventsArgs : EventArgs
    {
        //TODO
    }
}
