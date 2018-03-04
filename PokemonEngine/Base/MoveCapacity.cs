using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class MoveCapacity
    {
        private readonly IReadOnlyList<Move> starterMoves;
        public IReadOnlyList<Move> StarterMoves { get { return starterMoves; } }

        private readonly IReadOnlyDictionary<int, Move> moves;
        public IReadOnlyDictionary<int, Move> Moves { get { return moves; } }

        //TODO: TMs and HMs

        public MoveCapacity(IList<Move> starterMoves, IDictionary<int, Move> moves)
        {
            //TODO: Validation
            this.starterMoves = new List<Move>(starterMoves).AsReadOnly();
            this.moves = new ReadOnlyDictionary<int, Move>(moves);
        }
    }
}
