using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class PMoves
    {
        public const int MaxNumberOfMoves = 4;

        private readonly IList<PMove> moves;
        public PMove this[int i] { get { return moves[i]; } }

        public PMoves(IList<PMove> moves)
        {
            if (moves.Count == 0)
            {
                throw new Exception("Move count must be greater than 0");
            }
            if (moves.Count > MaxNumberOfMoves)
            {
                throw new Exception($"Move count {moves.Count} is greater than the maximum number of moves {MaxNumberOfMoves}");
            }
            if (moves.Count != moves.Distinct().Count())
            {
                throw new Exception("Duplicate moves cannot exist");
            }

            this.moves = new List<PMove>(4);
            (this.moves as List<PMove>).AddRange(moves);
            for (int i = this.moves.Count; i < MaxNumberOfMoves; i++)
            {
                this.moves.Add(null);
            }
        }

        public PMoves(params PMove[] moves) : this(new List<PMove>(moves)) { }

        public PMove ReplaceMove(int index, PMove newMove)
        {
            if (moves.Contains(newMove))
            {
                throw new Exception("Duplicate moves cannot exit");
            }
            PMove oldMove = moves[index];
            moves[index] = newMove;
            return oldMove;
        }
    }
}
