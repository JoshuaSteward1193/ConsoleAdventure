using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class InventoryController
    {
        private static bool[] selectedIndex;
        private static int indexer;
        
        public static void ShowInventory(Inventory inv)
        {
            if(inv.Items.Count > 0)
            {
                indexer = 0;
                selectedIndex = new bool[inv.Items.Count];
                selectedIndex[0] = true;
                for (int j = 1; j < selectedIndex.Length; j++)
                {
                    selectedIndex[j] = false;
                }
                PrintInv(inv);
                ControlInv(inv);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("There is nothing in the inventory");
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }

        public static void PrintLine(string name, bool selected, string desc)
        {
            if (selected)
            {
                Console.WriteLine($" > {name} | {desc}");                
            }
            else
            {
                Console.WriteLine($"   {name} | {desc}");                
            }            
        }

        public static void PrintInv(Inventory inv)
        {
            Console.Clear();
            Console.WriteLine("Use the arrow keys to select an item, and press Enter to use it. Press escape to go back.");
            GameItem item;
            for(int i = 0; i < inv.Items.Count; i++)
            {
                item = inv.Items[i];
                PrintLine(item.Name, selectedIndex[i], item.Description);
            }
            if (inv.Items.Count == 0)
            {
                Console.WriteLine("There is nothing in the inventory.");
            }
        }
        public static void ControlInv(Inventory inv)
        {
            bool goodInput = false;
            while (!goodInput)
            {                
                switch (Console.ReadKey(false).Key)
                {

                    case ConsoleKey.UpArrow:
                        if(indexer > 0)
                        {
                            selectedIndex[indexer] = false;
                            indexer--;
                            selectedIndex[indexer] = true;
                            PrintInv(inv);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if(indexer < selectedIndex.Length - 1)
                        {
                            selectedIndex[indexer] = false;
                            indexer++;
                            selectedIndex[indexer] = true;
                            PrintInv(inv);
                        }
                        break;
                    case ConsoleKey.Enter:
                        if(inv.Items.Count > 0)
                        {
                            Console.Clear();
                            inv.Items[indexer].UseItem();
                            if (inv.Items[indexer].Usable)
                            {
                                inv.Items.RemoveAt(indexer);
                            }
                            
                        }
                        goodInput = true;                        
                        break;
                    case ConsoleKey.Escape:
                        goodInput = true;
                        break;
                }
            }
        }
    }
}
