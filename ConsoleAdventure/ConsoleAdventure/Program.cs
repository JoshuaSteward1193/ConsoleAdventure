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

        //USER PREFERENCES
        public static bool PrintColor = true;
        public static bool DebugGame = true;

        static void Main(string[] args)
        {
            //INITIAL LOAD
            GameData.DataBuild();
            ChangeMap(GameData.AllMaps[0]);
            p1 = new Player("Nemo", 'P', ConsoleColor.Cyan, 10);
                //Stopwatch for debug purposes
            Stopwatch sw = new Stopwatch();

            GameData.AllMaps[0].TerrainData[GameData.AllMaps[0].SpawnPoints[0].YVal, GameData.AllMaps[0].SpawnPoints[0].XVal].SpawnCharacter(p1);

            //INTRO
            Console.WriteLine("Press anykey to begin");
            Console.ReadKey();             

            //TURN CYCLE
            while(p1.Health > 0)
            {
                Console.Clear();
                sw.Start();
                PrintMap(true);
                sw.Stop();
                DrawTime = Convert.ToInt32(sw.ElapsedMilliseconds);
                sw.Reset();

                if (DebugGame)
                {
                    Console.WriteLine($"Map Draw Time: {DrawTime} ms");
                }
                Console.ReadKey();
            }
            
        }

        private static void PrintMap(bool color)
        {
            int i = 0;
            int j = 0;
            StringBuilder sb = new StringBuilder();
            ConsoleColor targetColor = ConsoleColor.Black;
            while (i < currentMap.TerrainData.GetLength(0))
            {
                j = 0;
                sb.Clear();
                while(j < currentMap.TerrainData.GetLength(1))
                {
                    char target = currentMap.TerrainData[i, j].Icon;                    
                    if(sb.Length == 0)
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
                            if (targetColor != currentMap.TerrainData[i, j].FColor)
                            {
                                targetColor = currentMap.TerrainData[i, j].FColor;
                            }
                        }
                    }
                    
                    if (color)
                    {
                        if(targetColor != Console.ForegroundColor)
                        Console.ForegroundColor = currentMap.TerrainData[i, j].FColor;
                    }
                    
                    //Console.Write(target);
                    j++;
                }
                Console.WriteLine(sb);
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void BadPrint(bool color)
        {
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
        }
        private static void ChangeMap(Map x)
        {
            currentMap = x;
        }
    }
}
