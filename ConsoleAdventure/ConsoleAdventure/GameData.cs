﻿using ConsoleAdventure.GameObjects;
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
        public static List<Map> AllMaps;
        public static List<Battlefield> AllBFields;
        public static List<Transition> Transitions;
        public static List<Interactable> AllInteractables = new List<Interactable>();
        public static List<CombatMove> AllCombatMoves = new List<CombatMove>();

        public static void DataBuild()
        {
            Console.WriteLine("Loading Resources...");
            //LOAD MAPS
            AllMaps = new List<Map>()
            {
                new Map("Test Room 1", LoadTerrain("TestRoom1"), new Coordinate(2, 2)),
                new Map("Goblin Lair", LoadTerrain("GoblinLair1"), new Coordinate(3, 8)),
                new Map("StoryRoom1", LoadTerrain("StoryRoom1"), new Coordinate(16, 50))
            };

            AllBFields = new List<Battlefield>()
            {
                new Battlefield("Battlefield 1", LoadTerrain("Battlefield1"), new Coordinate(3, 4))
            };            
            AllBFields[0].SpawnPoints.Add(new Coordinate(3, 8));

            //CREATE TRANSITIONS
            Transitions = new List<Transition>()
            {
                new Transition(3000, new string[]
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
                }),
                new Transition(3000, new string[]
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
                }),
                new Transition(3000, new string[]
                {
                    "You have survived your first ordeal",
                    "and now you have an open path in front of you.",
                    "From here, you could go anywhere. Nothing seems impossible now.",
                    "All that remains is to step forward.",
                    "",
                    "You leave the crumbling stone ruins behind you",
                    "and find yourself on a road heading south through a rocky ravine..."
                })
            };            
            


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
            

            AllMaps[2].TerrainData[22, 50].Thing = new LockedDoorObject("Locked Door", "This is a heavy slab of solid wood.", 1);
            AllMaps[2].TerrainData[30, 58].Thing = new LockedDoorObject("Locked Door", "This is a heavy slab of solid wood.", 2);
            AllMaps[2].TerrainData[30, 48].Thing = new LockedDoorObject("Locked Door", "This is a heavy slab of solid wood.", 2);
            AllMaps[2].TerrainData[18, 50].Thing = new Interactable("Cell Door", "This door is old and decaying.", SpecialChars.Door, 1, 1, 0, ConsoleColor.DarkYellow);
            AllMaps[2].TerrainData[19, 60].Thing = new ChestObject("Old Chest", "You open the lid on the chest. Press 'i' to open your inventory.", false, new Inventory(new AppleItem(), new AppleItem(), new AppleItem(), new AppleItem()));
            AllMaps[2].TerrainData[21, 60].Thing = new ChestObject("Old Chest", "You open the lid on the chest.", false, new Inventory(new DoorKey("Rusty Key", "It looks like it's been well-used.", 1)));
            AllMaps[2].TerrainData[23, 55].Thing = new ChestObject("Old Chest", "You open the lid on the chest.", false, new Inventory(new AppleItem(), new AppleItem(), new AppleItem(), new AppleItem()));
            AllMaps[2].TerrainData[23, 59].Thing = new Interactable("Cook Fire", "A fire is burning merrily here.", '^', 1, 2, 1, ConsoleColor.Red);
            AllMaps[2].TerrainData[29, 46].Thing = new ChestObject("Old Barrel", "You peek down into the barrel", false, new Inventory(new Weapon("Short Sword", "A short blade. Probably used by goblins, as well as against them.", 5, "sword")));
            //AllMaps[2].TerrainData[24, 24].Thing = new Portal("Open doorway", "This is the only exit to the structure.", Transitions[2], AllMaps[2],
            //    )

            //ADD COMBAT MOVES
            
            AllCombatMoves.Add(new CombatMove("Slam", "raise the club over your head, and bring it crashing down onto", "raises the club over its head, and brings it crashing down onto", 1.5, 0.7, CombatMove.SkillType.Club));
            AllCombatMoves.Add(new CombatMove("Smack", "wind up and deliver a horizontal blow to", "winds up and delivers a horizontal blow to", 1.0, 0.9, CombatMove.SkillType.Club));

            //ADD CHARACTERS
            AllMaps[0].TerrainData[5, 9].SpawnCharacter(new NPC("Hearst", SpecialChars.HatManRight, 50, AllMaps[0]));
            AllMaps[2].TerrainData[26, 50].SpawnCharacter(new GoblinEnemy("Bokka", 2, AllMaps[2]));
            AllMaps[2].TerrainData[31, 55].SpawnCharacter(new GoblinEnemy("Thugga", 2, AllMaps[2]));
            AllMaps[2].TerrainData[26, 55].SpawnCharacter(new GoblinEnemy("Feg", 2, AllMaps[2]));
            AllMaps[2].TerrainData[27, 66].SpawnCharacter(new GoblinEnemy("Harrak", 3, AllMaps[2]));
            AllMaps[2].TerrainData[30, 60].SpawnCharacter(new GoblinEnemy("Rogald", 4, AllMaps[2], new DoorKey("Prison Key", "A solid-looking key. It probably opens most doors here.",2), false));
            AllMaps[2].TerrainData[30, 40].SpawnCharacter(new GoblinEnemy("Duma", 4, AllMaps[2], false));

            Console.WriteLine("Done!");
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
                case "StoryRoom1":
                    currentText = Properties.Resources.StoryRoom1;
                    break;
                case "StoryRoom2":
                    currentText = Properties.Resources.StoryRoom2;
                    break;
                case "Battlefield1":
                    currentText = Properties.Resources.Battlefield1;
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
