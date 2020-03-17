using System.Collections.Generic;
using System.Linq;

namespace Frankenstein.Utils
{
    public enum QueuePriority
    {
        high   = 0,
        medium = 1,
        low    = 2
    }

    public interface IPriorityQueue<T>
    {
        T Dequeue();
        T Dequeue(QueuePriority prio);
        void Enqueue(T value, QueuePriority prio);

        T Peek();
        T Peek(QueuePriority prio);
        int Total_size { get; }
        
        void Clear(QueuePriority prio);
    }

    internal class PriorityQueue<T> : IPriorityQueue<T>
    {
        private int                                       _total_size;
        private SortedDictionary<QueuePriority, List<T>> _storage;

        private PriorityQueue()
        {
        }
        
        public static IPriorityQueue<T> Create()
        {
            var result = new PriorityQueue<T>();
            result._storage    = new SortedDictionary<QueuePriority, List<T>>();
            result._total_size = 0;

            return result;
        }

        private bool IsEmpty()
        {
            return this._total_size == 0;
        }


        #region IPriorityQueue

        int IPriorityQueue<T>.Total_size
        {
            get { return this._total_size; }
        }


        T IPriorityQueue<T>.Dequeue()
        {
            if (this.IsEmpty())
            {
                return default(T);
            }
            else
                foreach (List<T> q in this._storage.Values)
                {
                    if (q.Count > 0)
                    {
                        this._total_size--;
                        return this.DequeLast(q);
                    }
                }

            return default(T);
        }
        
        T IPriorityQueue<T>.Peek()
        {
            if (this.IsEmpty())
                return default(T);
            else
                foreach (List<T> q in this._storage.Values)
                {
                    if (q.Count > 0)
                        return q.LastOrDefault();
                }

            return default(T);
        }

        T IPriorityQueue<T>.Peek(QueuePriority prio)
        {
            if (!this._storage.ContainsKey(prio))
                return default(T);
            
            return this._storage[prio].LastOrDefault();
        }

        T IPriorityQueue<T>.Dequeue(QueuePriority prio)
        {
            if (!this._storage.ContainsKey(prio))
                return default(T);
            
            this._total_size--;
            return this.DequeLast(this._storage[prio]);
        }

        void IPriorityQueue<T>.Enqueue(T item, QueuePriority prio)
        {
            if (!this._storage.ContainsKey(prio))
            {
                this._storage.Add(prio, new List<T>());
            }

            this._storage[prio].Add(item);
            this._total_size++;
        }

        /// <summary>
        /// Given list is always > 0
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        T DequeLast(IList<T> list)
        {
            if (list.Count == 0)
                return default(T);
            
            var lastIndex = list.Count - 1;
            var element = list[lastIndex];
            list.RemoveAt(lastIndex);
            return element;
        }

        void IPriorityQueue<T>.Clear(QueuePriority prio)
        {
            if (!this._storage.ContainsKey(prio))
                return;
            
            this._storage[prio].Clear();
        }

        #endregion
    }
}