using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BattleCity.Algorithms.Model
{
    public class PriorityQueue
    {
        public List<KeyValuePair<int, Point>> Heap;
        private readonly IComparer<int> _comparer;
        private const string IoeMessage = "Here";

        #region Constructors 

        public PriorityQueue()
            : this(Comparer<int>.Default)
        {
        }

        public PriorityQueue(IComparer<int> comparer)
        {
            Heap = new List<KeyValuePair<int, Point>>();
            _comparer = comparer ?? throw new ArgumentNullException();
        }

        #endregion

        public void Enqueue(KeyValuePair<int, Point> pair)
        {
            Heap.Add(pair);

            LastToFirstControl(Heap.Count - 1);
        }

        public void Enqueue(int priority, Point value)
        {
            var pair = new KeyValuePair<int, Point>(priority, value);
            Heap.Add(pair);

            LastToFirstControl(Heap.Count - 1);
        }

        public KeyValuePair<int, Point> Dequeue()
        {
            if (!IsEmpty)
            {
                var result = Heap[0];
                if (Heap.Count <= 1)
                {
                    Heap.Clear();
                }
                else
                {
                    Heap[0] = Heap[^1];
                    Heap.RemoveAt(Heap.Count - 1);
                    FirstToLastControl(0);
                }
                return result;
            }
            else
                throw new InvalidOperationException(IoeMessage);
        }

        public KeyValuePair<int, Point> Peek()
        {
            if (!IsEmpty)
                return Heap[0];
            else
                throw new InvalidOperationException(IoeMessage);
        }

        public int Count()
        {
            return Heap.Count;
        }

        public bool Contains(Point point)
        {
            return Heap.Any(keyValue => keyValue.Value.Equals(point));
        }

        public List<Point> GetHeapVariables()
        {
            return Heap.Select(t => t.Value).ToList();
        }
        public List<int> GetHeapCosts()
        {
            return Heap.Select(t => Convert.ToInt32(t.Key)).ToList();
        }

        public bool IsEmpty => Heap.Count == 0;

        private int LastToFirstControl(int position)
        {
            if (position >= Heap.Count)
                return -1;

            while (position > 0)
            {
                var parentPos = (position - 1) / 2;
                if (_comparer.Compare(Heap[parentPos].Key, Heap[position].Key) > 0)
                {
                    ExchangeElements(parentPos, position);
                    position = parentPos;
                }
                else break;
            }
            return position;
        }

        private void FirstToLastControl(int position)
        {
            if (position >= Heap.Count)
                return;

            while (true)
            {
                var smallestPosition = position;
                var leftPosition = 2 * position + 1;
                var rightPosition = 2 * position + 2;
                if (leftPosition < Heap.Count &&
                    _comparer.Compare(Heap[smallestPosition].Key, Heap[leftPosition].Key) > 0)
                    smallestPosition = leftPosition;
                if (rightPosition < Heap.Count &&
                    _comparer.Compare(Heap[smallestPosition].Key, Heap[rightPosition].Key) > 0)
                    smallestPosition = rightPosition;

                if (smallestPosition != position)
                {
                    ExchangeElements(smallestPosition, position);
                    position = smallestPosition;
                }
                else break;
            }
        }

        private void ExchangeElements(int position1, int position2)
        {
            var val = Heap[position1];
            Heap[position1] = Heap[position2];
            Heap[position2] = val;
        }
    }
}