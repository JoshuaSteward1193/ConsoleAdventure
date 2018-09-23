using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Transition
    {
        public string[] Lines { get; set; }
        public int DelayTime { get; set; }

        public Transition(int time, string[] lines)
        {
            Lines = lines;
            DelayTime = time;
        }

        public void Show()
        {
            System.Threading.Thread.Sleep(DelayTime);
            Console.Clear();            
            foreach (string x in Lines)
            {
                Console.WriteLine(x);
                Console.WriteLine();
                Console.WriteLine();
                System.Threading.Thread.Sleep(DelayTime);
            }
            System.Threading.Thread.Sleep(DelayTime * 2);
            Console.Clear();
        }
    }
}
