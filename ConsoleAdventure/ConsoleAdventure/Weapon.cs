using System.Collections.Generic;


namespace ConsoleAdventure
{
    class Weapon : GameItem
    {
        List<CombatMove> Moves = new List<CombatMove>();
        public int CombatPower;
        public string Type;
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
            Program.PrintCenterLine($"You equipped a {this.Name}");
        }
        public void EnemyEquip()
        {
            //TODO
        }
        
        private void BuildMoveSet()
        {
            switch (Name)
            {
                case "Fists":
                    Moves.Add(new CombatMove("Punch", "curl your hand into a fist and punch", "curls its hand into a fist and punches", 0.9, 0.9, CombatMove.SkillType.Fist));
                    Moves.Add(new CombatMove("Fist Strike", "rear back, and with all of your might you drive your fist into", "rears back, and drives its fist into", 1.5, 0.7, CombatMove.SkillType.Fist));
                    Moves.Add(new CombatMove("Jab", "deliver a quick punch to", "delivers a quick punch to", 0.8, 0.99, CombatMove.SkillType.Fist));
                    break;
                case "Short Sword":
                    Moves.Add(new CombatMove("Stab", "hold the blade pointing outwards at waist height and charge towards ", "levels the blade at you and charges towards ", 1.3, 0.85, CombatMove.SkillType.Shortsword));
                    Moves.Add(new CombatMove("Slice", "swing quickly with the blade towards ", "swings its blade towards ", 1.0, 0.9, CombatMove.SkillType.Shortsword));
                    Moves.Add(new CombatMove("Cleave", "raise the blade high over your head, and bring it swiftly down on ", "raises the blade high in the air, and swings downwards towards ", 1.4, 0.8, CombatMove.SkillType.Shortsword));
                    break;
            }
        }
    }
}
