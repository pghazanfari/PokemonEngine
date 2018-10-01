using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public class ModifierSet : IDisposable
    {
        private SortedDictionary<int, List<IModifier>> modifiers;

        public ModifierSet()
        {
            modifiers = new SortedDictionary<int, List<IModifier>>();
        }

        public void AddModifier(int level, IModifier modifier)
        {
            if (!modifiers.ContainsKey(level))
            {
                modifiers[level] = new List<IModifier>();
            }
            modifiers[level].Add(modifier);
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
                factor *= levelFactor(pair.Key);
            }
            return baseValue * factor;
        }

        private float levelFactor(int level)
        {
            return modifiers[level].Sum(x => x.Factor);
        }
        
        public void Dispose()
        {
            foreach (KeyValuePair<int, List<IModifier>> pair in modifiers)
            {
                foreach (IModifier modifier in pair.Value)
                {
                    modifier.Dispose();
                }
                pair.Value.Clear();
            }
        }
    }
}
