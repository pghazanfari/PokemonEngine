using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class PMoveSet
    {
        private readonly IReadOnlyList<PMove> starterMoves;
        public IReadOnlyList<PMove> StarterMoves { get { return starterMoves; } }

        private readonly IReadOnlyDictionary<int, PMove> moves;
        public IReadOnlyDictionary<int, PMove> Moves { get { return moves; } }

        //TODO: TMs and HMs

        public PMoveSet(IList<PMove> starterMoves, IDictionary<int, PMove> moves)
        {
            //TODO: Validation
            this.starterMoves = new List<PMove>(starterMoves).AsReadOnly();
            this.moves = new ReadOnlyDictionary<int, PMove>(moves);
        }
    }
}
