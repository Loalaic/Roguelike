using System;

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

            Map map = new Map(120, 29);
            map.GenerateDungeonRooms(7);
            map.GenerateDungeonDoors();
            map.GenerateDungeonPaths();
            map.RemoveExcessiveDoors();
            map.Colorize();

            int coY = map.GetUpStair()%1000;
            int coX = (map.GetUpStair()-coY)/1000;

            VisibleMap visibleMap = new VisibleMap(120, 29);
            visibleMap.CalculateMap(coX, coY, map);
            visibleMap.Draw(coX, coY, map);

            DrawPlayer(coX, coY);

            while (redo == 0)
            {
                

                Console.SetCursorPosition(0, 29);
                Console.Write("Turn: " + turn.ToString());

                keyInfo = Console.ReadKey(true);

                ClearLog();

                int moveDirection = 5;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.NumPad4:
                        map.Draw(coX, coY);
                        Move(4);
                        moveDirection = 4;
                        break;
                    case ConsoleKey.NumPad6:
                        map.Draw(coX, coY);
                        Move(6);
                        moveDirection = 6;
                        break;
                    case ConsoleKey.NumPad8:
                        map.Draw(coX, coY);
                        Move(8);
                        moveDirection = 8;
                        break;
                    case ConsoleKey.NumPad2:
                        map.Draw(coX, coY);
                        Move(2);
                        moveDirection = 2;
                        break;
                    case ConsoleKey.NumPad1:
                        map.Draw(coX, coY);
                        Move(1);
                        moveDirection = 1;
                        break;
                    case ConsoleKey.NumPad3:
                        map.Draw(coX, coY);
                        Move(3);
                        moveDirection = 3;
                        break;
                    case ConsoleKey.NumPad7:
                        map.Draw(coX, coY);
                        Move(7);
                        moveDirection = 7;
                        break;
                    case ConsoleKey.NumPad9:
                        map.Draw(coX, coY);
                        Move(9);
                        moveDirection = 9;
                        break;
                    case ConsoleKey.M:
                        map.Draw();
                        break;
                }

                visibleMap.CalculateMap(coX, coY, map);
                visibleMap.DrawGreyOut(coX, coY, moveDirection, map);
                visibleMap.Draw(coX, coY, map);
                

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
                        if (!map.IsPassable(coX,coY))
                        {
                            Log("You bumped into a wall!");
                            coX++;
                            coY--;
                            break;
                        }
                        break;
                    case 2:
                        coY++;
                        if (!map.IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coY--;
                            break;
                        }
                        break;
                    case 3:
                        coX++;
                        coY++;
                        if (!map.IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX--;
                            coY--;
                            break;
                        }
                        break;
                    case 4:
                        coX--;
                        if (!map.IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX++;
                            break;
                        }
                        break;
                    case 6:
                        coX++;
                        if (!map.IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX--;
                            break;
                        }
                        break;
                    case 7:
                        coX--;
                        coY--;
                        if (!map.IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX++;
                            coY++;
                            break;
                        }
                        break;
                    case 8:
                        coY--;
                        if (!map.IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coY++;
                            break;
                        }
                        break;
                    case 9:
                        coX++;
                        coY--;
                        if (!map.IsPassable(coX, coY))
                        {
                            Log("You bumped into a wall!");
                            coX--;
                            coY++;
                            break;
                        }
                        break;
                }

                
            }
        }
    }
}
