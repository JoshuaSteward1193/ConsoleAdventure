using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Program
    {
        public static Random rand = new Random();
        public static Map currentMap;
        public static int DrawTime;
        public static Player p1;
        public static bool goodInput;
        public static bool moveInput;



        //USER PREFERENCES
        public static bool PrintColor = true;
        public static bool DebugGame = true;
        public static int YBuffer = 15; //Should be odd number
        public static int XBuffer = 41; //Should be odd number

        static void Main(string[] args)
        {
            //INITIAL LOAD
            GameData.DataBuild();
            ChangeMap(GameData.AllMaps[0]);
            p1 = new Player("Nemo", 'P', ConsoleColor.Cyan, 10);
            //Stopwatch for debug purposes
            Stopwatch sw = new Stopwatch();

            GameData.AllMaps[0].TerrainData[GameData.AllMaps[0].SpawnPoints[0].YVal, GameData.AllMaps[0].SpawnPoints[0].XVal].SpawnCharacter(p1);
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;            


            //INTRO
            Console.WriteLine("Press anykey to begin");
            Console.ReadKey();

            //TURN CYCLE
            while (p1.Health > 0)
            {
                Console.Clear();
                sw.Start();
                PrintMap(PrintColor);
                sw.Stop();
                DrawTime = Convert.ToInt32(sw.ElapsedMilliseconds);
                sw.Reset();                

                if (DebugGame)
                {
                    Console.WriteLine($"Map Draw Time: {DrawTime} ms");
                }

                PlayerInput();
            }

        }

        private static void PrintMap(bool color)
        {
            ConsoleColor targetColor = ConsoleColor.Black;
            StringBuilder sb = new StringBuilder();
            int i;
            int iOffset = 9;
            int j;
            int jOffset = 22;
            if (p1.Position.YVal - iOffset >= 0)
            {
                i = p1.Position.YVal - iOffset;
                if (i >= currentMap.TerrainData.GetLength(0) - iOffset * 2)
                {
                    i = currentMap.TerrainData.GetLength(0) - iOffset * 2;
                }
            }
            else
            {
                i = 0;
                iOffset -= (p1.Position.YVal - iOffset);
            }
            while (i < currentMap.TerrainData.GetLength(0) && i < p1.Position.YVal + iOffset)
            {
                jOffset = 22;
                if (p1.Position.XVal - jOffset >= 0)
                {
                    j = p1.Position.XVal - jOffset;
                    if (j >= currentMap.TerrainData.GetLength(1) - jOffset * 2)
                    {
                        j = currentMap.TerrainData.GetLength(1) - jOffset * 2;
                    }
                }
                else
                {
                    j = 0;
                    jOffset -= (p1.Position.XVal - jOffset);
                }
                    sb.Clear();
                    while (j < currentMap.TerrainData.GetLength(1) && j < p1.Position.XVal + jOffset)
                    {
                        char target = currentMap.TerrainData[i, j].Icon;
                    if (targetColor != currentMap.TerrainData[i, j].FColor) targetColor = currentMap.TerrainData[i, j].FColor;
                        if (sb.Length == 0)
                        {
                            sb.Append(target);
                        }
                        else
                        {
                            if (sb[sb.Length - 1] == target)
                            {
                                sb.Append(target);
                            }
                            else
                            {
                                Console.Write(sb);
                                sb.Clear();
                                sb.Append(target);
                                if (targetColor != currentMap.TerrainData[i, j].FColor && color)
                                {
                                    targetColor = currentMap.TerrainData[i, j].FColor;
                                }
                            }
                        }

                        if (color)
                        {
                            if (targetColor != Console.ForegroundColor)
                                Console.ForegroundColor = currentMap.TerrainData[i, j].FColor;
                        }
                    j++;
                }
                Console.WriteLine(sb);
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }          
        
        private static void BadPrint(bool color)
        {
            /*
            int i = 0;
            int j = 0;
            while (i < currentMap.TerrainData.GetLength(0))
            {
                j = 0;                
                while (j < currentMap.TerrainData.GetLength(1))
                {
                    char target = currentMap.TerrainData[i, j].Icon;
                    if (color)
                    {                        
                        Console.ForegroundColor = currentMap.TerrainData[i, j].FColor;
                    }
                    Console.Write(target);
                    j++;
                }
                Console.WriteLine();
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            */
        }
        
        private static void ChangeMap(Map x)
        {
            currentMap = x;
        }        
        
        private static void PlayerInput()
        {
            moveInput = false;
            while (!moveInput)
            {
                ConsoleKey UserInput = Console.ReadKey(false).Key;
                if (UserInput == ConsoleKey.UpArrow || UserInput == ConsoleKey.DownArrow ||
                    UserInput == ConsoleKey.LeftArrow || UserInput == ConsoleKey.RightArrow)
                {
                    PerformNavigation(UserInput);
                }
                else
                {
                    //Perform actions
                }
            }
            
        }
        private static void PerformNavigation(ConsoleKey key)
        {
            int targetY = p1.Position.YVal;
            int targetX = p1.Position.XVal;
            string failure = "";
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    targetY--;
                    failure = "north";
                    break;
                case ConsoleKey.DownArrow:
                    targetY++;
                    failure = "south";
                    break;
                case ConsoleKey.LeftArrow:
                    targetX--;
                    failure = "west";
                    break;
                case ConsoleKey.RightArrow:
                    targetX++;
                    failure = "east";
                    break;
            }
            if(currentMap.TerrainData[targetY, targetX].Passable)
            {
                currentMap.TerrainData[targetY, targetX].MoveCharacterTo(currentMap.TerrainData[p1.Position.YVal, p1.Position.XVal], p1);
                moveInput = true;
            }
            else
            {
                Console.WriteLine($"You cannot move {failure}. There is a {currentMap.TerrainData[targetY, targetX].Terrain} here.");
            }
        }
        private static void PerformActions()
        {

        }
    }
}
