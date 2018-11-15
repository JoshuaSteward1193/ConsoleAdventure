using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
	class Program
	{
		public static Random rand = new Random();
        public static Stopwatch sw;
		public static Map currentMap;
		public static int DrawTime;
		public static Player p1;
        public static Inventory PlayerInventory;
		public static bool goodInput;
		public static bool moveInput;



		//USER PREFERENCES
		public static bool PrintColor = true;
		public static bool DebugGame = true;
		public static int YBuffer = 9; //Should be odd number
		public static int XBuffer = 21; //Should be odd number
        public static string SideBuffer = "                       ";


        static void Main(string[] args)
		{
			//INITIAL LOAD
			GameData.DataBuild();
			currentMap = GameData.AllMaps[2];
			p1 = new Player("Nemo", 'P', ConsoleColor.Cyan, 10);
            p1.SetHealthTo(6);
            PlayerInventory = new Inventory();
			//Stopwatch for debug purposes
			sw = new Stopwatch();

			currentMap.TerrainData[currentMap.SpawnPoints[0].YVal, currentMap.SpawnPoints[0].XVal].SpawnCharacter(p1);
			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
            Console.WindowWidth = 91;
            Console.WindowHeight = 33;
            ConsoleLockdown.Lockdown();

            //ASTAR PATHFINDING TEST - BROKEN
            //currentMap.AICharacters[0].Path = Pathfinding.AStar(currentMap.TerrainData[currentMap.AICharacters[0].Position.YVal,currentMap.AICharacters[0].Position.XVal], currentMap.TerrainData[10, 10], currentMap);


            //INTRO
            Console.WriteLine("Welcome to the adventure.");
            Console.WriteLine("For best results, please right-click on the title bar and set your font to Courier New.");
            Console.WriteLine("Thank you for playing my game.");
			Console.WriteLine("Press anykey to begin");
			Console.ReadKey();

            ScreenTransition.Transition();

			//TURN CYCLE
			while (p1.Health > 0)
			{								
                CharacterAI();

                DrawEverything();
                
                PlayerInput();

			}

		}
        private static void DrawEverything()
        {
            Console.Clear();
            sw.Start();

            //Set the side buffer
            SideBuffer = "";
            int bufferLength = (Console.WindowWidth - XBuffer * 2) / 2;
            for (int i = 0; i < bufferLength; i++)
            {
                SideBuffer += " ";
            }
            PrintHeader();            
            PrintMap(PrintColor);
            sw.Stop();
            DrawTime = Convert.ToInt32(sw.ElapsedMilliseconds);
            sw.Reset();

            if (DebugGame)
            {
                Console.Write(SideBuffer);
                Console.WriteLine($"Map Draw Time: {DrawTime} ms");
            }
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }
		private static void PrintMap(bool color)
		{
            PrintMaps.Print(currentMap.TerrainData, YBuffer, XBuffer, p1.Position, color);            
		}          
		
		private static void BadPrint(bool color)
		{
			/*
			int i = 0;
			int j = 0;
			while (i < currentMap.TerrainData.GetLength(0))
			{
				j = 0;                
				while (j < currentMap.TerrainData.GetLength(1))
				{
					char target = currentMap.TerrainData[i, j].Icon;
					if (color)
					{                        
						Console.ForegroundColor = currentMap.TerrainData[i, j].FColor;
					}
					Console.Write(target);
					j++;
				}
				Console.WriteLine();
				i++;
			}
			Console.ForegroundColor = ConsoleColor.Gray;
			*/
		}

        private static void PrintHeader()
        {
            Console.WriteLine();
            Console.Write(SideBuffer);
            Console.WriteLine($"Player: {p1.Name} | Health: {p1.Health}/{p1.MaxHealth} | Location: {currentMap.TerrainData[p1.Position.YVal, p1.Position.XVal].Terrain}");
            Console.WriteLine();
        }
		
		public static void ChangeMap(Map x, Coordinate spawn)
		{
            x.TerrainData[spawn.YVal, spawn.XVal].MoveCharacterTo(currentMap.TerrainData[p1.Position.YVal, p1.Position.XVal], p1);
            currentMap = x;
            PrintMap(PrintColor);
		}        
		
		private static void PlayerInput()
		{
            Console.CursorVisible = true;
			moveInput = false;
            goodInput = false;
            
            while (!moveInput && !goodInput)
            {
                ConsoleKey UserInput = ConsoleKey.X;
                if (Console.KeyAvailable)
                {
                    UserInput = Console.ReadKey(false).Key;
                }
                
                if (UserInput == ConsoleKey.UpArrow || UserInput == ConsoleKey.DownArrow ||
					UserInput == ConsoleKey.LeftArrow || UserInput == ConsoleKey.RightArrow)
				{
					PerformNavigation(UserInput);
				}
				else
				{
                    PerformActions(UserInput);
                    
				}
                Console.CursorVisible = false;
			}
			
		}
		private static void PerformNavigation(ConsoleKey key)
		{
			int targetY = p1.Position.YVal;
			int targetX = p1.Position.XVal;
			string failure = "";
			switch (key)
			{
				case ConsoleKey.UpArrow:
					targetY--;
					failure = "north";
					break;
				case ConsoleKey.DownArrow:
					targetY++;
					failure = "south";
					break;
				case ConsoleKey.LeftArrow:
					targetX--;
					failure = "west";
					break;
				case ConsoleKey.RightArrow:
					targetX++;
					failure = "east";
					break;
			}
			if(currentMap.TerrainData[targetY, targetX].Passable)
			{
				currentMap.TerrainData[targetY, targetX].MoveCharacterTo(currentMap.TerrainData[p1.Position.YVal, p1.Position.XVal], p1);
				moveInput = true;
			}
			else
			{
                if(currentMap.TerrainData[targetY, targetX].Thing != null)
                {
                    Console.WriteLine($"This is a {currentMap.TerrainData[targetY, targetX].Thing.Name}. " +
                        $"{currentMap.TerrainData[targetY, targetX].Thing.Description}.");
                    currentMap.TerrainData[targetY, targetX].Thing.Interact();
                    Console.ReadKey(true);
                    DrawEverything();

                }
                else if(currentMap.TerrainData[targetY, targetX].Resident != null)
                {
                    if (currentMap.TerrainData[targetY, targetX].Resident.GetType() == typeof(NPC)) {
                        Console.WriteLine($"This is a human named {currentMap.TerrainData[targetY, targetX].Resident.Name}.");
                    }
                    else //if (currentMap.TerrainData[targetY, targetX].Resident.GetType() == typeof(Enemy))
                    {
                        CombatManager combatManager = new CombatManager(p1, currentMap.TerrainData[targetY, targetX].Resident as Enemy);
                        
                    }
                }
                else
                {
                    
                    Console.WriteLine($"You cannot move {failure}. There is a {currentMap.TerrainData[targetY, targetX].Terrain} here.");
                }
				
			}
		}
        private static void CharacterAI()
        {
            
            if(currentMap.AICharacters.Count > 0)
            {
                foreach(Character c in currentMap.AICharacters)
                {
                    c.Wander();
                }
            }
            
            /*
            foreach(Character c in currentMap.AICharacters)
            {
                if(c.Path.Count > 0)
                {
                    c.GoToTarget();
                }
            }
            */
        }
		private static void PerformActions(ConsoleKey input)
		{
            switch (input)
            {
                case ConsoleKey.I:
                    InventoryController.ShowInventory(PlayerInventory);
                    goodInput = true;
                    break;

            }
		}
        
	}
}
