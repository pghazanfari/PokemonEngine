using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Weathers
{
    public class Fog : Weather
    {
        public const int AccuracyModifierLevel = 0;
        public const float AccuracyModifierFactor = 0.6f;

        private readonly List<IModifier> modifiers;

        public Fog() : base()
        {
            modifiers = new List<IModifier>();        
        }
        public Fog(int turnCount) : base(turnCount)
        {
            modifiers = new List<IModifier>();
        }

        public override void OnUseMove(object sender, UseMoveEventArgs args)
        {
            modifiers.Add(args.Action.AccuracyModifiers.AddModifier(AccuracyModifierLevel, AccuracyModifierFactor));
        }

        public override void OnChangeWeather(object sender, ChangeWeatherEventArgs args)
        {
            if (args.Battle.CurrentWeather == this)
            {
                modifiers.ForEach(x => x.Dispose());
                modifiers.Clear();
            }
        }
    }
}
