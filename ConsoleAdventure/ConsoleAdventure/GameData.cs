using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class GameData
    {
        public static List<Map> AllMaps = new List<Map>();

        public static void DataBuild()
        {
            //LOAD MAPS
            AllMaps.Add(new Map("Test Room 1", LoadTerrain("TestRoom1"), new Coordinate(2, 2)));

            //ADD OBJECTS
            AllMaps[0].TerrainData[8, 13].Thing = new Interactable("Old Stump", "the remains of an ancient tree",
                'o', -1, 0, 0, ConsoleColor.DarkYellow);
            AllMaps[0].TerrainData[20, 50].Thing = new Interactable("Ancient Grave", "the place where someone great " +
                "was buried in ages past", SpecialChars.Cross, -1, 0, 0, ConsoleColor.DarkGray);

        }
        public static Tile[,] LoadTerrain(string textFile)
        {
            
            string currentText;
            List<string> currentLines = new List<string>();

            //Decide which text file we need
            switch (textFile)
            {
                case "TestRoom1":
                    currentText = Properties.Resources.TestRoom1;
                    break;
                default:
                    currentText = Properties.Resources.TestRoom1;
                    break;
            }

            //Use StringReader to split the textfile into a list of lines
            StringReader sr = new StringReader(currentText);
            bool linesLeft = true;
            while (linesLeft)
            {
                string line = sr.ReadLine();
                if(line == null)
                {
                    linesLeft = false;
                }
                else
                {
                    currentLines.Add(line);
                }
            }

            //Create the Tile array we are going to return
            Tile[,] tileMap = new Tile[currentLines.Count, currentLines[0].Length];
            int i = 0;
            int j = 0;
            while(i < tileMap.GetLength(0))
            {
                j = 0;
                while(j < tileMap.GetLength(1))
                {
                    tileMap[i, j] = new Tile(currentLines[i][j], i, j);
                    j++;
                }
                i++;
            }

            //Return Tile Array
            return tileMap;
        }
        
    }
}
