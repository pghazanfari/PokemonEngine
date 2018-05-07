using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle.Messaging
{
    public class BattleMessageQueue : IEnumerable<IBattleMessage>
    {
        private Dictionary<IBattleMessage, LinkedListNode<IBattleMessage>> map;
        private LinkedList<IBattleMessage> queue;
        private HashSet<IBattleMessageSubscriber> receivers;

        public BattleMessageQueue()
        {
            queue = new LinkedList<IBattleMessage>();
            map = new Dictionary<IBattleMessage, LinkedListNode<IBattleMessage>>();
            receivers = new HashSet<IBattleMessageSubscriber>();
        }

        public int Count { get { return queue.Count; } }

        public bool HasNext { get { return queue.Count > 0; } }

        public bool AddSubscriber(IBattleMessageSubscriber receiver)
        {
            return receivers.Add(receiver);
        }

        public bool RemoveSubscriber(IBattleMessageSubscriber receiver)
        {
            return receivers.Remove(receiver);
        }

        public bool Contains(IBattleMessage obj)
        {
            return map.ContainsKey(obj);
        }

        public IBattleMessage Broadcast()
        {
            if (queue.Count > 0)
            {
                LinkedListNode<IBattleMessage> node = queue.First;
                queue.RemoveFirst();
                foreach (IBattleMessageSubscriber receiver in receivers)
                {
                    node.Value.Dispatch(receiver);
                }
                return node.Value;
            }
            throw new Exception("MessageQueue has no more messages remaining");
        }

        public bool Add(IBattleMessage message)
        {
            if (!map.ContainsKey(message))
            {
                queue.AddLast(message);
                return true;
            }
            return false;
        }

        public bool Swap(IBattleMessage firstMessage, IBattleMessage secondMessage)
        {
            if (map.ContainsKey(firstMessage) && map.ContainsKey(secondMessage))
            {
                map[firstMessage].Value = secondMessage;
                map[secondMessage].Value = firstMessage;

                LinkedListNode<IBattleMessage> tmp = map[firstMessage];
                map[firstMessage] = map[secondMessage];
                map[secondMessage] = tmp;

                return true;
            }
            return false;
        }

        public IEnumerator<IBattleMessage> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }
    }
}
