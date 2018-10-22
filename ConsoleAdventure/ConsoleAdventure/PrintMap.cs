using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class PrintMap
    {
        public static void Print(Tile[,] _tiles, int YBuffer, int XBuffer, Coordinate _center)
        {
            ConsoleColor targetColor = ConsoleColor.Black;
            StringBuilder sb = new StringBuilder();
            int i;
            int iOffset = YBuffer;
            int j;
            int jOffset = XBuffer;
            if (_center.YVal - iOffset >= 0)
            {
                i = _center.YVal - iOffset;
                if (i >= currentMap.TerrainData.GetLength(0) - iOffset * 2)
                {
                    i = currentMap.TerrainData.GetLength(0) - iOffset * 2;
                }
            }
            else
            {
                i = 0;
                iOffset -= (_center.YVal - iOffset);
            }
            while (i < currentMap.TerrainData.GetLength(0) && i < _center.YVal + iOffset)
            {
                jOffset = XBuffer;
                if (_center.XVal - jOffset >= 0)
                {
                    j = _center.XVal - jOffset;
                    if (j >= currentMap.TerrainData.GetLength(1) - jOffset * 2)
                    {
                        j = currentMap.TerrainData.GetLength(1) - jOffset * 2;
                    }
                }
                else
                {
                    j = 0;
                    jOffset -= (_center.XVal - jOffset);
                }
                Console.Write(SideBuffer);
                sb.Clear();
                while (j < currentMap.TerrainData.GetLength(1) && j < p1.Position.XVal + jOffset)
                {
                    char target = currentMap.TerrainData[i, j].Icon;
                    if (targetColor != currentMap.TerrainData[i, j].FColor) targetColor = currentMap.TerrainData[i, j].FColor;
                    if (sb.Length == 0)
                    {
                        sb.Append(target);
                    }
                    else
                    {
                        if (sb[sb.Length - 1] == target)
                        {
                            sb.Append(target);
                        }
                        else
                        {
                            Console.Write(sb);
                            sb.Clear();
                            sb.Append(target);
                            if (targetColor != currentMap.TerrainData[i, j].FColor && color)
                            {
                                targetColor = currentMap.TerrainData[i, j].FColor;
                            }
                        }
                    }

                    if (color)
                    {
                        if (targetColor != Console.ForegroundColor)
                        {
                            Console.ForegroundColor = currentMap.TerrainData[i, j].FColor;
                        }
                    }
                    j++;
                }
                Console.WriteLine(sb);
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
