using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class Moves
    {
        private readonly IReadOnlyList<IMove> starterMoves;
        public IReadOnlyList<IMove> StarterMoves { get { return starterMoves; } }

        private readonly IReadOnlyDictionary<int, IMove> moves;
        public IMove this[int i]
        {
            get
            {
                return moves[i];
            }
        }

        //TODO: TMs and HMs

        public Moves(IList<IMove> starterMoves, IDictionary<int, IMove> moves)
        {
            //TODO: Validation
            this.starterMoves = new List<IMove>(starterMoves).AsReadOnly();
            this.moves = new ReadOnlyDictionary<int, IMove>(moves);
        }
    }
}
