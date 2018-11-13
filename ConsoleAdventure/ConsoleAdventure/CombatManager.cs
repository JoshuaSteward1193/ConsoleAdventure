﻿using System;
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
            Console.WriteLine("");
            Console.WriteLine($"                 {player.Name}                                {enemy.Name} the {enemy.Type}");
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