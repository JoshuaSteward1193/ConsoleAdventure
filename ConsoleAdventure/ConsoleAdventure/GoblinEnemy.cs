using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class GoblinEnemy : Enemy
    {
        public GoblinEnemy(string name, int level, Map map, bool roam = true) : base(name,4, 10, "Goblin", 'g', ConsoleColor.Yellow, level, 0, 0, 0, map, roam)
        {
            StandardMove1 = GameData.AllCombatMoves[1];
            StandardMove2 = null;
            PowerMove1 = GameData.AllCombatMoves[0];
            SupportMove1 = null;            
            LevelScaler(level);
        }
        public GoblinEnemy(string name, int level, Map map, GameItem specialLoot, bool roam = true) : base(name, 4, 10, "Goblin", 'g', ConsoleColor.Yellow, level, 0, 0, 0, map, roam)
        {
            StandardMove1 = GameData.AllCombatMoves[1];
            StandardMove2 = null;
            PowerMove1 = GameData.AllCombatMoves[0];
            SupportMove1 = null;
            LevelScaler(level);
            Loot.Add(specialLoot);
        }

        public override int CombatChoice()
        {
            //If I have more than 50% health remaining:
            if(Convert.ToDouble(Health)/Convert.ToDouble(MaxHealth) > .5)
            {
                //20% chance for power move
                if (Program.rand.Next(0, 10) > 7) return 3;
                else return 1;
            }
            //If I have less than 50% health remaining
            else
            {
                //50% chance for power move
                if (Program.rand.Next(0, 10) > 4) return 3;
                else return 1;
            }
        }
        public override void LevelScaler(int level)
        {
            //Levels 1-5
            for(int i = 1; i <= 5 && i <= level; i++)
            {
                MaxHealth += 3;
                Strength += 2;
                Vigor += 1;
            }
            //Levels 6-10
            for(int i = 5; i <=10 && i <= level; i++)
            {
                MaxHealth += 5;
                Strength += 3;
                Vigor += 2;
            }
            //Levels > 10
            if(level > 10)
            {
                for (int i = level - 10; i <= level; i++)
                {
                    MaxHealth += 6;
                    Strength += 3;
                    Vigor += 3;
                }
            }
        }
    }
}
 