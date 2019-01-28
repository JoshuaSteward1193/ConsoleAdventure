using System;

namespace ConsoleAdventure
{
    class CombatManager
    {
        Player player;
        Enemy enemy;

        string choice1;
        string choice2;
        string choice3;
        int selection;
        bool playerTurn;

        Battlefield bField;        
        public CombatManager(Player p, Enemy e)
        {
            player = p;
            enemy = e;        

            //Set initial variables
            choice1 = $"{player.AssignedMoves[0].Name}";
            choice2 = $"{player.AssignedMoves[1].Name}";
            choice3 = $"{player.AssignedMoves[2].Name}";
            selection = 0;
            playerTurn = true;

            SetBattlefield(Program.currentMap.TerrainData[e.Position.YVal, e.Position.XVal].Terrain);
            CombatMessage();
            Console.ReadKey(true);
            Console.Clear();

            //Set the size of the side buffer
            Program.SideBuffer = "";
            int bufferLength = (Console.WindowWidth - 13) / 2;
            for(int i = 0; i < bufferLength; i++)
            {
                Program.SideBuffer += " ";
            }

            //Play screen transition
            ScreenTransition.Transition();
            Combat();
        } 

        private void Combat()
        {
            Console.Clear();
            //bField.TerrainData[bField.SpawnPoints[0].YVal, bField.SpawnPoints[0].XVal].SpawnCharacter(player);
            //bField.TerrainData[bField.SpawnPoints[1].YVal, bField.SpawnPoints[1].XVal].SpawnCharacter(enemy);
           
            //Main Print Loop
            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                
                PrintMaps.Print(bField.TerrainData, 4, 7, bField.SpawnPoints[0], true);
                PrintHeader();

                ConsoleKey key;

                //PLAYER TURN
                if (playerTurn)
                {
                    PrintPlayerOptions();
                    key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (selection < 3 && selection > 0) selection--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (selection < 2 && selection > -1) selection++;
                            break;
                        case ConsoleKey.Enter:
                            switch (selection)
                            {
                                case 0:
                                    PrintPlayerTurn(player.AssignedMoves[0]);
                                    break;
                                case 1:
                                    PrintPlayerTurn(player.AssignedMoves[1]);
                                    break;
                                case 2:
                                    PrintPlayerTurn(player.AssignedMoves[2]);
                                    break;
                            }
                            playerTurn = false;
                            break;
                        default:
                            break;
                    }
                }
                //ENEMY TURN
                else
                {
                    PrintEnemyOptions();
                    key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            switch (enemy.CombatChoice())
                            {
                                case 1:
                                    PrintEnemyTurn(enemy.StandardMove1);
                                    break;
                                case 2:
                                    PrintEnemyTurn(enemy.StandardMove2);
                                    break;
                                case 3:
                                    PrintEnemyTurn(enemy.PowerMove1);
                                    break;
                                case 4:
                                    PrintEnemyTurn(enemy.SupportMove1);
                                    break;
                                default:
                                    Console.WriteLine("Error with enemy AI");
                                    break;
                            }
                            playerTurn = true;
                            break;
                        default:
                            break;
                    }
                }                
            }
            Console.SetCursorPosition(Program.SideBuffer.Length, 19);
            if (player.Health > 0)
            {
                Program.PrintCenterLine("You won!");
                enemy.Die();
                Console.ReadKey(true);               
            }
            else
            {
                Program.PrintCenterLine("You lost!");
                Console.ReadKey(true);
            }
            Console.Clear();
        }

        private void CombatMessage()
        {
            Program.PrintCenterLine($"Starting combat between the player, {player.Name}, and {enemy.Name} the {enemy.Type}");
        }
        private void PrintHeader()
        {
            int playerNameStartPos = Program.SideBuffer.Length - player.Name.Length - 5;
            int enemyNameStartPos = Program.SideBuffer.Length + bField.TerrainData.GetLength(1) + 5;
            string playerHealth = $"{player.Health} / {player.MaxHealth}";
            string enemyHealth = $"{enemy.Health} / {enemy.MaxHealth}";
            Console.SetCursorPosition(playerNameStartPos, 7);            
            Console.Write(player.Name);
            Console.SetCursorPosition(playerNameStartPos - (playerHealth.Length - player.Name.Length), 8);
            Console.Write(playerHealth);
            Console.SetCursorPosition(enemyNameStartPos, 7);
            Console.Write(enemy.Name);
            Console.SetCursorPosition(enemyNameStartPos, 8);
            Console.Write(enemyHealth);
            Console.SetCursorPosition(5, 10);
            for (int i = 0; i < Console.WindowWidth - 10; i++)
            {
                Console.Write("=");
            }
        }
        private void PrintPlayerOptions()
        {           
            Console.SetCursorPosition(Program.SideBuffer.Length, 11);
            Console.ForegroundColor = ConsoleColor.Green;
            Program.PrintCenterLine("It is your turn.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(Program.SideBuffer.Length, 13);
            if (selection == 0)
            {
                Console.Write($">  {choice1}");
            }
            else Console.Write($"   {choice1}");
            Console.SetCursorPosition(Program.SideBuffer.Length, 14);
            if (selection == 1)
            {
                Console.Write($">  {choice2}");
            }
            else Console.Write($"   {choice2}");
            Console.SetCursorPosition(Program.SideBuffer.Length, 15);
            if (selection == 2)
            {
                Console.Write($">  {choice3}");
            }
            else Console.Write($"   {choice3}");
        }
        private void PrintEnemyOptions()
        {
            Console.SetCursorPosition(Program.SideBuffer.Length, 11);
            Console.ForegroundColor = ConsoleColor.Red;
            Program.PrintCenterLine($"It is {enemy.Name}'s turn.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(Program.SideBuffer.Length, 13);
            Console.Write(">  Defend!");
        }
        private void SetBattlefield(string type)
        {
            switch (type)
            {
                case "Stone Structure":
                    bField = GameData.AllBFields[0];
                    break;
                default:
                    bField = GameData.AllBFields[0];
                    break;
            }
        }
        private int CalcDamage(CombatMove move, int aStrength, int dVigor)
        {
            double rawDamage = aStrength * move.DamageMod / (dVigor / 2.0);
            if (rawDamage < 0) rawDamage = 1;
            int damage = Convert.ToInt32(rawDamage);
            return damage;
        }
        private void PrintEnemyTurn(CombatMove move)
        {
            Console.SetCursorPosition(Program.SideBuffer.Length / 2, 17);
            Program.PrintCenterLine($"{enemy.Name} {move.EActionText} you.");
            Console.SetCursorPosition(Program.SideBuffer.Length / 2, 18);
            if(Program.rand.NextDouble() <= move.AccuracyMod)
            {
                int damage = CalcDamage(move, enemy.Strength, player.Vigor);
                Program.PrintCenterLine($"You take {damage} points of damage.");
                player.Health -= damage;
            }
            else
            {
                Program.PrintCenterLine($"{enemy.Name}'s attack misses you!");
            }
            Console.ReadKey(true);
        }
        private void PrintPlayerTurn(CombatMove move)
        {
            Console.SetCursorPosition(Program.SideBuffer.Length / 2, 17);
            Program.PrintCenterLine($"You {move.PActionText} {enemy.Name}.");
            Console.SetCursorPosition(Program.SideBuffer.Length / 2, 18);
            if (Program.rand.NextDouble() <= move.AccuracyMod)
            {
                int damage = CalcDamage(move, player.Strength, enemy.Vigor);
                Program.PrintCenterLine($"{enemy.Name} takes {damage} points of damage.");
                enemy.Health -= damage;
            }
            else
            {
                Program.PrintCenterLine($"Your attack misses {enemy.Name}!");
            }
            Console.ReadKey(true);
        }
        
    }
}
