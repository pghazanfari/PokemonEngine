using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Base
{
    public class MoveSet
    {
        public const int MaxNumberOfMoves = 4;

        private readonly IList<Move> moves;
        public Move this[int i] { get { return moves[i]; } }

        public MoveSet(IList<Move> moves)
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

            this.moves = new List<Move>(4);
            (this.moves as List<Move>).AddRange(moves);
            for (int i = this.moves.Count; i < MaxNumberOfMoves; i++)
            {
                this.moves.Add(null);
            }
        }

        public MoveSet(params Move[] moves) : this(new List<Move>(moves)) { }

        // TODO: Move Replaced Event
        public Move ReplaceMove(int index, Move newMove)
        {
            if (moves.Contains(newMove))
            {
                throw new Exception("Duplicate moves cannot exit");
            }
            Move oldMove = moves[index];
            moves[index] = newMove;
            return oldMove;
        }
    }
}
