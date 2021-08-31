using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBC_OOP_Queue
{
    public static class GUI
    {
        // Colors
        private static ConsoleColor textColor = ConsoleColor.Yellow;
        private static ConsoleColor columnsColor = ConsoleColor.Green;
        private static ConsoleColor backColor = ConsoleColor.Gray;

        // Stickmen stuff
        private static int StickmenSeparation = 9;
        private static int StickmenStartXPos = 110;
        private static int ArrowX { get; set; }
        private static int ArrowY = 30;

        // Other

        private static int WarningAreaY = 28;




        /// <summary>
        /// Gets the UI started with the basic drawing
        /// </summary>
        public static void InitializeGUI()
        {
            CreateBaseGUI();
        }

        /// <summary>
        /// Creates the Base GUI
        /// </summary>
        private static void CreateBaseGUI()
        {
            PrintTitleLogo();

            PrintMenu();

            PrintDoor(120, 28);
        }

        /// <summary>
        /// Draws the stickman figures in queue
        /// </summary>
        public static void PrintQueueVisual()
        {
            // Clear first
            ClearQueueVisual();

            int xPos = StickmenStartXPos;

            // Prints a stickman for each person in queue.
            // Goes from right to left, hence why we are counting down
            foreach (Person p in QueueControl.MainQueue)
            {
                PrintStickFigure(p, xPos, 32, p.Color);
                xPos -= StickmenSeparation;
            }

        }

        /// <summary>
        /// Clears the stickman queue
        /// </summary>
        public static void ClearQueueVisual()
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
        public static void PrintStickFigure(Person p, int x, int y, ConsoleColor color)
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

            ConsoleTools.PrintArray(array, x, y, null, color);
        }

        /// <summary>
        /// Clears the area to make it ready for the next message
        /// </summary>
        public static void ClearWarningArea()
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
        public static void ShowWarning(string text)
        {
            // Clear first
            ClearWarningArea();

            Console.SetCursorPosition(2, WarningAreaY);
            Console.Write(text);
        }

        public static void ClearArrow()
        {
            // We know where it is
            Console.SetCursorPosition(ArrowX, ArrowY);
            Console.Write("         ");
            Console.SetCursorPosition(ArrowX, ArrowY + 1);
            Console.Write("         ");
        }

        public static void DrawArrowOnStickman(int index)
        {
            ClearArrow();

            // Find the right location
            ArrowX = StickmenStartXPos - (StickmenSeparation * index) + 3;

            string[] array = new string[]
            {
                @" |",
                @"\/",
            };

            ConsoleTools.PrintArray(array, ArrowX, ArrowY, null, ConsoleColor.White);

        }

        /// <summary>
        /// Do I really need to comment this one? Look at it :P 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void PrintDoor(int x, int y)
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

            ConsoleTools.PrintArray(array, x, y, null, ConsoleColor.White);
        }

        /// <summary>
        /// Prints the main menu
        /// </summary>
        public static void PrintMenu()
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

            ConsoleTools.PrintArray(array, 2, 16, null, ConsoleColor.White);

        }

        /// <summary>
        /// Prints the main LUX sign
        /// </summary>
        private static void PrintTitleLogo()
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

        private static void PrintNightclub(int x, int y, ConsoleColor color)
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

        private static void PrintLux()
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

        private static void PrintColumns()
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
        private static void PrintSingleColumn(int x, int y, string[] array, ConsoleColor color)
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
    }
}
