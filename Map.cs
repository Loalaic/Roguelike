using System;

namespace Roguelike
{
    class Map
    {
        private int mapX;
        private int mapY;

        private int upstair;
        private int downstair;

        char[,] map;
        ConsoleColor[,] colormap;

        //Constructor
        public Map(int x, int y)
        {
            mapX = x;
            mapY = y;

            map = new char[x, y];
            colormap = new ConsoleColor[x,y];

            for (int i = 0; i < x; i ++)
            {
                for (int j = 0; j < y; j++)
                {
                    map[i, j] = ' ';
                    colormap[i, j] = ConsoleColor.White;
                }
            }
        }

        //Render tile by tile
        public void Draw()
        {
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.ForegroundColor = colormap[i, j];
                    Console.Write(map[i,j]);
                }
            }
        }
        //Render a single tile specified by coordinate
        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = colormap[x, y];
            Console.Write(map[x, y]);
        }

        public void Draw(ConsoleColor color)
        {
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.ForegroundColor = color;
                    Console.Write(map[i, j]);
                }
            }
        }

        public void Draw(int x, int y, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(map[x, y]);
        }

        //Setter for individual tiles specified by coordinate
        public void SetChar(int x, int y, char c)
        {
            map[x, y] = c;
        }
        //Getter for individual tiles specified by coordinate
        public char GetChar(int x, int y)
        {
            return map[x, y];
        }
        //Create a single room on the map using two-point rectangle
        public void CreateRoom(int x, int y, int a, int b)
        {
            for (int i = x+1; i <= a-1; i++)
            {
                for (int j = y+1; j <= b-1; j++)
                {
                    map[i, j] = '.';
                }
            }

            map[x, b] = '└';
            map[a, b] = '┘';
            map[x, y] = '┌';
            map[a, y] = '┐';

            for (int i = x+1; i <= a-1; i++)
            {
                map[i, y] = '─';
                map[i, b] = '─';
            }

            for (int i = y+1; i <= b-1; i++)
            {
                map[x, i] = '│';
                map[a, i] = '│';
            }
        }
        //Auto create path between two coordinates if there is no interference
        public void CreatePath(int x1, int y1, int x2, int y2)
        {
            bool trigger = true;

            int tempx = x1;
            int tempy = y1;

            if (map[tempx, tempy].Equals('└') | map[tempx, tempy].Equals('┘') | map[tempx, tempy].Equals('┌') | map[tempx, tempy].Equals('┐') | map[tempx, tempy].Equals('│') | map[tempx, tempy].Equals('─') | map[tempx, tempy].Equals('.'))
            {
                trigger = false;
            }

            while (!(tempx == x2 && tempy == y2))
            {
                if (x2 > tempx)
                {
                    tempx++;
                    if (map[tempx, tempy].Equals('└') | map[tempx, tempy].Equals('┘') | map[tempx, tempy].Equals('┌') | map[tempx, tempy].Equals('┐') | map[tempx, tempy].Equals('│') | map[tempx, tempy].Equals('─') | map[tempx, tempy].Equals('.'))
                    {
                        trigger = false;
                        break;
                    }

                }
                if (y2 > tempy)
                {
                    tempy++;
                    if (map[tempx, tempy].Equals('└') | map[tempx, tempy].Equals('┘') | map[tempx, tempy].Equals('┌') | map[tempx, tempy].Equals('┐') | map[tempx, tempy].Equals('│') | map[tempx, tempy].Equals('─') | map[tempx, tempy].Equals('.'))
                    {
                        trigger = false;
                        break;
                    }
                }
                if (x2 < tempx)
                {
                    tempx--;
                    if (map[tempx, tempy].Equals('└') | map[tempx, tempy].Equals('┘') | map[tempx, tempy].Equals('┌') | map[tempx, tempy].Equals('┐') | map[tempx, tempy].Equals('│') | map[tempx, tempy].Equals('─') | map[tempx, tempy].Equals('.'))
                    {
                        trigger = false;
                        break;
                    }
                }
                if (y2 < tempy)
                {
                    tempy--;
                    if(map[tempx, tempy].Equals('└') | map[tempx, tempy].Equals('┘') | map[tempx, tempy].Equals('┌') | map[tempx, tempy].Equals('┐') | map[tempx, tempy].Equals('│') | map[tempx, tempy].Equals('─') | map[tempx, tempy].Equals('.'))
                    {
                        trigger = false;
                        break;
                    }
                }
                
            }

            if (trigger)
            {
                this.SetChar(x1, y1, '#');
            }

            while (trigger)
            {
                if (x2 > x1)
                {
                    x1++;
                    this.SetChar(x1, y1, '#');

                }
                if (y2 > y1)
                {
                    y1++;
                    this.SetChar(x1, y1, '#');
                }
                if (x2 < x1)
                {
                    x1--;
                    this.SetChar(x1, y1, '#');
                }
                if (y2 < y1)
                {
                    y1--;
                    this.SetChar(x1, y1, '#');
                }
                if (x2 == x1 && y2 == y1)
                {
                    trigger = false;
                }
            }
        }
        //Populate the map with numbers of rooms specified (# of rooms = argument rooms + 1)
        public void GenerateDungeonRooms (int rooms)
        {
            Random random = new Random();
            int c = 0;
            while (c < rooms + 1)
            {
                for (int i = 1; i <= rooms + 1; i++)
                {
                    int y = random.Next(mapY) +1;
                    int x = random.Next(mapX) +1;

                    if (mapY - y <= 10 || mapX - x <= 10)
                    {
                        i--;
                    }
                    else
                    {
                        int y2 = y + 5 + random.Next(5);
                        int x2 = x + 5 + random.Next(5);

                        bool checker = true;

                        for (int j = x; j <= x2; j++)
                        {
                            for (int k = y; k <= y2; k++)
                            {
                                if (!this.GetChar(j, k).Equals(' '))
                                {
                                    checker = false;
                                }
                            }
                        }

                        for (int j = 0; j < mapY; j++)
                        {
                            if (map[x, j].Equals('┘') | map[x, j].Equals('└') | map[x, j].Equals('─'))
                            {
                                checker = false;
                            }

                            if (map[x - 1, j].Equals('┘') | map[x - 1, j].Equals('└') | map[x -1, j].Equals('─'))
                            {
                                checker = false;
                            }

                            if (map[x + 1, j].Equals('┘') | map[x + 1, j].Equals('└') | map[x + 1, j].Equals('─'))
                            {
                                checker = false;
                            }
                        }

                        if (checker)
                        {
                            this.CreateRoom(x, y, x2, y2);
                            if (c == 0)
                            {
                                int uwu = random.Next(x2-x-1)+x+1;
                                int owo = random.Next(y2 - y-1) + y+1;
                                map[uwu, owo] = '<';
                                upstair = (uwu * 1000) + owo;
                            } else if (c == 1)
                            {
                                int uwu = random.Next(x2 - x - 1) + x + 1;
                                int owo = random.Next(y2 - y - 1) + y + 1;
                                map[uwu, owo] = '>';
                                downstair = (uwu * 1000) + owo;
                            }
                            c++;
                            if (c >= rooms + 1)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            
        }
        //Automatically create doors on the rooms
        public void GenerateDungeonDoors()
        {
            Random random = new Random();
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    if (map[i,j] == '┐')
                    {
                        int tempCount = 0;
                        int temp = j + 1;
                        while (map[i, temp] == '│')
                        {
                            temp++;
                            tempCount++;
                        }

                        int rando = j + random.Next(tempCount) + 1;

                        map[i, rando] = '░';
                    } else if (map[i, j] == '┌')
                    {
                        int tempCount = 0;
                        int temp = j + 1;
                        while (map[i, temp] == '│')
                        {
                            temp++;
                            tempCount++;
                        }

                        int rando = j + random.Next(tempCount) + 1;

                        map[i, rando] = '░';
                    }
                }
            }
        }
        //Automatically create all paths available between all rooms
        public void GenerateDungeonPaths()
        {
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    if (this.GetChar(i,j) == '░')
                    {
                        for (int a = i+1; a < mapX; a++)
                        {
                            for (int b = 0; b < mapY; b++)
                            {
                                if (this.GetChar(a,b) == '░')
                                {
                                    this.CreatePath(i+1, j, a-1, b);
                                }
                            }
                        }
                    }
                }
            }
        }
        //Assign value to the colormap array tile by tile
        public void Colorize()
        {
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    if (map[i, j].Equals('#'))
                    {
                        colormap[i, j] = ConsoleColor.Gray;
                    } else if (map[i, j].Equals('└') | map[i, j].Equals('┘') | map[i, j].Equals('┌') | map[i, j].Equals('┐') | map[i, j].Equals('│') | map[i, j].Equals('─'))
                    {
                        colormap[i, j] = ConsoleColor.DarkYellow;
                    } else if (map[i, j].Equals('░'))
                    {
                        colormap[i, j] = ConsoleColor.Yellow;
                    } else if (map[i, j].Equals('<') | map[i, j].Equals('>'))
                    {
                        colormap[i, j] = ConsoleColor.Green;
                    }
                }
            }
        }
        //Assign value to one tile of colormap automatically
        public void Colorize(int x, int y)
        {
                    if (map[x, y].Equals('#'))
                    {
                        colormap[x, y] = ConsoleColor.Gray;
                    }
                    else if (map[x, y].Equals('└') | map[x, y].Equals('┘') | map[x, y].Equals('┌') | map[x, y].Equals('┐') | map[x, y].Equals('│') | map[x, y].Equals('─'))
                    {
                        colormap[x, y] = ConsoleColor.DarkYellow;
                    }
                    else if (map[x, y].Equals('░'))
                    {
                        colormap[x, y] = ConsoleColor.Yellow;
                    }
                    else if (map[x, y].Equals('<') | map[x, y].Equals('>'))
                    {
                        colormap[x, y] = ConsoleColor.Green;
                    }

        }
        //Assign value to one tile by hand
        public void Colorize(int x, int y, ConsoleColor color)
        {
            colormap[x, y] = color;
        }

        public int GetUpStair()
        {
            return upstair;
        }

        public int GetDownStair()
        {
            return downstair;
        }

        public void RemoveExcessiveDoors()
        {
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    if (this.GetChar(i, j) == '░')
                    {
                        if (!(this.GetChar(i-1, j) == '#' | this.GetChar(i + 1, j) == '#'))
                        {
                            this.SetChar(i, j, '│');
                        }
                    }
                }
            }
        }

        public bool IsPassable(int coX, int coY)
        {
            bool condition = true;

            if (!(this.GetChar(coX, coY).Equals('.') | this.GetChar(coX, coY).Equals('#') | this.GetChar(coX, coY).Equals('░') | this.GetChar(coX, coY).Equals('<') | this.GetChar(coX, coY).Equals('>')))
                condition = false;

            return condition;
        }
    }
}
