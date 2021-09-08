using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZBC_OOP_Queue
{
    public class GUI
    {
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

        // Colors
        private ConsoleColor textColor = ConsoleColor.Yellow;
        private ConsoleColor columnsColor = ConsoleColor.Green;
        private ConsoleColor backColor = ConsoleColor.Gray;

        // Stickmen stuff
        private int StickmenSeparation = 9;
        private int StickmenStartXPos = 110;
        private int ArrowX { get; set; }
        private int ArrowY = 30;

        // Other
        static string LastKeyInput;

        private int WarningAreaY = 28;

        private QueueControl _control;



        private bool isRunning;

        /// <summary>
        /// Gets the UI started with the basic drawing
        /// </summary>
        public void InitializeGUI()
        {

            // Size and lock the console. No scrolling, no resizing
            Console.SetWindowSize(140, 40);
            Console.SetBufferSize(140, 40);

            Console.Title = "Queue";
            LockConsole();

            _control = new QueueControl();
            isRunning = true;
            UpdateLastKeyInput();
            CreateBaseGUI();


            while (isRunning)
            {
                GetUserInput();
            }
        }

        /// <summary>
        /// Works with user input
        /// </summary>
        private void GetUserInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            ClearArrow();

            switch (key.Key)
            {
                case ConsoleKey.D1:  // Add person
                    LastKeyInput = "1";

                    if (_control.GetQueueCount() > 12) // max that can fit in the screen
                    {
                        ShowWarning("Too many in queue already!");
                        break;
                    }

                    _control.QueuePerson(new Person(_control.GetRandomName()));
                    PrintQueueVisual();
                    break;

                case ConsoleKey.D2:  // Remove first
                    LastKeyInput = "2";
                    Person p = _control.GetFirstInQueue();

                    if (p != null)
                    {
                        ShowWarning($"Dequeued {p.Name}");
                    }

                    PrintQueueVisual();

                    break;

                case ConsoleKey.D3:  // Count
                    LastKeyInput = "3";
                    ShowWarning($"There are {_control.GetQueueCount()}");
                    break;

                case ConsoleKey.D4:   // Find oldest and youngest
                    LastKeyInput = "4";
                    Person oldest = _control.FindOldestInQueue();
                    Person youngest = _control.FindYoungestInQueue();

                    ShowWarning($"The oldest is {oldest.Name}, {oldest.Age}.\n\r  The youngest is {youngest.Name}, {youngest.Age}");

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

        /// <summary>
        /// Start the search function
        /// </summary>
        private void SearchFunction()
        {
            ShowWarning("Search for people in queue with the name: ");

            string name = Console.ReadLine();

            int index = _control.FindPersonInQueue(name);

            if (index == -1)
            {
                ShowWarning($"Person called {name} not found!");
            }
            else
            {
                ShowWarning($"Found {name}!");
                DrawArrowOnStickman(index);
            }
        }

        /// <summary>
        /// Updates the last key input
        /// </summary>
        private void UpdateLastKeyInput()
        {
            Console.SetCursorPosition(2, 24);
            Console.Write($"Last input: {LastKeyInput}");

        }


        /// <summary>
        /// Creates the Base GUI
        /// </summary>
        private void CreateBaseGUI()
        {
            PrintTitleLogo();

            PrintMenu();

            PrintDoor(120, 28);
        }

        /// <summary>
        /// Draws the stickman figures in queue
        /// </summary>
        public void PrintQueueVisual()
        {
            // Clear first
            ClearQueueVisual();

            int xPos = StickmenStartXPos;

            // Prints a stickman for each person in queue.
            // Goes from right to left, hence why we are counting down
            foreach (Person p in _control.GetQueue())
            {
                PrintStickFigure(p, xPos, 32, p.Color);
                xPos -= StickmenSeparation;
            }

        }

        /// <summary>
        /// Clears the stickman queue
        /// </summary>
        public void ClearQueueVisual()
        {
            for (int i = 0; i < 8; i++)
            {
                // Simple
                Console.SetCursorPosition(0, 32 + i);
                Console.Write("                                                                                                                        ");
            }
        }

        /// <summary>
        /// Prints a single stick figure
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void PrintStickFigure(Person p, int x, int y, ConsoleColor color)
        {
            string name = p.Name;

            // Doesen't fit otherwise
            if (name.Length > 6)
            {
                name = name.Substring(0, 6);
            }

            // I could center better the name under the figure using TextEditor, but
            // i think it's out of the scope of this assignment

            string[] array = new string[]
            {
                @"   0",
                @"  /|\",
                @"   | ",
                @"  / \",
                @" /   \",
                $" {name}",
                $"  {p.Age} "
            };

            PrintArray(array, x, y, null, color);
        }

        /// <summary>
        /// Clears the area to make it ready for the next message
        /// </summary>
        public void ClearWarningArea()
        {
            for (int i = 0; i < 4; i++)
            {
                // Simple
                Console.SetCursorPosition(0, WarningAreaY + i);
                Console.Write("                                                                       ");
            }
        }

        /// <summary>
        /// Displays a text message at the location
        /// </summary>
        /// <param name="text"></param>
        public void ShowWarning(string text)
        {
            // Clear first
            ClearWarningArea();

            Console.SetCursorPosition(2, WarningAreaY);
            Console.Write(text);
        }

        public void ClearArrow()
        {
            // We know where it is
            Console.SetCursorPosition(ArrowX, ArrowY);
            Console.Write("         ");
            Console.SetCursorPosition(ArrowX, ArrowY + 1);
            Console.Write("         ");
        }

        public void DrawArrowOnStickman(int index)
        {
            ClearArrow();

            // Find the right location
            ArrowX = StickmenStartXPos - (StickmenSeparation * index) + 3;

            string[] array = new string[]
            {
                @" |",
                @"\/",
            };

            PrintArray(array, ArrowX, ArrowY, null, ConsoleColor.White);

        }

        /// <summary>
        /// Do I really need to comment this one? Look at it :P 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PrintDoor(int x, int y)
        {
            string[] array = new string[]
            {
                @" __________",
                @"|  __  __  |",
                @"| |  ||  | |",
                @"| |  ||  | |",
                @"| |__||__| |",
                @"|  __  __()|",
                @"| |  ||  | |",
                @"| |  ||  | |",
                @"| |__||__| |",
                @"|__________|",
            };

            PrintArray(array, x, y, null, ConsoleColor.White);
        }

        /// <summary>
        /// Prints the main menu
        /// </summary>
        public void PrintMenu()
        {
            string[] array = new string[]
            {
                @"1 - Add person in queue",
                @"2 - Allow the first person in queue in",
                @"3 - Count how many are in queue",
                @"4 - Show oldest and youngest in queue",
                @"5 - Find person in queue",
                @"6 - Exit program",
            };

            PrintArray(array, 2, 16, null, ConsoleColor.White);

        }

        /// <summary>
        /// Prints the main LUX sign
        /// </summary>
        private void PrintTitleLogo()
        {
            Console.ForegroundColor = backColor;

            // Background lines
            int startX = 12;
            string line = @" _____________________________________________________________________________________________________________";
            string lineSlash = @"\____________________________________________________________________________________________________________\";

            Console.SetCursorPosition(startX, 0);

            Console.WriteLine(line);

            // Little 3D
            for (int i = 1; i < 9; i++)
            {
                Console.SetCursorPosition(startX, i);

                Console.Write("|");

                Console.SetCursorPosition(startX + i, i);
                Console.Write(lineSlash);
            }

            Console.SetCursorPosition(startX + 1, 8);
            Console.Write("_______");


            PrintColumns();
            PrintLux();
            Console.ForegroundColor = ConsoleColor.White;
            PrintNightclub(30, 9, ConsoleColor.Red);



        }

        private void PrintNightclub(int x, int y, ConsoleColor color)
        {
            string[] array = new string[]
            {
                @"        _      __   __      __     __              __                       ",
                @"  ___  (_)__ _/ /  / /_____/ /_ __/ /    ___ ___  / /________ ____  _______ ",
                @" / _ \/ / _ `/ _ \/ __/ __/ / // / _ \  / -_) _ \/ __/ __/ _ `/ _ \/ __/ -_)",
                @"/_//_/_/\_, /_//_/\__/\__/_/\_,_/_.__/  \__/_//_/\__/_/  \_,_/_//_/\__/\__/ ",
                @"       /___/                                                                ",

            };

            Console.ForegroundColor = color;

            // Gonna print every line of the array, skipping if it's not a relevant character
            for (int i = 0; i < array.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);

                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == ' ')
                    {
                        // If we need to skip this, we'll increment the cursor position manually by 1
                        var pos = Console.GetCursorPosition();

                        Console.SetCursorPosition(pos.Left + 1, pos.Top);
                    }
                    else
                    {
                        Console.Write(array[i][j]);
                    }
                }
            }
        }

        private void PrintLux()
        {
            string[] textArray = new string[]
            {
                  @"/\\\",
                  @"\/\\\",
                  @" \/\\\",
                  @"  \/\\\______________/\\\____/\\\__/\\\____/\\\",
                  @"   \/\\\_____________\/\\\___\/\\\_\///\\\/\\\/",
                  @"    \/\\\_____________\/\\\___\/\\\___\///\\\/",
                  @"     \/\\\_____________\/\\\___\/\\\____/\\\/\\\",
                  @"      \/\\\\\\\\\\\\\\\_\//\\\\\\\\\___/\\\/\///\\\",
                  @"       \///////////////___\/////////___\///____\///",
            };


            Console.ForegroundColor = textColor;

            int originXPos = 44;
            Console.SetCursorPosition(originXPos, 0);

            // Gonna print every line of the array, skipping if it's not a relevant character
            for (int i = 0; i < textArray.Length; i++)
            {
                Console.SetCursorPosition(originXPos, i);

                for (int j = 0; j < textArray[i].Length; j++)
                {
                    if (textArray[i][j] == '_' || textArray[i][j] == ' ')
                    {
                        // If we need to skip this, we'll increment the cursor position manually by 1
                        var pos = Console.GetCursorPosition();

                        Console.SetCursorPosition(pos.Left + 1, pos.Top);
                    }
                    else
                    {
                        Console.Write(textArray[i][j]);
                    }
                }
            }
        }

        /// <summary>
        /// Prints the side columns
        /// </summary>
        private void PrintColumns()
        {
            string[] array = new string[]
            {
                @"/\\\__/\\\__/\\\",
                @"\/\\\_\/\\\_\/\\\",
                @" \/\\\_\/\\\_\/\\\",
                @"  \///__\///__\///"
            };

            int leftXPos = 14;
            int rightXPos = 104;
            int bottomBlocksOffset = 5;

            // There's 4 of these
            PrintSingleColumn(leftXPos, 0, array, columnsColor);
            PrintSingleColumn(leftXPos + bottomBlocksOffset, 5, array, columnsColor);
            PrintSingleColumn(rightXPos, 0, array, columnsColor);
            PrintSingleColumn(rightXPos + bottomBlocksOffset, 5, array, columnsColor);

        }

        /// <summary>
        /// Prints a single column
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="array"></param>
        /// <param name="color"></param>
        private void PrintSingleColumn(int x, int y, string[] array, ConsoleColor color)
        {

            Console.ForegroundColor = color;

            // Gonna print every line of the array, skipping if it's not a relevant character
            for (int i = 0; i < array.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);

                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == '_' || array[i][j] == ' ')
                    {
                        // If we need to skip this, we'll increment the cursor position manually by 1
                        var pos = Console.GetCursorPosition();

                        Console.SetCursorPosition(pos.Left + 1, pos.Top);
                    }
                    else
                    {
                        Console.Write(array[i][j]);
                    }
                }
            }
        }

        /// <summary>
        /// Prints the given string array to the console in the specified location, skipping over eventual blacklisted characters. Blacklist can be null.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="blacklist"></param>
        public void PrintArray(string[] array, int x, int y, List<char> blacklist, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            if (color == ConsoleColor.Black)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            for (int i = 0; i < array.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);

                for (int j = 0; j < array[i].Length; j++)
                {
                    if (blacklist != null && blacklist.Contains(array[i][j]))
                    {
                        // If we need to skip this, we'll increment the cursor position manually by 1
                        var pos = Console.GetCursorPosition();

                        Console.SetCursorPosition(pos.Left + 1, pos.Top);

                    }
                    else
                    {
                        Console.Write(array[i][j]);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
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
    }
}
