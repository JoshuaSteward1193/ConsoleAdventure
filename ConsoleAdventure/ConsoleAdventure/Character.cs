﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Character
    {
        public string Name { get; set; }
        public char Icon { get; set; }
        private int health;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
                if (health < 0)
                {
                    health = 0;
                }
            }
        }
        virtual public Coordinate Position { get; set; }
        public ConsoleColor Color { get; set; }
        public Map MyMap { get; set; }

        public Character(string name, char ico, ConsoleColor col, int hp, Map map)
        {
            Name = name;
            Icon = ico;
            Health = hp;            
            Color = col;
            MyMap = map;

            Type type = this.GetType();


            if (type != typeof(Player))
            {
                MyMap.AICharacters.Add(this);
            }
            
        }
        
        public void Wander()
        {
            int val = Program.rand.Next(0, 6);
            Tile target = null;
            switch (val)
            {
                case 0:
                    target = Program.currentMap.TerrainData[Position.YVal - 1, Position.XVal];
                    break;
                case 1:
                    target = Program.currentMap.TerrainData[Position.YVal + 1, Position.XVal];
                    break;
                case 2:
                    target = Program.currentMap.TerrainData[Position.YVal, Position.XVal + 1];
                    break;
                case 3:
                    target = Program.currentMap.TerrainData[Position.YVal, Position.XVal - 1];
                    break;
                default:
                    target = null;
                    break;
            }
            if (target != null && target.Passable)
            {
                target.MoveCharacterTo(Program.currentMap.TerrainData[Position.YVal, Position.XVal], this);
            }
        }

    }
}
