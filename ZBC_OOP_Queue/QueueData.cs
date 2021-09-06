using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBC_OOP_Queue
{
    public class QueueData
    {
        private static Queue<Person> MainQueue;

        public QueueData()
        {
            MainQueue = new Queue<Person>();
        }

        /// <summary>
        /// Adds a person to the queue
        /// </summary>
        /// <param name="p"></param>
        public void AddToQueue(Person p)
        {
            MainQueue.Enqueue(p);
        }

        /// <summary>
        /// Returns the queue
        /// </summary>
        /// <returns></returns>
        public Queue<Person> GetQueue()
        {
            return MainQueue;
        }

        /// <summary>
        /// Dequeues the first in queue
        /// </summary>
        /// <returns></returns>
        public Person GetFirstInQueue()
        {
            return MainQueue.Dequeue();
        }

        /// <summary>
        /// Returns how many people are in the queue.
        /// </summary>
        /// <returns></returns>
        public int GetQueueCount()
        {
            return MainQueue.Count;
        }
    }
}
