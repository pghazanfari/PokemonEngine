using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Util
{
    //Do we want a variation where null is not allowed?
    public class UniqueList<T> : IList<T>
    {
        private readonly HashSet<T> set;
        private readonly List<T> list;

        private uint nullCount;

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

        public T this[int index]
        {
            get
            {
                return list[index];
            }

            set
            {
                if (!set.Contains(value))
                {
                    T item = list[index];
                    list[index] = value;
                    set.Remove(item);
                }

            }
        }

        public UniqueList()
        {
            nullCount = 0;
            list = new List<T>();
            set = new HashSet<T>();
        }

        public UniqueList(IEnumerable<T> enumerable)
        {
            nullCount = 0;
            set = new HashSet<T>();
            list = new List<T>();

            foreach (T item in enumerable) {
                if (item == null)
                {
                    list.Add(item);
                    nullCount++;
                }
                else
                {
                    if (set.Add(item))
                    {
                        list.Add(item);
                    }
                }
            }
        }

        public UniqueList(int capacity)
        {
            nullCount = 0;
            set = new HashSet<T>();
            list = new List<T>(capacity);
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (item == null)
            {
                list.Insert(index, item);
                nullCount++;
            }
            else
            {
                if (set.Add(item))
                {
                    list.Insert(index, item);
                }
            }
        }

        public void RemoveAt(int index)
        {
            if (list[index] == null) { nullCount--; }
            set.Remove(list[index]);
            list.RemoveAt(index);
        }

        public void Add(T item)
        {
            if (item == null)
            {
                list.Add(item);
                nullCount++;
            }
            else
            {
                if (!set.Add(item))
                {
                    list.Add(item);
                }
            }
        }

        public void Clear()
        {
            set.Clear();
            list.Clear();
            nullCount = 0;
        }

        public bool Contains(T item)
        {
            return (item == null && nullCount > 0) || set.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            if (set.Remove(item) || (item == null && nullCount > 0) )
            {
                return list.Remove(item);
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
