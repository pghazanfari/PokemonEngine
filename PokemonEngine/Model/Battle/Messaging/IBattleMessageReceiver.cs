using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Battle.Messaging
{
    public interface IBattleMessageReceiver : IMessageReceiver<Run>
    {
    }
}
