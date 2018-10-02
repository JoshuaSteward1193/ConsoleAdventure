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
        public static List<Transition> Transitions = new List<Transition>();
        public static List<Interactable> AllInteractables = new List<Interactable>();

        public static void DataBuild()
        {
            //LOAD MAPS
            AllMaps.Add(new Map("Test Room 1", LoadTerrain("TestRoom1"), new Coordinate(2, 2)));
            AllMaps.Add(new Map("Goblin Lair", LoadTerrain("GoblinLair1"), new Coordinate(3, 8)));

            //CREATE TRANSITIONS
            Transitions.Add(new Transition(3000, new string[]
            {
                "You cautiously approach the entrance to the tunnel",
                "The dank, moldy air wafts across your face from below",
                "From the depths, you hear a distant screech",
                "Steeling yourself, you duck your head and enter the cave",
                "The air is cold and clammy as you descend on slime-covered steps",
                "The light from above grows weaker as the descending stairs run deeper",
                "You see the glow of torchlight from somewhere far below you",
                "The screech sounds louder, but still somewhat distant",
                "Then the stairs give way to a landing, with a ladder leading further below",
                "You grasp the ladder and descend once again",
                "You find yourself in a small room, carved out of the bedrock",
                "You hear the screech again, and know it is close"
            }));
            Transitions.Add(new Transition(3000, new string[]
            {
                "Unable to bear the foul air",
                "dim lighting",
                "and oppressive weight of the earth above you any longer",
                "You scramble back up the wooden ladder",
                "You arrive on the stone landing, and hurry upwards",
                "As fast as the slimy stone stairs allow",
                "On the verge of panic, you continue upwards faster",
                "It seems as if you should have arrived at the surface long ago",
                "Panting hard, you fight back the panic and press it down inside",
                "Struggling aginst the magnetic pull of the depths below, you finally glimpse a light",
                "A few more stairs, and you can make out the bright sunshine streaming into the cave",
                "As you cross the threshold and re-enter the sun's bright light",
                "You hear a distant screech echoing up the tunnel behind you"
            }));
            //CREATE OBJECTS
            AllInteractables.Add(new Interactable("Mushroom Ring", "A ring-shaped group of mushrooms are " +
                "growing here", SpecialChars.MushroomRing, 1, 1, 0, ConsoleColor.Red));


            //ADD OBJECTS
            AllMaps[0].TerrainData[8, 13].Thing = new Interactable("Old Stump", "It is the remains of an ancient tree",
                'o', -1, 0, 0, ConsoleColor.DarkYellow);
            AllMaps[0].TerrainData[20, 50].Thing = new AncientGraveObject();
            AllMaps[0].TerrainData[21, 50].Thing = new Portal("Dark Cave", "This cave leads down into the darkness.",
                Transitions[0], AllMaps[1], AllMaps[1].SpawnPoints[0], SpecialChars.CaveEntrance, ConsoleColor.Gray);
            AllMaps[1].TerrainData[AllMaps[1].SpawnPoints[0].YVal, AllMaps[1].SpawnPoints[0].XVal].Thing = new Portal
                ("Wooden Ladder", "It leads back to the surface", Transitions[1], AllMaps[0], new Coordinate(21, 50), SpecialChars.LadderUp,
                ConsoleColor.DarkYellow);
            AllMaps[1].TerrainData[10, 5].Thing = new MushroomColonyObject();
            AllMaps[1].TerrainData[12, 12].Thing = new MushroomColonyObject();
            AllMaps[0].TerrainData[10, 15].Thing = new MushroomColonyObject();
            

            //ADD CHARACTERS
            AllMaps[0].TerrainData[5, 9].SpawnCharacter(new NPC("Hearst", SpecialChars.HatManRight, 50, AllMaps[0]));

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
                case "GoblinLair1":
                    currentText = Properties.Resources.GoblinLair1;
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
