using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Messaging
{
    public class MessageQueue<T> : IEnumerable<T> where T : IMessage<T>
    {
        private Dictionary<T, LinkedListNode<T>> map;
        private LinkedList<T> queue;

        private HashSet<IMessageReceiver<T>> receivers;

        public MessageQueue()
        {
            queue = new LinkedList<T>();
            map = new Dictionary<T, LinkedListNode<T>>();
            receivers = new HashSet<IMessageReceiver<T>>();
        }

        public int Count { get { return queue.Count; } }

        public bool HasNext { get { return queue.Count > 0; } }

        public bool AddReceiver(IMessageReceiver<T> receiver)
        {
            return receivers.Add(receiver);
        }

        public bool RemoveReceiver(IMessageReceiver<T> receiver)
        {
            return receivers.Remove(receiver);
        }

        public bool Contains(T obj)
        {
            return map.ContainsKey(obj);
        }

        public virtual bool Enqueue(T obj)
        {
            if (!map.ContainsKey(obj))
            {
                map[obj] = queue.AddLast(obj);
                return true;
            }
            return false;
        }

        public virtual T Broadcast()
        {
            if (queue.Count > 0)
            {
                LinkedListNode<T> node = queue.First;
                queue.RemoveFirst();
                foreach (IMessageReceiver<T> receiver in receivers)
                {
                    node.Value.Dispatch(receiver);
                }
                return node.Value;
            }
            throw new Exception("MessageQueue has no more messages remaining");
        } 

        public virtual bool InjectAfter(T message, T newMessage)
        {
            if (map.ContainsKey(message))
            {
                queue.AddAfter(map[message], newMessage);
            }
            return false;
        }

        public virtual bool InjectBefore(T message, T newMessage)
        {
            if (map.ContainsKey(message))
            {
                queue.AddBefore(map[message], newMessage);
            }
            return false;
        }

        public virtual bool Replace(T message, T newMessage)
        {
            if (map.ContainsKey(message))
            {
                queue.AddAfter(map[message], newMessage);
                queue.Remove(map[message]);
                map.Remove(message);
                return true;
            }
            return false;
        }

        public virtual bool Remove(T message)
        {
            if (map.ContainsKey(message))
            {
                queue.Remove(map[message]);
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }
    }
}
