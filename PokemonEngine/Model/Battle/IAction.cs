using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle
{
    public interface IAction : IMessage// TODO: , IComparable<IBattleAction>
    {
        Slot User { get; }
    }
}
