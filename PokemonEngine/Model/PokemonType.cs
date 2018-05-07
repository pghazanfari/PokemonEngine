﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model
{
    public class PokemonType
    {
        public const float EFFECTIVE = 1.0f;
        public const float NOT_VERY_EFFECTIVE = 0.5f;
        public const float SUPER_EFFECTIVE = 2.0f;
        public const float NO_EFFECT = 0.0f;

        private static Dictionary<PokemonType, Dictionary<PokemonType, float>> Effectiveness = new Dictionary<PokemonType, Dictionary<PokemonType, float>>();

        public static readonly PokemonType Normal = new PokemonType("Normal");
        public static readonly PokemonType Fighting = new PokemonType("Fighting");
        public static readonly PokemonType Flying = new PokemonType("Flying");
        public static readonly PokemonType Poison = new PokemonType("Poison");
        public static readonly PokemonType Ground = new PokemonType("Ground");
        public static readonly PokemonType Rock = new PokemonType("Rock");
        public static readonly PokemonType Bug = new PokemonType("Bug");
        public static readonly PokemonType Ghost = new PokemonType("Ghost");
        public static readonly PokemonType Steel = new PokemonType("Steel");
        public static readonly PokemonType Fire = new PokemonType("Fire");
        public static readonly PokemonType Water = new PokemonType("Water");
        public static readonly PokemonType Grass = new PokemonType("Grass");
        public static readonly PokemonType Electric = new PokemonType("Electric");
        public static readonly PokemonType Psychic = new PokemonType("Psychic");
        public static readonly PokemonType Ice = new PokemonType("Ice");
        public static readonly PokemonType Dragon = new PokemonType("Dragon");
        public static readonly PokemonType Dark = new PokemonType("Dark");
        public static readonly PokemonType Fairy = new PokemonType("Fairy");

        public string Name { get; }
        private PokemonType(string name)
        {
            Name = name;
        }

        public bool IsSuperEffectiveAgainst(PokemonType other)
        {
            return EffectivenessAgainst(other) > EFFECTIVE;
        }

        public bool IsNotVeryEffectiveAgainst(PokemonType other)
        {
            return EffectivenessAgainst(other) < EFFECTIVE;
        }

        public bool HasNoEffectAgainst(PokemonType other)
        {
            return EffectivenessAgainst(other) == NO_EFFECT;
        }

        public float EffectivenessAgainst(PokemonType other)
        {
            float effectiveness = EFFECTIVE;
            Effectiveness[this].TryGetValue(other, out effectiveness);
            return effectiveness;
        }

        public float EffectivenessFrom(PokemonType other)
        {
            float effectiveness = EFFECTIVE;
            Effectiveness[other].TryGetValue(this, out effectiveness);
            return effectiveness;
        }

        static PokemonType()
        {
            //Effectiveness[Attacker][Defender] = Effectiveness

            //Normal
            Effectiveness[Normal][Rock] = NOT_VERY_EFFECTIVE;
            Effectiveness[Normal][Ghost] = NO_EFFECT;
            Effectiveness[Normal][Steel] = NOT_VERY_EFFECTIVE;

            //Fighting
            Effectiveness[Fighting][Normal] = SUPER_EFFECTIVE;
            Effectiveness[Fighting][Flying] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fighting][Poison] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fighting][Rock] = SUPER_EFFECTIVE;
            Effectiveness[Fighting][Bug] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fighting][Ghost] = NO_EFFECT;
            Effectiveness[Fighting][Steel] = SUPER_EFFECTIVE;
            Effectiveness[Fighting][Psychic] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fighting][Ice] = SUPER_EFFECTIVE;
            Effectiveness[Fighting][Dark] = SUPER_EFFECTIVE;
            Effectiveness[Fighting][Fairy] = NOT_VERY_EFFECTIVE;

            //Flying
            Effectiveness[Flying][Fighting] = SUPER_EFFECTIVE;
            Effectiveness[Flying][Rock] = NOT_VERY_EFFECTIVE;
            Effectiveness[Flying][Bug] = SUPER_EFFECTIVE;
            Effectiveness[Flying][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Flying][Grass] = SUPER_EFFECTIVE;
            Effectiveness[Flying][Electric] = NOT_VERY_EFFECTIVE;

            //Poison
            Effectiveness[Poison][Poison] = NOT_VERY_EFFECTIVE;
            Effectiveness[Poison][Ground] = NOT_VERY_EFFECTIVE;
            Effectiveness[Poison][Rock] = NOT_VERY_EFFECTIVE;
            Effectiveness[Poison][Ghost] = NOT_VERY_EFFECTIVE;
            Effectiveness[Poison][Steel] = NO_EFFECT;
            Effectiveness[Poison][Grass] = SUPER_EFFECTIVE;
            Effectiveness[Poison][Fairy] = SUPER_EFFECTIVE;

            //Ground
            Effectiveness[Ground][Flying] = NO_EFFECT;
            Effectiveness[Ground][Poison] = SUPER_EFFECTIVE;
            Effectiveness[Ground][Rock] = SUPER_EFFECTIVE;
            Effectiveness[Ground][Bug] = NOT_VERY_EFFECTIVE;
            Effectiveness[Ground][Steel] = SUPER_EFFECTIVE;
            Effectiveness[Ground][Fire] = SUPER_EFFECTIVE;
            Effectiveness[Ground][Grass] = NOT_VERY_EFFECTIVE;
            Effectiveness[Ground][Electric] = SUPER_EFFECTIVE;

            //Rock
            Effectiveness[Rock][Fighting] = NOT_VERY_EFFECTIVE;
            Effectiveness[Rock][Flying] = SUPER_EFFECTIVE;
            Effectiveness[Rock][Ground] = NOT_VERY_EFFECTIVE;
            Effectiveness[Rock][Bug] = SUPER_EFFECTIVE;
            Effectiveness[Rock][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Rock][Fire] = SUPER_EFFECTIVE;
            Effectiveness[Rock][Ice] = SUPER_EFFECTIVE;

            //Bug
            Effectiveness[Bug][Fighting] = NOT_VERY_EFFECTIVE;
            Effectiveness[Bug][Flying] = NOT_VERY_EFFECTIVE;
            Effectiveness[Bug][Poison] = NOT_VERY_EFFECTIVE;
            Effectiveness[Bug][Ghost] = NOT_VERY_EFFECTIVE;
            Effectiveness[Bug][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Bug][Fire] = NOT_VERY_EFFECTIVE;
            Effectiveness[Bug][Grass] = SUPER_EFFECTIVE;
            Effectiveness[Bug][Psychic] = SUPER_EFFECTIVE;
            Effectiveness[Bug][Dark] = SUPER_EFFECTIVE;
            Effectiveness[Bug][Fairy] = NOT_VERY_EFFECTIVE;

            //Ghost
            Effectiveness[Ghost][Normal] = NO_EFFECT;
            Effectiveness[Ghost][Ghost] = SUPER_EFFECTIVE;
            Effectiveness[Ghost][Psychic] = SUPER_EFFECTIVE;
            Effectiveness[Ghost][Dark] = NO_EFFECT;

            //Steel
            Effectiveness[Steel][Rock] = SUPER_EFFECTIVE;
            Effectiveness[Steel][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Steel][Fire] = NOT_VERY_EFFECTIVE;
            Effectiveness[Steel][Water] = NOT_VERY_EFFECTIVE;
            Effectiveness[Steel][Electric] = NOT_VERY_EFFECTIVE;
            Effectiveness[Steel][Ice] = SUPER_EFFECTIVE;
            Effectiveness[Steel][Fairy] = SUPER_EFFECTIVE;

            //Fire
            Effectiveness[Fire][Rock] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fire][Bug] = SUPER_EFFECTIVE;
            Effectiveness[Fire][Steel] = SUPER_EFFECTIVE;
            Effectiveness[Fire][Fire] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fire][Water] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fire][Grass] = SUPER_EFFECTIVE;
            Effectiveness[Fire][Ice] = SUPER_EFFECTIVE;
            Effectiveness[Fire][Dragon] = NOT_VERY_EFFECTIVE;

            //Water
            Effectiveness[Water][Ground] = SUPER_EFFECTIVE;
            Effectiveness[Water][Rock] = SUPER_EFFECTIVE;
            Effectiveness[Water][Fire] = SUPER_EFFECTIVE;
            Effectiveness[Water][Water] = NOT_VERY_EFFECTIVE;
            Effectiveness[Water][Grass] = NOT_VERY_EFFECTIVE;
            Effectiveness[Water][Dragon] = NOT_VERY_EFFECTIVE;

            //Grass
            Effectiveness[Grass][Flying] = NOT_VERY_EFFECTIVE;
            Effectiveness[Grass][Poison] = NOT_VERY_EFFECTIVE;
            Effectiveness[Grass][Ground] = SUPER_EFFECTIVE;
            Effectiveness[Grass][Rock] = SUPER_EFFECTIVE;
            Effectiveness[Grass][Bug] = NOT_VERY_EFFECTIVE;
            Effectiveness[Grass][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Grass][Fire] = NOT_VERY_EFFECTIVE;
            Effectiveness[Grass][Water] = SUPER_EFFECTIVE;
            Effectiveness[Grass][Grass] = NOT_VERY_EFFECTIVE;
            Effectiveness[Grass][Dragon] = NOT_VERY_EFFECTIVE;

            //Electric
            Effectiveness[Electric][Flying] = SUPER_EFFECTIVE;
            Effectiveness[Electric][Ground] = NO_EFFECT;
            Effectiveness[Electric][Water] = SUPER_EFFECTIVE;
            Effectiveness[Electric][Grass] = NOT_VERY_EFFECTIVE;
            Effectiveness[Electric][Electric] = NOT_VERY_EFFECTIVE;
            Effectiveness[Electric][Ice] = NOT_VERY_EFFECTIVE;

            //Psychic
            Effectiveness[Psychic][Fighting] = SUPER_EFFECTIVE;
            Effectiveness[Psychic][Poison] = SUPER_EFFECTIVE;
            Effectiveness[Psychic][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Psychic][Psychic] = NOT_VERY_EFFECTIVE;
            Effectiveness[Psychic][Dark] = NO_EFFECT;

            //Ice
            Effectiveness[Ice][Flying] = SUPER_EFFECTIVE;
            Effectiveness[Ice][Ground] = SUPER_EFFECTIVE;
            Effectiveness[Ice][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Ice][Fire] = NOT_VERY_EFFECTIVE;
            Effectiveness[Ice][Water] = NOT_VERY_EFFECTIVE;
            Effectiveness[Ice][Grass] = SUPER_EFFECTIVE;
            Effectiveness[Ice][Ice] = NOT_VERY_EFFECTIVE;
            Effectiveness[Ice][Dragon] = SUPER_EFFECTIVE;

            //Dragon
            Effectiveness[Dragon][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Dragon][Dragon] = SUPER_EFFECTIVE;
            Effectiveness[Dragon][Fairy] = NO_EFFECT;

            //Dark
            Effectiveness[Dark][Fire] = NOT_VERY_EFFECTIVE;
            Effectiveness[Dark][Ghost] = SUPER_EFFECTIVE;
            Effectiveness[Dark][Psychic] = SUPER_EFFECTIVE;
            Effectiveness[Dark][Dark] = NOT_VERY_EFFECTIVE;
            Effectiveness[Dark][Fairy] = NOT_VERY_EFFECTIVE;

            //Fairy
            Effectiveness[Fairy][Fighting] = SUPER_EFFECTIVE;
            Effectiveness[Fairy][Ground] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fairy][Steel] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fairy][Fire] = NOT_VERY_EFFECTIVE;
            Effectiveness[Fairy][Dragon] = SUPER_EFFECTIVE;
            Effectiveness[Fairy][Dark] = SUPER_EFFECTIVE;
        }
    }
}