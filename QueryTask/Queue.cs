using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTask
{ 
    public class Queue<T> : IEnumerable<T>, IEnumerable
    {
        private T[] _array;
        private int size;
        private int capacity;
        private int head;
        private int tail;

        public Queue(int Size)
        {
            this.size = Size;
            this._array = new T[Size];
            this.head = -1;
            this.tail = 0;
        }

        /// <summary>
        /// Check is empty
        /// </summary>
        /// <returns></returns>
        public bool isEmpty()
        { 
            return size == 0;
        }

        /// <summary>
        /// Add element in Queue<T>
        /// </summary>
        /// <param name="newElement">Element to add</param>
        public void Enqueue(T newElement)
        {
            if (this.size == this.capacity)
            {
                T[] newQueue = new T[2 * capacity];
                Array.Copy(_array, 0, newQueue, 0, _array.Length);
                _array = newQueue;
                capacity *= 2;
            }
            size++;
            _array[tail++ % capacity] = newElement;
        }

        /// <summary>
        /// Delete element in Queue<T>
        /// </summary>
        public T Dequeue()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }
            size--;
            return _array[++head % capacity];
        }

        /// <summary>
        /// Count of Queue<T>
        /// </summary>
        public int Count
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// Iterator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new CustomIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Class for custom iterator
        /// </summary>
        public class CustomIterator : IEnumerator<T>
        {
            private readonly Queue<T> container;
            private int currentIndex;

            /// <summary>
            /// ctor
            /// </summary>
            public CustomIterator() { }

            /// <summary>
            /// ctor
            /// </summary>
            /// <param name="container"></param>
            public CustomIterator(Queue<T> container)
            {
                currentIndex = container.head - 1;
                this.container = container;
            }

            /// <summary>
            /// Current element
            /// </summary>
            public T Current
            {
                get
                {
                    if (currentIndex < 0 || currentIndex > container.tail)
                        throw new InvalidOperationException();
                    return container._array[currentIndex];
                }
            }

            /// <summary>
            /// Method for return current element
            /// </summary>
            object IEnumerator.Current => Current;

            public void Dispose()  {  }

            /// <summary>
            /// Сhecking for the existence of the next element
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                currentIndex = (currentIndex + 1);

                return currentIndex != container.tail;
            }

            /// <summary>
            /// Counter reset
            /// </summary>
            public void Reset()
            {
                currentIndex = container.head - 1;
            }
        }
    }

    
}
