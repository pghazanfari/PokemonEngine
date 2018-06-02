using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Common;
using System.Collections;

namespace PokemonEngine.Model.Unique
{
    public class Party : IEnumerable<Unique.IPokemon>, ICloneable
    {
        public class PokemonSwappedEventArgs : EventArgs
        {
            public readonly Unique.IPokemon First;
            public readonly int FirstSlot;

            public readonly Unique.IPokemon Second;
            public readonly int SecondSlot;

            public PokemonSwappedEventArgs(Unique.IPokemon first, int firstSlot, Unique.IPokemon second, int secondSlot) : base()
            {
                First = first;
                FirstSlot = firstSlot;
                Second = second;
                SecondSlot = secondSlot;
            }
        }

        public class PokemonReplacedEventArgs : EventArgs
        {
            public readonly Unique.IPokemon PartyPokemon;
            public readonly int PartyPokemonSlot;
            public readonly Unique.IPokemon ReplacementPokemon;

            public PokemonReplacedEventArgs(Unique.IPokemon partyPokemon, int partyPokemonSlot, Unique.IPokemon replacementPokemon) : base()
            {
                PartyPokemon = partyPokemon;
                PartyPokemonSlot = partyPokemonSlot;
                ReplacementPokemon = replacementPokemon;
            }
        }

        public const int DefaultSize = 6;

        private readonly IList<Unique.IPokemon> pokemon;

        private readonly int size;
        public int Size { get { return size; } }

        public Unique.IPokemon this[int i]
        {
            get
            {
                return pokemon[i];
            }
        }

        public bool IsFull
        {
            get
            {
                return pokemon.Count >= size;
            }
        }

        public int PokemonCount
        {
            get
            {
                return pokemon.Count;
            }
        }

        public event EventHandler<Party, PokemonSwappedEventArgs> OnPokemonSwap;
        public event EventHandler<Party, PokemonSwappedEventArgs> OnPokemonSwapped;

        public event EventHandler<Party, PokemonReplacedEventArgs> OnPokemonReplace;
        public event EventHandler<Party, PokemonReplacedEventArgs> OnPokemonReplaced;

        public Party(int size, IList<Unique.IPokemon> pokemon)
        {
            if (size < pokemon.Count) { throw new Exception($"Party size ({size}) is smaller than the size of the supplied list of pokemon ({pokemon.Count})"); }

            this.size = size;
            this.pokemon = new List<Unique.IPokemon>(pokemon);
        }

        public Party(IList<Unique.IPokemon> pokemon) : this(DefaultSize, pokemon) { }

        public Party(int partySize, params Unique.IPokemon[] pokemon): this(partySize, new List<Unique.IPokemon>(pokemon)) { }

        public Party(params Unique.IPokemon[] pokemon) : this(DefaultSize, pokemon) { }

        public bool Contains(Unique.IPokemon pokemon)
        {
            return this.pokemon.Contains(pokemon);
        }

        public void Swap(Unique.IPokemon first, Unique.IPokemon second)
        {
            int firstIndex = pokemon.IndexOf(first);
            int secondIndex = pokemon.IndexOf(second);

            if (firstIndex < 0) { throw new Exception("First pokemon is not in this party"); }
            if (secondIndex < 0) { throw new Exception("Second pokemon is not in this party");  }

            Swap(firstIndex, secondIndex);
        }

        public void Swap(int firstSlot, int secondSlot)
        {
            if (firstSlot < 0 || firstSlot >= PokemonCount)
            {
                throw new Exception($"Invalid slot for first pokemon: {firstSlot}");
            }
            if (secondSlot < 0 || secondSlot >= PokemonCount)
            {
                throw new Exception($"Invalid slot for second pokemon: {secondSlot}");
            }

            if (firstSlot == secondSlot)
            {
                throw new Exception($"You cannot swap pokemon in the same slot");
            }
            PokemonSwappedEventArgs args = new PokemonSwappedEventArgs(pokemon[firstSlot], firstSlot, pokemon[secondSlot], secondSlot);
            OnPokemonSwap?.Invoke(this, args);
            Unique.IPokemon tmp = pokemon[firstSlot];
            pokemon[firstSlot] = pokemon[secondSlot];
            pokemon[secondSlot] = tmp;
            OnPokemonSwapped?.Invoke(this, args);
        }

        public Unique.IPokemon Replace(int slot, Unique.IPokemon replacementPokemon)
        {
            PokemonReplacedEventArgs args = new PokemonReplacedEventArgs(pokemon[slot], slot, replacementPokemon);
            OnPokemonReplace(this, args);
            Unique.IPokemon old = pokemon[slot];
            pokemon[slot] = replacementPokemon;
            OnPokemonReplaced(this, args);
            return old;
        }

        public IEnumerator<IPokemon> GetEnumerator()
        {
            return pokemon.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return pokemon.GetEnumerator();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public Party Clone()
        {
            return new Party(this.pokemon.Select(x => x.Clone()).ToList());
        }
    }
}
