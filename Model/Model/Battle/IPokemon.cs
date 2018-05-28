using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public interface IPokemon : Unique.IPokemon
    {
        new StatisticSet Stats { get; }
        new MoveSet<IMove> Moves { get; }

        //event EventHandler<UpdateStatStageEventArgs> OnUpdateStatStage;
        //event EventHandler<StatStageUpdatedEventArgs> OnStatStageUpdated;
    }

    public static class IPokemonImpl
    {
        public static bool HasFainted(this IPokemon pokemon)
        {
            return pokemon.HP == 0;
        }
    }
}
