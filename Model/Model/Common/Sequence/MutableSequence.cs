using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Common.Sequence
{
    public class MutableSequence<T> : IMutableSequence<T>
    {
        private IDictionary<T, LinkedListNode<T>> map;
        private LinkedList<T> list;

        public MutableSequence()
        {
            list = new LinkedList<T>();
            map = new Dictionary<T, LinkedListNode<T>>();
        }

        public MutableSequence(IEnumerable<T> enumerable) : this()
        {
            foreach (T obj in enumerable)
            {
                Add(obj);
            }
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public T First
        {
            get
            {
                return list.First.Value;
            }
        }

        public void Add(T item)
        {
            if (!map.ContainsKey(item))
            {
                map[item] = list.AddLast(item);
            }
        }

        public void Clear()
        {
            map.Clear();
            list.Clear();
        }

        public bool Contains(T obj)
        {
            return map.ContainsKey(obj);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            LinkedListNode<T> node = list.First;
            int i = arrayIndex;
            while (i < array.Length && node != null)
            {
                array[i] = node.Value;
                i++;
                node = node.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public T Next()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            if (map.ContainsKey(item))
            {
                list.Remove(map[item]);
                map.Remove(item);
                return true;
            }
            return false;
        }

        public void Replace(T existingObj, T newObj)
        {
            if (map.ContainsKey(existingObj))
            {
                map[existingObj].Value = newObj;
                map[newObj] = map[existingObj];
                map.Remove(existingObj);
            }
        }

        public void Swap(T firstObj, T secondObj)
        {
            if (map.ContainsKey(firstObj) && map.ContainsKey(secondObj))
            {
                map[firstObj].Value = secondObj;
                map[secondObj].Value = firstObj;

                LinkedListNode<T> tmp = map[firstObj];
                map[firstObj] = map[secondObj];
                map[secondObj] = tmp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
