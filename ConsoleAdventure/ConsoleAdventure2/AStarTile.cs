using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAdventure2
{
    class AStarTile
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public AStarTile Parent;
    }
}
