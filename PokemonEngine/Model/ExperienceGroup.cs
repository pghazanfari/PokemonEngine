using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    return expErratic(level);
                case ExperienceGroup.Fast:
                    return expFast(level);
                case ExperienceGroup.MediumFast:
                    return expMediumFast(level);
                case ExperienceGroup.MediumSlow:
                    return expMediumSlow(level);
                case ExperienceGroup.Slow:
                    return expSlow(level);
                case ExperienceGroup.Fluctuating:
                    return expFluctuating(level);

            }
            throw new Exception($"Unrecognized ExperienceGroup type {expGroup.ToString()}");
        }

        private static int expErratic(int level)
        {
            if (level <= 50)
            {
                return (int)Math.Ceiling( (Math.Pow(level, 3) * (100 - level)) / 50);
            }
            if (level > 50 && level <= 68)
            {
                return (int)Math.Ceiling((Math.Pow(level, 3) * (150 - level)) / 100);
            }
            if (level > 68 && level <= 98)
            {
                return (int)Math.Ceiling(Math.Pow(level, 3) * Math.Floor((1911 - 10 * level) / 3.0) / 500);
            }

            // level > 98
            return (int)Math.Ceiling((Math.Pow(level, 3) * (160 - level)) / 100);
        }

        private static int expFast(int level)
        {
            return (int)Math.Ceiling(4 * Math.Pow(level, 3) / 5.0);
        }

        private static int expMediumFast(int level)
        {
            return (int)Math.Ceiling(Math.Pow(level, 3));
        }

        private static int expMediumSlow(int level)
        {
            return (int)Math.Ceiling((6.0 / 5.0) * Math.Pow(level, 3) - 15 * Math.Pow(level, 2) + 100 * level - 140);
        }

        private static int expSlow(int level)
        {
            return (int)Math.Ceiling(5.0 * Math.Pow(level, 3) / 4.0);
        }

        private static int expFluctuating(int level)
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
