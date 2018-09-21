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
        public static int ColorTime;
        public static int ColorLessTime;
        public static int BadColorTime;
        public static int BadColorLessTime;
        static void Main(string[] args)
        {
            //INITIAL LOAD
            GameData.DataBuild();
            ChangeMap(GameData.AllMaps[0]);
            Stopwatch sw = new Stopwatch();

            //INTRO
            Console.WriteLine("Press anykey to begin");
            Console.ReadKey();

            sw.Start();
            Console.WriteLine();
            BadPrint(false);
            sw.Stop();
            BadColorLessTime = Convert.ToInt32(sw.ElapsedMilliseconds);
            sw.Reset();

            sw.Start();
            Console.WriteLine();            
            PrintMap(false);
            sw.Stop();
            ColorLessTime = Convert.ToInt32(sw.ElapsedMilliseconds);
            sw.Reset();

            sw.Start();
            Console.WriteLine();
            BadPrint(true);
            sw.Stop();
            BadColorTime = Convert.ToInt32(sw.ElapsedMilliseconds);
            sw.Reset();

            sw.Start();
            Console.WriteLine();
            PrintMap(true);
            sw.Stop();
            ColorTime = Convert.ToInt32(sw.ElapsedMilliseconds);

            Console.WriteLine($"Printing map without StringBuilder or Color takes {BadColorLessTime} ms");
            Console.WriteLine($"Printing map with StringBuilder but without color takes {ColorLessTime} ms.");
            Console.WriteLine($"Printing map without StringBuilder but with color takes {BadColorTime} ms.");
            Console.WriteLine($"Printing map with  StringBuilder and color takes {ColorTime} ms");
            Console.ReadKey();

            //TURN CYCLE
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
