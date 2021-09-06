﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBC_OOP_Queue
{
    public class QueueControl
    {
        private QueueData _data;
        private string[] randomNames;
        public QueueControl()
        {
            _data = new QueueData();
            InitiateRandomNames();
        }

        /// <summary>
        /// Returns how many people are in the queue.
        /// </summary>
        /// <returns></returns>
        public int GetQueueCount()
        {
            return _data.GetQueueCount();
        }

        /// <summary>
        /// Returns the queue
        /// </summary>
        /// <returns></returns>
        public Queue<Person> GetQueue()
        {
            return _data.GetQueue();
        }

        /// <summary>
        /// Returns the first person in queue
        /// </summary>
        /// <returns></returns>
        public Person GetFirstInQueue()
        {
            if(_data.GetQueueCount() > 0)
            {
                return _data.GetFirstInQueue();
            } else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Adds a person to the queue
        /// </summary>
        /// <param name="p"></param>
        public void QueuePerson(Person p)
        {
            _data.AddToQueue(p);
        }

        /// <summary>
        /// Finds the index of someone in the queue. Returns -1 if unsuccessful
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindPersonInQueue(string name)
        {
            // Had to cast it to a list because it's a bit clumsy to find the index
            // with a queue. 

            List<Person> list = _data.GetQueue().ToList();

            return list.FindIndex(x => x.Name == name);
        }

        /// <summary>
        /// Returns the first Person with the lowest age
        /// </summary>
        /// <returns></returns>
        public Person FindYoungestInQueue()
        {
            // Not using LINQ.max because i want the person object as well

            int temp = 500;

            Person returnPerson = null;

            // Yes there might be multiple with the same age. Let's just play dumb alright?
            // It returns the first one with the lowest age

            foreach (Person p in _data.GetQueue())
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
        public Person FindOldestInQueue()
        {
            // Not using LINQ.max because i want the person object as well

            int temp = 0;

            Person returnPerson = null;

            // Yes there might be multiple with the same age. Let's just play dumb alright?
            // It returns the first one with the highest age

            foreach(Person p in _data.GetQueue())
            {
                if(p.Age > temp)
                {
                    temp = p.Age;
                    returnPerson = p;
                }
            }

            return returnPerson;
        }

        /// <summary>
        /// Returns a random name from our list
        /// </summary>
        /// <returns></returns>
        public string GetRandomName()
        {
            Random rand = new Random();
            return randomNames[rand.Next(0, randomNames.Length)];
        }

        private void InitiateRandomNames()
        {
            randomNames = new string[]
            {
                "Noah",
                "Liam",
                "Mason",
                "Jacob",
                "William",
                "Ethan",
                "James",
                "Alexander",
                "Michael",
                "Benjamin",
                "Elijah",
                "Daniel",
                "Aiden",
                "Logan",
                "Matthew",
                "Lucas",
                "Jackson",
                "David",
                "Oliver",
                "Jayden",
                "Joseph",
                "Amya",
                "Iliana",
                "Jaida",
                "Paloma",
                "Asia",
                "Louisa",
                "Sarahi",
                "Tara",
                "Andi",
                "Arden",
                "Dalary",
                "Aimee",
                "Alisson",
                "Halle",
                "Aitana",
                "Landry",
                "Alisha",
                "Elin",
                "Maliah",
                "Belen",
                "Briley",
                "Raina",
                "Vienna",
                "Esperanza",
                "Judith",
                "Faye",
            };
        }
    }
}
