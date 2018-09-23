using System;

namespace ConsoleAdventure
{
    class Portal : Interactable
    {
        public Map Destination { get; set; }
        public Coordinate Spawn { get; set; }
        public Transition Action { get; set; }

        public Portal(string name, string desc, Transition act, Map dest, Coordinate spwn,  char ico, ConsoleColor color, ConsoleColor bColor = ConsoleColor.Black) : 
            base(name, desc, ico, -1, 0, 0, color, bColor)
        {
            Destination = dest;
            Spawn = spwn;
            Action = act;
        }

        public override void Interact()
        {
            Action.Show();
            Program.ChangeMap(Destination, Spawn);
        }
    }
}
