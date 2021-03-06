﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    abstract class Enemy : Character
    {
        public string Type { get; set; }
        public int AreaRange { get; set; }
        public int LOSRange { get; set; }
        public int ExperienceDrop { get; set; }

        public List<GameItem> Loot = new List<GameItem>();
        //Enemies have slots containing specific moves. The CombatChoice() method returns an int
        //based on which move should be performed, and the CombatManager interprets the returned int 
        //and determines which move should be performed.
        public CombatMove StandardMove1;
        public CombatMove StandardMove2;
        public CombatMove PowerMove1;
        public CombatMove SupportMove1;

        public Enemy(string name,int _areaRange, int _losRange, string type, char ico, ConsoleColor col, int level, int xpDrop, int hp, int str, int vig, Map map, bool roam) : base(name, ico, col, level, hp, str, vig, map, roam)
        {
            Type = type;
            AreaRange = _areaRange;
            LOSRange = _losRange;           
        }

        public void Die()
        {
            Program.PrintCenterLine($"{Name} falls to the ground, dead.");
            if(Loot.Count > 0)
            {
                foreach(GameItem x in Loot)
                {
                    Program.PrintCenterLine($"{Name} dropped a {x.Name}!");
                    Program.PlayerInventory.Items.Add(x);
                }
            }
            Program.currentMap.TerrainData[Position.YVal, Position.XVal].Resident = null;
            Program.currentMap.AICharacters.Remove(this);
        }
        public void OverrideLoot(GameItem[] items)
        {
            foreach(GameItem x in items)
            {
                Loot.Add(x);
            }
        }
        public void LookForPlayer()
        {
            bool seesPlayer = false;
            int _y = Position.YVal;
            int _x = Position.XVal;
            Map _map = Program.currentMap;

            //CHECK NORTH
            for(int i = 1; i <= LOSRange; i++)
            {
                if(_map.TerrainData[_y - i, _x].Resident == Program.p1)
                {
                    seesPlayer = true;
                    break;
                }
            }

            //CHECK SOUTH
            if (!seesPlayer)
            {
                for (int i = 1; i <= LOSRange; i++)
                {
                    if (_map.TerrainData[_y + i, _x].Resident == Program.p1)
                    {
                        seesPlayer = true;
                        break;
                    }
                }
            }

            //CHECK EAST
            if (!seesPlayer)
            {
                for (int i = 1; i <= LOSRange; i++)
                {
                    if (_map.TerrainData[_y, _x + i].Resident == Program.p1)
                    {
                        seesPlayer = true;
                        break;
                    }
                }
            }

            //CHECK WEST
            if (!seesPlayer)
            {
                for (int i = 1; i <= LOSRange; i++)
                {
                    if (_map.TerrainData[_y, _x - i].Resident == Program.p1)
                    {
                        seesPlayer = true;
                        break;
                    }
                }
            }

            //CHECK NEARBY
        }
        public abstract int CombatChoice();
        public abstract void LevelScaler(int level);
    }
}
