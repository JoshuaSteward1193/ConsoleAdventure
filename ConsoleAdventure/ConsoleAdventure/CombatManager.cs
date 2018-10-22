using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class CombatManager
    {
        Player player;
        Enemy enemy;

        Battlefield bField;
        
        public CombatManager(Player p, Enemy e)
        {
            player = p;
            enemy = e;
            SetBattlefield(Program.currentMap.TerrainData[e.Position.YVal, e.Position.XVal].Terrain);
            CombatMessage();
            Combat();
        } 

        private void Combat()
        {
            Console.Clear();

            //Main Print Loop
            while (true)
            {
                PrintHeader();
                Console.ReadKey();
            }
            
            

        }
        private void CombatMessage()
        {
            Console.WriteLine($"Starting combat between the player, {player.Name}, and {enemy.Name} the {enemy.Type}");
        }
        private void PrintHeader()
        {
            Console.WriteLine("");
            Console.WriteLine($"     {player.Name}                                   {enemy.Name} the {enemy.Type}");
        }
        private void SetBattlefield(string type)
        {
            switch (type)
            {
                case "Stone Structure":
                    bField = GameData.AllBFields[0];
                    break;
                default:
                    bField = GameData.AllBFields[0];
                    break;
            }
        }
        
    }
}
