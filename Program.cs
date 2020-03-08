using System;
using System.Runtime;

namespace Roguelike

{
    class Program
    {
        static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.Write(message);
        }

        static void ClearLog()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        static void DrawPlayer(int coX, int coY)
        {
            Console.SetCursorPosition(coX, coY);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("@");
        }

        static void Main(string[] args)
        {

            

            ConsoleKeyInfo keyInfo;
            int redo = 0;
            int turn = 0;
            

            Console.CursorVisible = false;

            Levels levels = new Levels(100);
            int dLevel = levels.getDLevel();

            levels.InitLevels();

            int coY = levels.getCurrentMap().GetUpStair()%1000;
            int coX = (levels.getCurrentMap().GetUpStair()-coY)/1000;

            
            levels.getCurrentVisibleMap().CalculateMap(coX, coY, levels.getCurrentMap());
            levels.getCurrentVisibleMap().Draw(coX, coY, levels.getCurrentMap());

            DrawPlayer(coX, coY);

            while (redo == 0)
            {
                

                Console.SetCursorPosition(0, 29);
                Console.Write("Turn: " + turn.ToString());

                Console.SetCursorPosition(15, 29);
                Console.Write("Dungeon Level: " + (dLevel+1).ToString());

                keyInfo = Console.ReadKey(true);

                ClearLog();

                int moveDirection = 5;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.NumPad4:
                        levels.getCurrentMap().Draw(coX, coY, ConsoleColor.DarkGray);
                        Move(4);
                        moveDirection = 4;
                        break;
                    case ConsoleKey.NumPad6:
                        levels.getCurrentMap().Draw(coX, coY, ConsoleColor.DarkGray);
                        Move(6);
                        moveDirection = 6;
                        break;
                    case ConsoleKey.NumPad8:
                        levels.getCurrentMap().Draw(coX, coY, ConsoleColor.DarkGray);
                        Move(8);
                        moveDirection = 8;
                        break;
                    case ConsoleKey.NumPad2:
                        levels.getCurrentMap().Draw(coX, coY, ConsoleColor.DarkGray);
                        Move(2);
                        moveDirection = 2;
                        break;
                    case ConsoleKey.NumPad1:
                        levels.getCurrentMap().Draw(coX, coY, ConsoleColor.DarkGray);
                        Move(1);
                        moveDirection = 1;
                        break;
                    case ConsoleKey.NumPad3:
                        levels.getCurrentMap().Draw(coX, coY, ConsoleColor.DarkGray);
                        Move(3);
                        moveDirection = 3;
                        break;
                    case ConsoleKey.NumPad7:
                        levels.getCurrentMap().Draw(coX, coY, ConsoleColor.DarkGray);
                        Move(7);
                        moveDirection = 7;
                        break;
                    case ConsoleKey.NumPad9:
                        levels.getCurrentMap().Draw(coX, coY, ConsoleColor.DarkGray);
                        Move(9);
                        moveDirection = 9;
                        break;
                    case ConsoleKey.Enter:
                        Interact(coX, coY, levels.getCurrentMap());
                        break;
                    case ConsoleKey.C:
                        Console.SetCursorPosition(0, 0);
                        string cheat = Console.ReadLine();
                        Cheat(cheat);
                        break;
                }

                levels.getCurrentVisibleMap().CalculateMap(coX, coY, levels.getCurrentMap());
                levels.getCurrentVisibleMap().DrawGreyOut(coX, coY, moveDirection, levels.getCurrentMap());
                levels.getCurrentVisibleMap().Draw(coX, coY, levels.getCurrentMap());
                

                DrawPlayer(coX, coY);

                turn++;
            }

            Console.ReadLine();



            void Move(int direction)
            {
                switch (direction)
                {
                    case 1:
                        coX--;
                        coY++;
                        if (!levels.getCurrentMap().IsPassable(coX,coY))
                        {
                            Log("You bumped into a wall!");
                            coX++;
                            coY--;
                            break;
                        }
                        break;
                    case 2:
                        coY++;
                        if (!levels.getCurrentMap().IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coY--;
                            break;
                        }
                        break;
                    case 3:
                        coX++;
                        coY++;
                        if (!levels.getCurrentMap().IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX--;
                            coY--;
                            break;
                        }
                        break;
                    case 4:
                        coX--;
                        if (!levels.getCurrentMap().IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX++;
                            break;
                        }
                        break;
                    case 6:
                        coX++;
                        if (!levels.getCurrentMap().IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX--;
                            break;
                        }
                        break;
                    case 7:
                        coX--;
                        coY--;
                        if (!levels.getCurrentMap().IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX++;
                            coY++;
                            break;
                        }
                        break;
                    case 8:
                        coY--;
                        if (!levels.getCurrentMap().IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coY++;
                            break;
                        }
                        break;
                    case 9:
                        coX++;
                        coY--;
                        if (!levels.getCurrentMap().IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX--;
                            coY++;
                            break;
                        }
                        break;
                }

                
            }

            void Interact(int x, int y, Map map)
            {
                char target = map.GetChar(x, y);
                switch (target)
                {
                    case '<':
                        levels.goUpStairs();
                        coY = levels.getCurrentMap().GetDownStair() % 1000;
                        coX = (levels.getCurrentMap().GetDownStair() - coY) / 1000;
                        Console.Clear();
                        dLevel = levels.getDLevel();
                        levels.getCurrentMap().Draw(levels.getCurrentVisibleMap());
                        break;
                    case '>':
                        levels.goDownStairs();
                        coY = levels.getCurrentMap().GetUpStair() % 1000;
                        coX = (levels.getCurrentMap().GetUpStair() - coY) / 1000;
                        Console.Clear();
                        dLevel = levels.getDLevel();
                        levels.getCurrentMap().Draw(levels.getCurrentVisibleMap());
                        break;
                }
            }

            void Cheat(string code)
            {
                switch (code)
                {
                    case "Teleport":
                        Log("Where do you want to teleport to? Type coordinates x,y: ");
                        string coorodinate = Console.ReadLine();
                        int x = Convert.ToInt16(coorodinate.Split(',')[0]);
                        int y = Convert.ToInt16(coorodinate.Split(',')[1]);
                        Teleport(x,y);
                        break;
                    case "See Map":
                        levels.getCurrentMap().Draw();
                        break;
                    case "Teleport Levels":
                        Log("Which level do you want to teleport to? Type dLevel: ");
                        string input = Console.ReadLine();
                        int i = Convert.ToInt16(input);
                        levels.setDLevel(i);
                        coY = levels.getCurrentMap().GetUpStair() % 1000;
                        coX = (levels.getCurrentMap().GetUpStair() - coY) / 1000;
                        Console.Clear();
                        dLevel = levels.getDLevel();
                        levels.getCurrentMap().Draw(levels.getCurrentVisibleMap());
                        break;

                }
            }

            void Teleport(int xcor, int ycor)
            {
                levels.getCurrentMap().Draw(coX, coY);
                coX = xcor;
                coY = ycor;
            }
        }
    }
}
