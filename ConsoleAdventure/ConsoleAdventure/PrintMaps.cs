using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    static class PrintMaps
    {
        public static string SideBuffer = "                       ";
        public static void Print(Tile[,] _tiles, int YBuffer, int XBuffer, Coordinate _center, bool color)
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
                if (i >= _tiles.GetLength(0) - iOffset * 2)
                {
                    i = _tiles.GetLength(0) - iOffset * 2;
                }
            }
            else
            {
                i = 0;
                iOffset -= (_center.YVal - iOffset);
            }
            while (i < _tiles.GetLength(0) && i < _center.YVal + iOffset)
            {
                jOffset = XBuffer;
                if (_center.XVal - jOffset >= 0)
                {
                    j = _center.XVal - jOffset;
                    if (j >= _tiles.GetLength(1) - jOffset * 2)
                    {
                        j = _tiles.GetLength(1) - jOffset * 2;
                    }
                }
                else
                {
                    j = 0;
                    jOffset -= (_center.XVal - jOffset);
                }
                Console.Write(SideBuffer);
                sb.Clear();
                while (j < _tiles.GetLength(1) && j < _center.XVal + jOffset)
                {
                    char target = _tiles[i, j].Icon;
                    if (targetColor != _tiles[i, j].FColor) targetColor = _tiles[i, j].FColor;
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
                            if (targetColor != _tiles[i, j].FColor && color)
                            {
                                targetColor = _tiles[i, j].FColor;
                            }
                        }
                    }

                    if (color)
                    {
                        if (targetColor != Console.ForegroundColor)
                        {
                            Console.ForegroundColor = _tiles[i, j].FColor;
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
