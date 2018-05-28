using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Actions;

namespace PokemonEngine.Model.Battle
{
    public interface ITargetSelector
    {
        ICollection<Slot> SelectTargets(IBattle battle, UseMove move);
    }
    /*
    public class DefaultTargetSelector : ITargetSelector
    {
        public DefaultTargetSelector() { }

        //TODO: Consider how some move types force what you can choose
        public ICollection<Slot> SelectTargets(IBattle battle, UseMove move)
        {
            switch (move.Move.Target)
            {
                case MoveTarget.AllFoes:
                    break;
                case MoveTarget.AllOtherPokemon:
                    break;
                case MoveTarget.AllPokemon:
                    break;
                case MoveTarget.Ally:
                    break;
                case MoveTarget.AnyFoe:
                    break;
                case MoveTarget.Self:
                    break;
                case MoveTarget.SelfOrAlly:
                    break;
                case MoveTarget.Team:
                    break;
                default:
                    throw new InvalidOperationException("Selecting target for an unknown MoveTarget type");
            }
        }
    }
    */
}
