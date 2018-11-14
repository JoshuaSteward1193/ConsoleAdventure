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
        string sideBuffer;
        public CombatManager(Player p, Enemy e)
        {
            player = p;
            enemy = e;
            SetBattlefield(Program.currentMap.TerrainData[e.Position.YVal, e.Position.XVal].Terrain);
            CombatMessage();
            Console.ReadKey(true);
            Console.Clear();

            //Set the size of the side buffer
            Program.SideBuffer = "";
            int bufferLength = (Console.WindowWidth - 13) / 2;
            for(int i = 0; i < bufferLength; i++)
            {
                Program.SideBuffer += " ";
            }

            //Play screen transition
            ScreenTransition.Transition();
            Combat();
        } 

        private void Combat()
        {
            Console.Clear();
            bField.TerrainData[bField.SpawnPoints[0].YVal, bField.SpawnPoints[0].XVal].SpawnCharacter(player);
            bField.TerrainData[bField.SpawnPoints[1].YVal, bField.SpawnPoints[1].XVal].SpawnCharacter(enemy);
            //Main Print Loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                
                PrintMaps.Print(bField.TerrainData, 4, 7, bField.SpawnPoints[0], true);
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
            int playerNameStartPos = Program.SideBuffer.Length - player.Name.Length - 5;
            int enemyNameStartPos = Program.SideBuffer.Length + bField.TerrainData.GetLength(1) + 5;
            string playerHealth = $"{player.Health} / {player.MaxHealth}";
            string enemyHealth = $"{enemy.Health} / {enemy.MaxHealth}";
            Console.SetCursorPosition(playerNameStartPos, 7);            
            Console.Write(player.Name);
            Console.SetCursorPosition(playerNameStartPos - (playerHealth.Length - player.Name.Length), 8);
            Console.Write(playerHealth);
            Console.SetCursorPosition(enemyNameStartPos, 7);
            Console.Write(enemy.Name);
            Console.SetCursorPosition(enemyNameStartPos, 8);
            Console.Write(enemyHealth);
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
        private void PrintCombat()
        {
            
        }
        
    }
}
