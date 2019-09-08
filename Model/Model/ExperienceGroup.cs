using System;

namespace PokemonEngine.Model
{
    public enum ExperienceGroup { Erratic, Fast, MediumFast, MediumSlow, Slow, Fluctuating }

    public static class ExpirienceGroupImpl
    {
        public static int ExperienceNeededForLevel(this ExperienceGroup expGroup, int level)
        {
            switch (expGroup)
            {
                case ExperienceGroup.Erratic:
                    return ExpErratic(level);
                case ExperienceGroup.Fast:
                    return ExpFast(level);
                case ExperienceGroup.MediumFast:
                    return ExpMediumFast(level);
                case ExperienceGroup.MediumSlow:
                    return ExpMediumSlow(level);
                case ExperienceGroup.Slow:
                    return ExpSlow(level);
                case ExperienceGroup.Fluctuating:
                    return ExpFluctuating(level);

            }
            throw new Exception($"Unrecognized ExperienceGroup type {expGroup.ToString()}");
        }

        private static int ExpErratic(int level)
        {
            if (level <= 50)
            {
                return (int)Math.Ceiling(Math.Pow(level, 3) * (100 - level) / 50);
            }
            if (level > 50 && level <= 68)
            {
                return (int)Math.Ceiling(Math.Pow(level, 3) * (150 - level) / 100);
            }
            if (level > 68 && level <= 98)
            {
                return (int)Math.Ceiling(Math.Pow(level, 3) * Math.Floor((1911 - 10 * level) / 3.0) / 500);
            }

            // level > 98
            return (int)Math.Ceiling(Math.Pow(level, 3) * (160 - level) / 100);
        }

        private static int ExpFast(int level)
        {
            return (int)Math.Ceiling(4 * Math.Pow(level, 3) / 5.0);
        }

        private static int ExpMediumFast(int level)
        {
            return (int)Math.Ceiling(Math.Pow(level, 3));
        }

        private static int ExpMediumSlow(int level)
        {
            return (int)Math.Ceiling(6.0 / 5.0 * Math.Pow(level, 3) - 15 * Math.Pow(level, 2) + 100 * level - 140);
        }

        private static int ExpSlow(int level)
        {
            return (int)Math.Ceiling(5.0 * Math.Pow(level, 3) / 4.0);
        }

        private static int ExpFluctuating(int level)
        {
            if (level <= 15)
            {
                return (int)Math.Ceiling(Math.Pow(level, 3) * ((Math.Floor((level + 1) / 3.0) + 24) / 50.0));
            }
            if (level > 15 && level <= 36)
            {
                return (int)Math.Ceiling(Math.Pow(level, 3) * (level + 14) / 50.0);
            }

            // level > 36
            return (int)Math.Ceiling(Math.Pow(level, 3) * ((Math.Floor(level / 2.0) + 32) / 50.0));
        }
    }
}
