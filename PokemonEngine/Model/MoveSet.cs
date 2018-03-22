using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Util;

namespace PokemonEngine.Model
{
    public class MoveSet<T> where T : IUniqueMove
    {
        public const int MaxNumberOfMoves = 4;

        private readonly IReadOnlyList<T> roMoves;
        public IReadOnlyList<T> Moves { get { return roMoves; } }

        private readonly IList<T> moves;
        public T this[int i] { get { return moves[i]; } }

        public MoveSet(IList<T> moves)
        {
            if (moves.Count == 0)
            {
                throw new Exception("MoveSet must contain at least 1 move");
            }
            if (moves.Count > MaxNumberOfMoves)
            {
                throw new Exception($"Move count {moves.Count} is greater than the maximum number of moves {MaxNumberOfMoves}");
            }

            IList<T> nonNullMoves = moves.Where(x => x != null).ToList();
            if (nonNullMoves.Count == 0)
            {
                throw new Exception("Move set must contain at least 1 non-null move");
            }

            this.moves = new List<T>(MaxNumberOfMoves);
            (this.moves as List<T>).AddRange(moves);
            for (int i = this.moves.Count; i < MaxNumberOfMoves; i++)
            {
                this.moves.Add(default(T));
            }
            roMoves = (this.moves as List<T>).AsReadOnly();
        }

        public MoveSet(params T[] moves) : this(new List<T>(moves)) { }

        // TODO: Move Replaced Event
        public T ReplaceMove(int index, T newMove)
        {
            if (moves.Contains(newMove))
            {
                throw new Exception("Duplicate moves cannot exit");
            }
            T oldMove = moves[index];
            moves[index] = newMove;
            return oldMove;
        }
    }
}
