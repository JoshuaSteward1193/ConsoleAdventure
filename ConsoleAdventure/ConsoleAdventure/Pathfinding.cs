using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Pathfinding
    {
        /*
        public static Coordinate PlayerPos { get; set; }
        public static Coordinate GoalPos { get; set; }

        public static AStarNode TargetNode;
        public static AStarNode StartNode;
        */
        public static List<Tile> OpenList = new List<Tile>();
        public static List<Tile> ClosedList = new List<Tile>();

        public static List<Tile> returnList = new List<Tile>();

        public static List<Tile> AStar(Tile startTile, Tile goalTile, Map m)
        {
            Debug.WriteLine("Starting new Pathfinding!");
            OpenList.Clear();
            ClosedList.Clear();
            returnList.Clear();


            Tile currentTile = startTile;
            Console.WriteLine("Created Current Node");
            OpenList.Add(currentTile);
            //current = OpenNodes[0];
            while (OpenList.Count > 0)
            {
                
                //Find the node with the Lowest Fcost
                
                for (int i = 0; i < OpenList.Count; i++)
                {
                    //This complicated if statement basically looks for the tile in the list with the lowest fCost and breaks ties with hCost
                    //It then sets the current tile to the one with the lowest fCost
                    Console.WriteLine((OpenList[i].GetGcost(startTile) + OpenList[i].GetHcost(goalTile)));
                    if ((OpenList[i].GetGcost(startTile) + OpenList[i].GetHcost(goalTile)) < (currentTile.GetGcost(startTile) + currentTile.GetHcost(goalTile))
                        || (OpenList[i].GetGcost(startTile) + OpenList[i].GetHcost(goalTile)) == (currentTile.GetGcost(startTile) + currentTile.GetHcost(goalTile))
                        && OpenList[i].GetHcost(goalTile) < currentTile.GetHcost(goalTile))                    
                    {
                        currentTile = OpenList[i];
                    }
                }

                //Add current to closed list because we are looking at it now
                //Remove current from open list because we are looking at it now
                OpenList.Remove(currentTile);
                ClosedList.Add(currentTile);

                //If the current node has the same coordinates as the goal node, Mission Accomplished!
                if(currentTile == goalTile)
                {
                    break;
                }

                //TILE NEEDS GetNeighbors()
                List<Tile> neighbors = new List<Tile>();
                neighbors = m.GetNeighbors(currentTile.Coord.YVal, currentTile.Coord.XVal);

                foreach(Tile n in neighbors)
                {
                    if(n.Passable == false || ClosedList.Contains(n))
                    {
                        continue;
                    }

                    int MoveCostToNeighbor = CalculateManhattanDistance(currentTile, n);
                    if(MoveCostToNeighbor < n.GetGcost(startTile) || !OpenList.Contains(n))
                    {
                        n.Gcost = MoveCostToNeighbor;
                        n.Hcost = CalculateManhattanDistance(n, goalTile);
                        n.Parent = currentTile;

                        if (!OpenList.Contains(n))
                        {
                            OpenList.Add(n);
                        }
                    }
                }
                

            }
            RetracePath(startTile, goalTile); 
            return returnList;
        }
        static void RetracePath(Tile start, Tile end)
        {            
            Tile current = end;
            while(current != start)
            {
                returnList.Add(current);
                current = current.Parent;
            }
            returnList.Reverse();
        }
        
        public static int CalculateManhattanDistance(Tile tileA, Tile tileB)
        {
            if(tileA != null && tileB != null)
            {
                int x1 = tileA.Coord.XVal;
                int x2 = tileB.Coord.XVal;
                int y1 = tileA.Coord.YVal;
                int y2 = tileB.Coord.YVal;
                return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            }
            else
            {
                return 0;
            }
            
        }
    }
}
