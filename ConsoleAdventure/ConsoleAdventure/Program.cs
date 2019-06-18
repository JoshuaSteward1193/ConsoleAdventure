using System;
using System.Diagnostics;
using System.Text;

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
        public static Inventory PlayerArmory;
		public static bool goodInput;
		public static bool moveInput;

        //UI VARIABLES
        public static StringBuilder DialogBoxTopLine = new StringBuilder();
        public static StringBuilder DialogBoxBottomLine = new StringBuilder();
        public static int DialogTopPos;
        public static int DialogBotPos;
        public static int DialogBoxCurrLine;
        public static int DialogBoxIndex;

        //USER PREFERENCES
        public static bool PrintColor = true;
		public static bool DebugGame = true;
        public static int ConsoleWidth = 91;
        public static int ConsoleHeight = 33;
		public static int YBuffer = 9; //Should be odd number
		public static int XBuffer = 21; //Should be odd number
        public static string SideBuffer = "                       ";


        static void Main(string[] args)
		{
			//INITIAL LOAD
			GameData.DataBuild();

            //UI LOAD
            DialogBoxTopLine.Append(SpecialChars.DoubleLineLeftTop);
            for (int i = 0; i < 79; i++) DialogBoxTopLine.Append(SpecialChars.DoubleLineAcross);
            DialogBoxTopLine.Append(SpecialChars.DoubleLineRightTop);
            DialogBoxBottomLine.Append(SpecialChars.DoubleLineLeftBottom);
            for (int i = 0; i < 79; i++) DialogBoxBottomLine.Append(SpecialChars.DoubleLineAcross);
            DialogBoxBottomLine.Append(SpecialChars.DoubleLineRightBottom);

            DialogBoxCurrLine = 0;
            DialogBoxIndex = 0;

            currentMap = GameData.AllMaps[2];
			p1 = new Player("Nemo", 'P', ConsoleColor.Cyan, 10);
            p1.SetHealthTo(6);
            PlayerInventory = new Inventory();
            PlayerArmory = new Inventory();
            p1.EquipStartingWeapon(new Weapon("Fists", "Just your bare hands", 1, "Fist"));
            //Stopwatch for debug purposes
            sw = new Stopwatch();

			currentMap.TerrainData[currentMap.SpawnPoints[0].YVal, currentMap.SpawnPoints[0].XVal].SpawnCharacter(p1);
			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
            Console.WindowWidth = ConsoleWidth;
            Console.WindowHeight = ConsoleHeight;
            Console.SetBufferSize(ConsoleWidth, ConsoleHeight);
            ConsoleLockdown.Lockdown();

            //ASTAR PATHFINDING TEST - BROKEN
            //currentMap.AICharacters[0].Path = Pathfinding.AStar(currentMap.TerrainData[currentMap.AICharacters[0].Position.YVal,currentMap.AICharacters[0].Position.XVal], currentMap.TerrainData[10, 10], currentMap);


            //INTRO
            PrintCenterLine("Welcome to the game.");
            PrintCenterLine("For best results, please right-click on the title bar and set your font to Courier New.");
            PrintCenterLine("Thank you for playing my game.");
            PrintCenterLine("Press any key to begin");
			Console.ReadKey();
            Console.Clear();

            //PLAYER SET UP
            PrintCenterLine("Firstly, what name would you like your character to have?");
            Console.CursorVisible = true;
            p1.Name = Console.ReadLine();
            Console.CursorVisible = false;
            Console.Clear();
            PrintCenterLine($"You shall henceforth and forever be known as {p1.Name}");
            PrintCenterLine("Press the enter key to start the game.");
            Console.ReadLine();
            
            //STORY BEGIN
            ScreenTransition.Transition();
            PrintCenterLine("You aren't entirely sure what has happened, but you find yourself in a ruined prison.");
            PrintCenterLine("Apparently, some goblins ambushed you while you were sleeping and imprisioned you.");            
            PrintCenterLine("You probably didn't plan on your adventure starting like this, but here you are.");
            PrintCenterLine("This is actually quite fortuitous, because these goblins are mere pests.");
            PrintCenterLine("They shall serve as a whetstone upon which to sharpen your skills. A tutorial, as it were.");
            Console.ReadLine();
            Console.Clear();
            DrawEverything();

			//TURN CYCLE
			while (p1.Health > 0)
			{
                PlayerInput();                      

                CharacterAI();

                DrawEverything();
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
            PrintDialogBox(7, 23);
            DialogBoxCurrLine = 24;
            DialogBoxIndex = 7;
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }
		private static void PrintMap(bool color)
		{
            PrintMaps.Print(currentMap.TerrainData, YBuffer, XBuffer, p1.Position, color);            
		}
        public static void PrintCenterText(string line)
        {
            int length = line.Length;
            if (length < ConsoleWidth)
            {
                Console.SetCursorPosition((ConsoleWidth - length) / 2, Console.CursorTop);                
            }
            Console.Write(line);
        }
		public static void PrintCenterLine(string line)
        {
            int length = line.Length;
            if (length < ConsoleWidth)
            {
                Console.SetCursorPosition((ConsoleWidth - length) / 2, Console.CursorTop);               
            }
            Console.Write(line + '\n');
            //TODO: Needs an else and accompanying logic
        }
        public static void PrintDialogBox(int height, int pos)
        {
            int position = pos;
            DialogTopPos = pos;
            Console.SetCursorPosition(5, position);
            Console.Write(DialogBoxTopLine);
            position++;
            for(int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(5, position);
                Console.Write(SpecialChars.DoubleLineUpDown);
                Console.SetCursorPosition(85, position);
                Console.Write(SpecialChars.DoubleLineUpDown);
                position++;
            }

            Console.SetCursorPosition(5, position);
            Console.Write(DialogBoxBottomLine);
            DialogBotPos = position;
        }

        public static void WriteToDialogBox(string message)
        {
            if (DialogBoxIndex != 7) DialogBoxCurrLine++;
            string[] words = message.Split(' ');
            //int index = 7;
            //int currline = DialogBoxCurrLine;
            Console.SetCursorPosition(7, DialogBoxCurrLine);
            for(int i = 0; i < words.Length; i++)
            {
                if (words[i].Length + DialogBoxIndex > 83)
                {
                    DialogBoxCurrLine++;
                    Console.SetCursorPosition(7, DialogBoxCurrLine);
                    DialogBoxIndex = 7;
                }
                Console.Write(words[i]);
                Console.Write(' ');
                DialogBoxIndex += words[i].Length + 1;
            }
            
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
            //Console.Write(SideBuffer);
            PrintCenterLine($"Player: {p1.Name} | Health: {p1.Health}/{p1.MaxHealth} | Location: {currentMap.TerrainData[p1.Position.YVal, p1.Position.XVal].Terrain}");
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
                    WriteToDialogBox($"This is a {currentMap.TerrainData[targetY, targetX].Thing.Name}. " +
                        $"{currentMap.TerrainData[targetY, targetX].Thing.Description} ");
                    currentMap.TerrainData[targetY, targetX].Thing.Interact();
                    Console.ReadKey(true);
                    DrawEverything();

                }
                else if(currentMap.TerrainData[targetY, targetX].Resident != null)
                {
                    if (currentMap.TerrainData[targetY, targetX].Resident.GetType() == typeof(NPC)) {
                        WriteToDialogBox($"This is a human named {currentMap.TerrainData[targetY, targetX].Resident.Name}.");
                    }
                    else //if (currentMap.TerrainData[targetY, targetX].Resident.GetType() == typeof(Enemy))
                    {
                        CombatManager combatManager = new CombatManager(p1, currentMap.TerrainData[targetY, targetX].Resident as Enemy);
                        DrawEverything();
                    }
                }
                else
                {

                    WriteToDialogBox($"You cannot move {failure}. There is a {currentMap.TerrainData[targetY, targetX].Terrain} here.");
                }
				
			}
		}
        private static void CharacterAI()
        {
            
            if(currentMap.AICharacters.Count > 0)
            {
                foreach(Character c in currentMap.AICharacters)
                {
                    if (c.Roams)
                    {
                        if (c.Health > 0)
                        {
                            c.Wander();
                        }
                    }            
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
                case ConsoleKey.A:
                    InventoryController.ShowInventory(PlayerArmory);
                    goodInput = true;
                    break;


            }
            
		}
        
	}
}
