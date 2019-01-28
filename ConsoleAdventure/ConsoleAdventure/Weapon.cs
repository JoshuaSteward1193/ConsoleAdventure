using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Weapon : GameItem
    {
        List<CombatMove> Moves = new List<CombatMove>();
        int CombatPower;
        string Type;
        public Weapon(string _name, string _desc, int _power, string _type) : base(_name, _desc, 0, false)
        {
            CombatPower = _power;
            Type = _type;
            BuildMoveSet();
        }
        public override void UseItem()
        {
            Program.p1.EquippedWeapon = this;
            Program.p1.AssignedMoves.Clear();
            foreach (CombatMove x in Moves)
            {
                Program.p1.AssignedMoves.Add(x);
            }
        }
        
        private void BuildMoveSet()
        {
            switch (Name)
            {
                case "Short Sword":
                    Moves.Add(new CombatMove("Stab", "hold the blade pointing outwards and waist height and charge towards ", "levels the blade at you and charges towards ", 1.3, 0.85, CombatMove.SkillType.Shortsword));
                    Moves.Add(new CombatMove("Slice", "swing quickly with the blade towards ", "swings its blade towards ", 1.0, 0.9, CombatMove.SkillType.Shortsword));
                    Moves.Add(new CombatMove("Cleave", "raise the blade high over your head, and bring it swiftly down on ", "raises the blade high in the air, and swings downwards towards ", 1.4, 0.8, CombatMove.SkillType.Shortsword));
                    break;
            }
        }
    }
}
