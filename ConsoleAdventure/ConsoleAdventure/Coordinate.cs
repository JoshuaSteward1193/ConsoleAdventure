using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Coordinate
    {
        public int YVal { get; set; }
        public int XVal { get; set; }

        public Coordinate(int y, int x)
        {
            YVal = y;
            XVal = x;
        }

        public override bool Equals(object obj)
        {
            //Fail out early if null
            if(obj == null)
            {
                return false;
            }

            //Cast the object as a Coordinate and check if it has the same Y and X values
            Coordinate coord = obj as Coordinate;
            return (coord != null) && (YVal == coord.YVal) && (XVal == coord.XVal);
        }
        public bool Equals(Coordinate coord)
        {
            return (coord != null) && (YVal == coord.YVal) && (XVal == coord.XVal);
        }
    }
}
