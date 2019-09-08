using System.Collections.Generic;

namespace PokemonEngine.Model
{
    public class PokemonType
    {
        public const float EFFECTIVE = 1.0f;
        public const float NOT_VERY_EFFECTIVE = 0.5f;
        public const float SUPER_EFFECTIVE = 2.0f;
        public const float NO_EFFECT = 0.0f;

        private static readonly List<PokemonType> all = new List<PokemonType>();
        public static IReadOnlyList<PokemonType> All { get; } = all.AsReadOnly();

        private static readonly Dictionary<PokemonType, Dictionary<PokemonType, float>> Effectiveness = new Dictionary<PokemonType, Dictionary<PokemonType, float>>();

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
            all.Add(this);
        }

        public override string ToString()
        {
            return Name;
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
            if (Effectiveness[this].ContainsKey(other)) { effectiveness = Effectiveness[this][other]; }
            return effectiveness;
        }

        public float EffectivenessFrom(PokemonType other)
        {
            float effectiveness = EFFECTIVE;
            if (Effectiveness[other].ContainsKey(this)) { effectiveness = Effectiveness[other][this]; }
            return effectiveness;
        }

        static PokemonType()
        {
            //Effectiveness[Attacker][Defender] = Effectiveness


            //Normal
            Effectiveness[Normal] = new Dictionary<PokemonType, float>
            {
                [Rock] = NOT_VERY_EFFECTIVE,
                [Ghost] = NO_EFFECT,
                [Steel] = NOT_VERY_EFFECTIVE
            };

            //Fighting
            Effectiveness[Fighting] = new Dictionary<PokemonType, float>
            {
                [Normal] = SUPER_EFFECTIVE,
                [Flying] = NOT_VERY_EFFECTIVE,
                [Poison] = NOT_VERY_EFFECTIVE,
                [Rock] = SUPER_EFFECTIVE,
                [Bug] = NOT_VERY_EFFECTIVE,
                [Ghost] = NO_EFFECT,
                [Steel] = SUPER_EFFECTIVE,
                [Psychic] = NOT_VERY_EFFECTIVE,
                [Ice] = SUPER_EFFECTIVE,
                [Dark] = SUPER_EFFECTIVE,
                [Fairy] = NOT_VERY_EFFECTIVE
            };

            //Flying
            Effectiveness[Flying] = new Dictionary<PokemonType, float>
            {
                [Fighting] = SUPER_EFFECTIVE,
                [Rock] = NOT_VERY_EFFECTIVE,
                [Bug] = SUPER_EFFECTIVE,
                [Steel] = NOT_VERY_EFFECTIVE,
                [Grass] = SUPER_EFFECTIVE,
                [Electric] = NOT_VERY_EFFECTIVE
            };

            //Poison
            Effectiveness[Poison] = new Dictionary<PokemonType, float>
            {
                [Poison] = NOT_VERY_EFFECTIVE,
                [Ground] = NOT_VERY_EFFECTIVE,
                [Rock] = NOT_VERY_EFFECTIVE,
                [Ghost] = NOT_VERY_EFFECTIVE,
                [Steel] = NO_EFFECT,
                [Grass] = SUPER_EFFECTIVE,
                [Fairy] = SUPER_EFFECTIVE
            };

            //Ground
            Effectiveness[Ground] = new Dictionary<PokemonType, float>
            {
                [Flying] = NO_EFFECT,
                [Poison] = SUPER_EFFECTIVE,
                [Rock] = SUPER_EFFECTIVE,
                [Bug] = NOT_VERY_EFFECTIVE,
                [Steel] = SUPER_EFFECTIVE,
                [Fire] = SUPER_EFFECTIVE,
                [Grass] = NOT_VERY_EFFECTIVE,
                [Electric] = SUPER_EFFECTIVE
            };

            //Rock
            Effectiveness[Rock] = new Dictionary<PokemonType, float>
            {
                [Fighting] = NOT_VERY_EFFECTIVE,
                [Flying] = SUPER_EFFECTIVE,
                [Ground] = NOT_VERY_EFFECTIVE,
                [Bug] = SUPER_EFFECTIVE,
                [Steel] = NOT_VERY_EFFECTIVE,
                [Fire] = SUPER_EFFECTIVE,
                [Ice] = SUPER_EFFECTIVE
            };

            //Bug
            Effectiveness[Bug] = new Dictionary<PokemonType, float>
            {
                [Fighting] = NOT_VERY_EFFECTIVE,
                [Flying] = NOT_VERY_EFFECTIVE,
                [Poison] = NOT_VERY_EFFECTIVE,
                [Ghost] = NOT_VERY_EFFECTIVE,
                [Steel] = NOT_VERY_EFFECTIVE,
                [Fire] = NOT_VERY_EFFECTIVE,
                [Grass] = SUPER_EFFECTIVE,
                [Psychic] = SUPER_EFFECTIVE,
                [Dark] = SUPER_EFFECTIVE,
                [Fairy] = NOT_VERY_EFFECTIVE
            };

            //Ghost
            Effectiveness[Ghost] = new Dictionary<PokemonType, float>
            {
                [Normal] = NO_EFFECT,
                [Ghost] = SUPER_EFFECTIVE,
                [Psychic] = SUPER_EFFECTIVE,
                [Dark] = NO_EFFECT
            };

            //Steel
            Effectiveness[Steel] = new Dictionary<PokemonType, float>
            {
                [Rock] = SUPER_EFFECTIVE,
                [Steel] = NOT_VERY_EFFECTIVE,
                [Fire] = NOT_VERY_EFFECTIVE,
                [Water] = NOT_VERY_EFFECTIVE,
                [Electric] = NOT_VERY_EFFECTIVE,
                [Ice] = SUPER_EFFECTIVE,
                [Fairy] = SUPER_EFFECTIVE
            };

            //Fire
            Effectiveness[Fire] = new Dictionary<PokemonType, float>
            {
                [Rock] = NOT_VERY_EFFECTIVE,
                [Bug] = SUPER_EFFECTIVE,
                [Steel] = SUPER_EFFECTIVE,
                [Fire] = NOT_VERY_EFFECTIVE,
                [Water] = NOT_VERY_EFFECTIVE,
                [Grass] = SUPER_EFFECTIVE,
                [Ice] = SUPER_EFFECTIVE,
                [Dragon] = NOT_VERY_EFFECTIVE
            };

            //Water
            Effectiveness[Water] = new Dictionary<PokemonType, float>
            {
                [Ground] = SUPER_EFFECTIVE,
                [Rock] = SUPER_EFFECTIVE,
                [Fire] = SUPER_EFFECTIVE,
                [Water] = NOT_VERY_EFFECTIVE,
                [Grass] = NOT_VERY_EFFECTIVE,
                [Dragon] = NOT_VERY_EFFECTIVE
            };

            //Grass
            Effectiveness[Grass] = new Dictionary<PokemonType, float>
            {
                [Flying] = NOT_VERY_EFFECTIVE,
                [Poison] = NOT_VERY_EFFECTIVE,
                [Ground] = SUPER_EFFECTIVE,
                [Rock] = SUPER_EFFECTIVE,
                [Bug] = NOT_VERY_EFFECTIVE,
                [Steel] = NOT_VERY_EFFECTIVE,
                [Fire] = NOT_VERY_EFFECTIVE,
                [Water] = SUPER_EFFECTIVE,
                [Grass] = NOT_VERY_EFFECTIVE,
                [Dragon] = NOT_VERY_EFFECTIVE
            };

            //Electric
            Effectiveness[Electric] = new Dictionary<PokemonType, float>
            {
                [Flying] = SUPER_EFFECTIVE,
                [Ground] = NO_EFFECT,
                [Water] = SUPER_EFFECTIVE,
                [Grass] = NOT_VERY_EFFECTIVE,
                [Electric] = NOT_VERY_EFFECTIVE,
                [Ice] = NOT_VERY_EFFECTIVE
            };

            //Psychic
            Effectiveness[Psychic] = new Dictionary<PokemonType, float>
            {
                [Fighting] = SUPER_EFFECTIVE,
                [Poison] = SUPER_EFFECTIVE,
                [Steel] = NOT_VERY_EFFECTIVE,
                [Psychic] = NOT_VERY_EFFECTIVE,
                [Dark] = NO_EFFECT
            };

            //Ice
            Effectiveness[Ice] = new Dictionary<PokemonType, float>
            {
                [Flying] = SUPER_EFFECTIVE,
                [Ground] = SUPER_EFFECTIVE,
                [Steel] = NOT_VERY_EFFECTIVE,
                [Fire] = NOT_VERY_EFFECTIVE,
                [Water] = NOT_VERY_EFFECTIVE,
                [Grass] = SUPER_EFFECTIVE,
                [Ice] = NOT_VERY_EFFECTIVE,
                [Dragon] = SUPER_EFFECTIVE
            };

            //Dragon
            Effectiveness[Dragon] = new Dictionary<PokemonType, float>
            {
                [Steel] = NOT_VERY_EFFECTIVE,
                [Dragon] = SUPER_EFFECTIVE,
                [Fairy] = NO_EFFECT
            };

            //Dark
            Effectiveness[Dark] = new Dictionary<PokemonType, float>
            {
                [Fire] = NOT_VERY_EFFECTIVE,
                [Ghost] = SUPER_EFFECTIVE,
                [Psychic] = SUPER_EFFECTIVE,
                [Dark] = NOT_VERY_EFFECTIVE,
                [Fairy] = NOT_VERY_EFFECTIVE
            };

            //Fairy
            Effectiveness[Fairy] = new Dictionary<PokemonType, float>
            {
                [Fighting] = SUPER_EFFECTIVE,
                [Ground] = NOT_VERY_EFFECTIVE,
                [Steel] = NOT_VERY_EFFECTIVE,
                [Fire] = NOT_VERY_EFFECTIVE,
                [Dragon] = SUPER_EFFECTIVE,
                [Dark] = SUPER_EFFECTIVE
            };
        }
    }
}
