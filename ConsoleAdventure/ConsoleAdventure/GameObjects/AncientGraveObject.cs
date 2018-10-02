using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class AncientGraveObject : Interactable
    {
        public AncientGraveObject()
        {
            Name = "Ancient Grave";
            Description = "Someone was buried here a long time ago.";            
            Icon = SpecialChars.Cross;
            FColor = ConsoleColor.DarkGray;
            BColor = ConsoleColor.Black;
            Charges = -1;
            InteractionID = 0;
            MaintainanceID = 0;
        }
    }
}
