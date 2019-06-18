using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Player : Character
    {
        public List<DoorKey> KeyRing = new List<DoorKey>();
        public List<CombatMove> AssignedMoves = new List<CombatMove>();
        public Weapon EquippedWeapon;

        public int XP { get; set; }
        public Player(string name, char ico, ConsoleColor col, int hp) : base(name, ico, col, 1, hp, 5,5, null, false)
        {
            EquippedWeapon = null;            
        }
        public void EquipStartingWeapon(Weapon weap)
        {
            weap.UseItem();
        }
        public void SetHealthTo(int _value)
        {
            Health = _value;
        }
    }
}
