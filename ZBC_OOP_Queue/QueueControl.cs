using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBC_OOP_Queue
{
    public static class QueueControl
    {
        public static Queue<Person> MainQueue = new Queue<Person>();

        /// <summary>
        /// Returns how many people are in the queue.
        /// </summary>
        /// <returns></returns>
        public static int GetQueueCount()
        {
            return MainQueue.Count;
        }

        /// <summary>
        /// Returns the first person in queue
        /// </summary>
        /// <returns></returns>
        public static Person GetFirstInQueue()
        {
            if(MainQueue.Count > 0)
            {
                return MainQueue.Dequeue();
            } else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Adds a person to the queue
        /// </summary>
        /// <param name="p"></param>
        public static void QueuePerson(Person p)
        {
            MainQueue.Enqueue(p);
        }

        /// <summary>
        /// Finds the index of someone in the queue. Returns -1 if unsuccessful
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int FindPersonInQueue(string name)
        {
            // Had to cast it to a list because it's a bit clumsy to find the index
            // with a queue. 

            List<Person> list = MainQueue.ToList();

            return list.FindIndex(x => x.Name == name);
        }

        /// <summary>
        /// Returns the first Person with the lowest age
        /// </summary>
        /// <returns></returns>
        public static Person FindYoungestInQueue()
        {
            // Not using LINQ.max because i want the person object as well

            int temp = 500;

            Person returnPerson = null;

            // Yes there might be multiple with the same age. Let's just play dumb alright?
            // It returns the first one with the lowest age

            foreach (Person p in MainQueue)
            {
                if (p.Age < temp)
                {
                    temp = p.Age;
                    returnPerson = p;
                }
            }

            return returnPerson;
        }

        /// <summary>
        /// Returns the first Person with the oldest age
        /// </summary>
        /// <returns></returns>
        public static Person FindOldestInQueue()
        {
            // Not using LINQ.max because i want the person object as well

            int temp = 0;

            Person returnPerson = null;

            // Yes there might be multiple with the same age. Let's just play dumb alright?
            // It returns the first one with the highest age

            foreach(Person p in MainQueue)
            {
                if(p.Age > temp)
                {
                    temp = p.Age;
                    returnPerson = p;
                }
            }

            return returnPerson;
        }
    }
}
