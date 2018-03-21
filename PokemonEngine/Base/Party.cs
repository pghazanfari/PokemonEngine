using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Base.Events;
using PokemonEngine.Util;

namespace PokemonEngine.Base
{
    public class Party
    {
        public class PokemonSwappedEventArgs : EventArgs
        {
            public readonly IUniquePokemon First;
            public readonly int FirstSlot;

            public readonly IUniquePokemon Second;
            public readonly int SecondSlot;

            public PokemonSwappedEventArgs(IUniquePokemon first, int firstSlot, IUniquePokemon second, int secondSlot) : base()
            {
                First = first;
                FirstSlot = firstSlot;
                Second = second;
                SecondSlot = secondSlot;
            }
        }

        public class PokemonReplacedEventArgs : EventArgs
        {
            public readonly IUniquePokemon PartyPokemon;
            public readonly int PartyPokemonSlot;
            public readonly IUniquePokemon ReplacementPokemon;

            public PokemonReplacedEventArgs(IUniquePokemon partyPokemon, int partyPokemonSlot, IUniquePokemon replacementPokemon) : base()
            {
                PartyPokemon = partyPokemon;
                PartyPokemonSlot = partyPokemonSlot;
                ReplacementPokemon = replacementPokemon;
            }
        }

        public const int DefaultSize = 6;

        private readonly IList<IUniquePokemon> pokemon;
        private readonly IReadOnlyList<IUniquePokemon> roPokemon;
        public IReadOnlyList<IUniquePokemon> Pokemon { get { return roPokemon; } }

        public int PartySize { get; private set; }
        public int  MaxPartySize { get { return pokemon.Count; } }

        public IUniquePokemon this[int i]
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
                foreach (IUniquePokemon p in pokemon) { if (p == null) { return false; } }
                return true;
            }
        }

        public event EventHandler<Party, PokemonSwappedEventArgs> OnPokemonSwap;
        public event EventHandler<Party, PokemonSwappedEventArgs> OnPokemonSwapped;

        public event EventHandler<Party, PokemonReplacedEventArgs> OnPokemonReplace;
        public event EventHandler<Party, PokemonReplacedEventArgs> OnPokemonReplaced;

        public Party(int partySize, UniqueList<IUniquePokemon> pokemon)
        {
            if (partySize < pokemon.Count) { throw new Exception($"Party size ({partySize}) is smaller than the size of the supplied list of pokemon ({pokemon.Count})"); }

            this.pokemon = new List<IUniquePokemon>(pokemon);
            roPokemon = (this.pokemon as List<IUniquePokemon>).AsReadOnly();

            for (int i = this.pokemon.Count; i < partySize; i++) { this.pokemon.Add(null); }
            PartySize = this.pokemon.Count;
        }

        public Party(UniqueList<IUniquePokemon> pokemon) : this(DefaultSize, pokemon) { }

        public Party(int partySize, params IUniquePokemon[] pokemon): this(partySize, new UniqueList<IUniquePokemon>(pokemon)) { }

        public Party(params IUniquePokemon[] pokemon) : this(DefaultSize, pokemon) { }

        public bool Contains(IUniquePokemon pokemon)
        {
            return this.pokemon.Contains(pokemon);
        }

        public void Swap(IUniquePokemon first, IUniquePokemon second)
        {
            int firstIndex = pokemon.IndexOf(first);
            int secondIndex = pokemon.IndexOf(second);

            if (firstIndex < 0) { throw new Exception("First pokemon is not in this party"); }
            if (secondIndex < 0) { throw new Exception("Second pokemon is not in this party");  }

            Swap(firstIndex, secondIndex);
        }

        public void Swap(int firstSlot, int secondSlot)
        {
            if (firstSlot < 0 || firstSlot >= PartySize)
            {
                throw new Exception($"Invalid slot for first pokemon: {firstSlot}");
            }
            if (secondSlot < 0 || secondSlot >= PartySize)
            {
                throw new Exception($"Invalid slot for second pokemon: {secondSlot}");
            }

            if (firstSlot == secondSlot)
            {
                throw new Exception($"You cannot swap pokemon in the same slot");
            }
            PokemonSwappedEventArgs args = new PokemonSwappedEventArgs(pokemon[firstSlot], firstSlot, pokemon[secondSlot], secondSlot);
            OnPokemonSwap?.Invoke(this, args);
            IUniquePokemon tmp = pokemon[firstSlot];
            pokemon[firstSlot] = pokemon[secondSlot];
            pokemon[secondSlot] = tmp;
            OnPokemonSwapped?.Invoke(this, args);
        }

        public IUniquePokemon Replace(int slot, IUniquePokemon replacementPokemon)
        {
            PokemonReplacedEventArgs args = new PokemonReplacedEventArgs(pokemon[slot], slot, replacementPokemon);
            OnPokemonReplace(this, args);
            IUniquePokemon old = pokemon[slot];
            pokemon[slot] = replacementPokemon;
            OnPokemonReplaced(this, args);
            return old;
        }
    }
}
