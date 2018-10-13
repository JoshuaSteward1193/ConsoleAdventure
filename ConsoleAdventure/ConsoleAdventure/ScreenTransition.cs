using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    static class ScreenTransition
    {
        static StringBuilder print = new StringBuilder(Console.WindowWidth);
        
        public static void Transition()
        {
            int index = 0;
            
            while(index != Console.WindowWidth - 1)
            {
                Console.Clear();
                print.Insert(index, 'X');
                index++;
                print.Insert(index, 'X');
                index++;
                print.Insert(index, 'X');
                index++;
                for (int i = 0; i < Console.WindowHeight - 1; i++)
                {
                    Console.WriteLine(print);
                    Console.WriteLine(print);
                    Console.WriteLine(print);
                    Console.WriteLine(print);
                }
                
                System.Threading.Thread.Sleep(300);
            }
            
            Console.ReadKey(true);
        }
    }
}
