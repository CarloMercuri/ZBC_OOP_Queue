using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace ZBC_OOP_Queue
{
    class Program
    {
        static string[] RandomNames { get; set; }

       

        static string LastKeyInput;

        private static bool isRunning;

        // Console size hack, makes it so you cannot resize it

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            // Size and lock the console. No scrolling, no resizing
            Console.SetWindowSize(140, 40);
            Console.SetBufferSize(140, 40);

            Console.Title = "Queue";
            LockConsole();

            InitiateRandomNames();

            GUI.InitializeGUI();
                        
            isRunning = true;
            UpdateLastKeyInput();

            while (isRunning)
            {
                GetUserInput();
            }

        }

        
        /// <summary>
        /// Works with user input
        /// </summary>
        static void GetUserInput()
        {
            ConsoleKeyInfo key =  Console.ReadKey();

            GUI.ClearArrow();

            switch (key.Key)
            {
                case ConsoleKey.D1:  // Add person
                    LastKeyInput = "1";

                    if (QueueControl.GetQueueCount() > 12) // max that can fit in the screen
                    {
                        GUI.ShowWarning("Too many in queue already!");
                        break;
                    }

                    QueueControl.QueuePerson(new Person(GetRandomName()));
                    GUI.PrintQueueVisual();
                    break;

                case ConsoleKey.D2:  // Remove first
                    LastKeyInput = "2";
                    Person p = QueueControl.GetFirstInQueue();

                    if(p != null)
                    {
                        GUI.ShowWarning($"Dequeued {p.Name}");
                    }

                    GUI.PrintQueueVisual();

                    break;

                case ConsoleKey.D3:  // Count
                    LastKeyInput = "3";
                    GUI.ShowWarning($"There are {QueueControl.GetQueueCount()}");
                    break;

                case ConsoleKey.D4:   // Find oldest and youngest
                    LastKeyInput = "4"; 
                    Person oldest = QueueControl.FindOldestInQueue();
                    Person youngest = QueueControl.FindYoungestInQueue();

                    GUI.ShowWarning($"The oldest is {oldest.Name}, {oldest.Age}.\n\r  The youngest is {youngest.Name}, {youngest.Age}");

                    break;

                case ConsoleKey.D5: // Search
                    LastKeyInput = "5";

                    SearchFunction();

                    break;

                case ConsoleKey.D6:  // Quit
                    LastKeyInput = "6";
                    isRunning = false;
                    break;
            }

            
            UpdateLastKeyInput();

            Console.SetCursorPosition(Console.WindowWidth - 1, 0);



        }


        static void SearchFunction()
        {
            GUI.ShowWarning("Search for people in queue with the name: ");

            string name = Console.ReadLine();

            int index = QueueControl.FindPersonInQueue(name);

            if(index == -1)
            {
                GUI.ShowWarning($"Person called {name} not found!");
            } else
            {
                GUI.ShowWarning($"Found {name}!");
                GUI.DrawArrowOnStickman(index);
            }
        }


        /// <summary>
        /// Updates the last key input
        /// </summary>
        static void UpdateLastKeyInput()
        {
            Console.SetCursorPosition(2, 24);
            Console.Write($"Last input: {LastKeyInput}");
          
        }

        /// <summary>
        /// Makes it so you cannot resize or maximize it
        /// </summary>
        private static void LockConsole()
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
        }

        /// <summary>
        /// Returns a random name from our list
        /// </summary>
        /// <returns></returns>
        private static string GetRandomName()
        {
            Random rand = new Random();
            return RandomNames[rand.Next(0, RandomNames.Length)];
        }

        private static void InitiateRandomNames()
        {
            RandomNames = new string[]
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
