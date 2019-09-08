using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokemonEngine.Model.Unique
{
    public class Party : IEnumerable<Unique.IPokemon>, ICloneable
    {
        public class PokemonReplacedEventArgs : EventArgs
        {
            public Unique.IPokemon PartyPokemon { get; }
            public int PartyPokemonSlot { get; }
            public Unique.IPokemon ReplacementPokemon { get; }

            public PokemonReplacedEventArgs(Unique.IPokemon partyPokemon, int partyPokemonSlot, Unique.IPokemon replacementPokemon) : base()
            {
                PartyPokemon = partyPokemon;
                PartyPokemonSlot = partyPokemonSlot;
                ReplacementPokemon = replacementPokemon;
            }
        }

        public const int DefaultSize = 6;

        private readonly IList<Unique.IPokemon> pokemon;
        public int Size { get; }

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
                return pokemon.Count >= Size;
            }
        }

        public int PokemonCount
        {
            get
            {
                return pokemon.Count;
            }
        }

        public event EventHandler<PokemonSwappedEventArgs> OnSwapPokemon;
        public event EventHandler<PokemonSwappedEventArgs> OnPokemonSwapped;

        public event EventHandler<PokemonReplacedEventArgs> OnReplacePokemon;
        public event EventHandler<PokemonReplacedEventArgs> OnPokemonReplaced;

        public Party(int size, IList<Unique.IPokemon> pokemon)
        {
            if (size < pokemon.Count) { throw new Exception($"Party size ({size}) is smaller than the size of the supplied list of pokemon ({pokemon.Count})"); }

            Size = size;
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

        public void Swap(int slot1, int slot2)
        {
            if (slot1 < 0 || slot1 >= PokemonCount)
            {
                throw new Exception($"Invalid slot for first pokemon: {slot1}");
            }
            if (slot2 < 0 || slot2 >= PokemonCount)
            {
                throw new Exception($"Invalid slot for second pokemon: {slot2}");
            }

            if (slot1 == slot2)
            {
                throw new Exception($"You cannot swap pokemon in the same slot");
            }
            PokemonSwappedEventArgs args = new PokemonSwappedEventArgs(this, slot1, slot2);
            OnSwapPokemon?.Invoke(this, args);
            Unique.IPokemon tmp = pokemon[slot1];
            pokemon[slot1] = pokemon[slot2];
            pokemon[slot2] = tmp;
            OnPokemonSwapped?.Invoke(this, args);
        }

        public Unique.IPokemon Replace(int slot, Unique.IPokemon replacementPokemon)
        {
            PokemonReplacedEventArgs args = new PokemonReplacedEventArgs(pokemon[slot], slot, replacementPokemon);
            OnReplacePokemon(this, args);
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
            return new Party(pokemon.Select(x => x.Clone()).ToList());
        }
    }
}
