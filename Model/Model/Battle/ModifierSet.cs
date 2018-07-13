using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class ModifierSet
    {
        private class Modifier : IModifier
        {
            private readonly ModifierSet owner;

            private readonly float factor;
            public float Factor { get { return factor; } }

            public Modifier(ModifierSet owner, float factor)
            {
                this.owner = owner;
                this.factor = factor;
            }

            public void Dispose()
            {
                owner.RemoveModifier(this);
            }
        }

        private SortedDictionary<int, List<IModifier>> modifiers;
        public float this[int level]
        {
            get
            {
                return modifiers[level].Sum(x => x.Factor);
            }
        }

        public ModifierSet()
        {
            modifiers = new SortedDictionary<int, List<IModifier>>();
        }

        public IModifier AddModifier(int level, float factor)
        {
            Modifier modifier = new Modifier(this, factor);
            if (!modifiers.ContainsKey(level))
            {
                modifiers[level] = new List<IModifier>();
            }
            modifiers[level].Add(modifier);
            return modifier;
        }

        public bool RemoveModifier(IModifier modifier)
        {
            bool result = false;

            List<int> toRemove = new List<int>();

            foreach (KeyValuePair<int, List<IModifier>> pair in modifiers)
            {
                if (pair.Value.Contains(modifier))
                {
                    pair.Value.Remove(modifier);
                    if (pair.Value.Count == 0) toRemove.Add(pair.Key);
                    result = true;
                    break;
                }
            }

            foreach (int level in toRemove)
            {
                modifiers.Remove(level);
            }

            return result;
        }

        public float Calculate(float baseValue)
        {
            float factor = 1.0f;
            foreach (KeyValuePair<int, List<IModifier>> pair in modifiers)
            {
                factor *= this[pair.Key];
            }
            return baseValue * factor;
        }
    }
}
