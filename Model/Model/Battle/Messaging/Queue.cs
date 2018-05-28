using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Messaging
{
    public class Queue : IEnumerable<IMessage>
    {
        private Dictionary<IMessage, LinkedListNode<IMessage>> map;
        private LinkedList<IMessage> queue;
        private HashSet<ISubscriber> receivers;

        public IMessage First { get { return queue.First.Value; } }

        public Queue()
        {
            queue = new LinkedList<IMessage>();
            map = new Dictionary<IMessage, LinkedListNode<IMessage>>();
            receivers = new HashSet<ISubscriber>();
        }

        public int Count { get { return queue.Count; } }

        public bool HasNext { get { return queue.Count > 0; } }

        public bool AddSubscriber(ISubscriber receiver)
        {
            return receivers.Add(receiver);
        }

        public bool RemoveSubscriber(ISubscriber receiver)
        {
            return receivers.Remove(receiver);
        }

        public bool Contains(IMessage obj)
        {
            return map.ContainsKey(obj);
        }

        public IMessage Broadcast()
        {
            if (queue.Count > 0)
            {
                LinkedListNode<IMessage> node = queue.First;
                queue.RemoveFirst();
                foreach (ISubscriber receiver in receivers)
                {
                    node.Value.Dispatch(receiver);
                }
                map.Remove(node.Value);
                return node.Value;
            }
            throw new Exception("MessageQueue has no more messages remaining");
        }

        public void Flush()
        {
            while (HasNext)
            {
                Broadcast();
            }
        }

        public bool AddFirst(IMessage message)
        {
            if (!map.ContainsKey(message))
            {
                map[message] = queue.AddFirst(message);
                return true;
            }
            return false;
        }

        public bool Enqueue(IMessage message)
        {
            if (!map.ContainsKey(message))
            {
                map[message] = queue.AddLast(message);
                return true;
            }
            return false;
        }

        public bool Swap(IMessage firstMessage, IMessage secondMessage)
        {
            if (map.ContainsKey(firstMessage) && map.ContainsKey(secondMessage))
            {
                map[firstMessage].Value = secondMessage;
                map[secondMessage].Value = firstMessage;

                LinkedListNode<IMessage> tmp = map[firstMessage];
                map[firstMessage] = map[secondMessage];
                map[secondMessage] = tmp;

                return true;
            }
            return false;
        }

        public IEnumerator<IMessage> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }
    }
}
