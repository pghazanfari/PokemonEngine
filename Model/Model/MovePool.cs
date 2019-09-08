using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PokemonEngine.Model
{
    public class MovePool
    {
        public IReadOnlyList<IMove> StarterMoves { get; }

        private IReadOnlyDictionary<int, IMove> Moves { get; }
        public IMove this[int i]
        {
            get
            {
                return Moves[i];
            }
        }

        //TODO: TMs and HMs

        public MovePool(IList<IMove> starterMoves, IDictionary<int, IMove> moves)
        {
            //TODO: Validation
            StarterMoves = new List<IMove>(starterMoves).AsReadOnly();
            Moves = new ReadOnlyDictionary<int, IMove>(moves);
        }
    }
}
