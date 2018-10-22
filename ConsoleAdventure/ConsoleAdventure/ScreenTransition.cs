using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    static class ScreenTransition
    {
        static StringBuilder LeftLine = new StringBuilder();
        static StringBuilder RightLine = new StringBuilder();

        public static void Transition()
        {
            int index = 0;
            bool leftAlign = true;
            LeftLine.Append(' ', Console.WindowWidth - 1);
            RightLine.Append(' ', Console.WindowWidth - 1);
            Console.Clear();

            while(index != Console.WindowWidth)
            {
                Console.Clear();
                //Add 10 characters to each array
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;
                if (index == Console.WindowWidth - 1) break;
                LeftLine[index] = 'X';
                RightLine[RightLine.Length - index - 1] = 'X';
                index++;

                //Print on all lines of the console
                for (int i = 0; i < Console.WindowHeight; i++)
                {                    
                    if (i % 3 == 0)
                    {
                        leftAlign = !leftAlign;
                    }

                    if (leftAlign) Console.WriteLine(LeftLine);
                    else Console.WriteLine(RightLine);
                }
                leftAlign = true;
                System.Threading.Thread.Sleep(75);
            }
            //Console.ReadKey(true);
        }   
        
    }
}
