using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Enemy : Character
    {
        public string Type { get; set; }
        public int AreaRange { get; set; }
        public int LOSRange { get; set; }
        public List<CombatMove> Actions = new List<CombatMove>();
        public Enemy(string name,int _areaRange, int _losRange, string type, char ico, ConsoleColor col, int hp, Map map) : base(name, ico, col, hp, map)
        {
            Type = type;
            AreaRange = _areaRange;
            LOSRange = _losRange;
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
    }
}
