using System;
using System.Collections.Generic;

namespace ConsoleAdventure
{
    class Character
    {
        public string Name { get; set; }
        public char Icon { get; set; }
        public bool Roams { get; set; }

        //COMBAT STATS
        public int Level { get; set; }
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
                if (health < 0) health = 0;                
                if (Health > MaxHealth) health = MaxHealth;
            }
        }
        private int maxHealth;
        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                maxHealth = value;
                Health = maxHealth;
            }
        }
        public int Strength { get; set; }
        public int Vigor { get; set; }

        virtual public Coordinate Position { get; set; }
        public ConsoleColor Color { get; set; }
        public Map MyMap { get; set; }
        public List<Tile> Path = new List<Tile>();

        public Character(string name, char ico, ConsoleColor col, int lvl, int hp, int str, int vig, Map map, bool roam)
        {
            Level = lvl;
            Name = name;
            Icon = ico;
            MaxHealth = hp;            
            Color = col;
            MyMap = map;
            Strength = str;
            Vigor = vig;
            Roams = roam;
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
        public void GoToTarget()
        {
            if(Path.Count > 0)
            {
                if(MyMap.TerrainData[Path[0].Coord.YVal, Path[0].Coord.XVal].Passable)
                {
                    MyMap.TerrainData[Path[0].Coord.YVal, Path[0].Coord.YVal].MoveCharacterTo(MyMap.TerrainData[Position.YVal, Position.XVal], this);
                    Path.RemoveAt(0);
                }                
            }
        }

    }
}
