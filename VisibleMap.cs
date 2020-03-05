using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    class VisibleMap
    {
        private int mapX;
        private int mapY;

        private int lineOfSight = 7;

        bool[,] map;

        public VisibleMap (int x, int y)
        {
            mapX = x;
            mapY = y;

            map = new bool[x, y];

            for (int i = 0; i < x; i ++)
            {
                for (int j = 0; j < y; j++)
                {
                    map[i, j] = false;
                }
            }
        }

        public VisibleMap (int x, int y, int loS)
        {
            mapX = x;
            mapY = y;
            lineOfSight = loS;

            map = new bool[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    map[i, j] = false;
                }
            }
        }

        public void CastRay(int x1, int y1, int x2, int y2, Map map)
        {
            while (true)
            {
                if (x1 < x2 & y1 < y2)
                {
                    if (map.IsPassable(x1, y1)){
                        this.map[x1, y1] = true;
                        x1++;
                        y1++;
                    }
                    else
                    {
                        this.map[x1, y1] = true;
                        break;
                    }
                }
                if (x1 < x2 & y1 > y2)
                {
                    if (map.IsPassable(x1, y1))
                    {
                        this.map[x1, y1] = true;
                        x1++;
                        y1--;
                    }
                    else
                    {
                        this.map[x1, y1] = true;
                        break;
                    }
                }
                if (x1 > x2 & y1 < y2)
                {
                    if (map.IsPassable(x1, y1))
                    {
                        this.map[x1, y1] = true;
                        x1--;
                        y1++;
                    }
                    else
                    {
                        this.map[x1, y1] = true;
                        break;
                    }
                }
                if (x1 > x2 & y1 > y2)
                {
                    if (map.IsPassable(x1, y1))
                    {
                        this.map[x1, y1] = true;
                        x1--;
                        y1--;
                    }
                    else
                    {
                        this.map[x1, y1] = true;
                        break;
                    }
                }
                if (x1 > x2 & y1 == y2)
                {
                    if (map.IsPassable(x1, y1))
                    {
                        this.map[x1, y1] = true;
                        x1--;
                    }
                    else
                    {
                        this.map[x1, y1] = true;
                        break;
                    }
                }
                if (x1 < x2 & y1 == y2)
                {
                    if (map.IsPassable(x1, y1))
                    {
                        this.map[x1, y1] = true;
                        x1++;
                    }
                    else
                    {
                        this.map[x1, y1] = true;
                        break;
                    }
                }
                if (x1 == x2 & y1 < y2)
                {
                    if (map.IsPassable(x1, y1))
                    {
                        this.map[x1, y1] = true;
                        y1++;
                    }
                    else
                    {
                        this.map[x1, y1] = true;
                        break;
                    }
                }
                if (x1 == x2 & y1 > y2)
                {
                    if (map.IsPassable(x1, y1))
                    {
                        this.map[x1, y1] = true;
                        y1--;
                    }
                    else
                    {
                        this.map[x1, y1] = true;
                        break;
                    }
                }
                if (x1 == x2 & y1 == y2)
                {
                    break;
                }
            }
        }

        public void CalculateMap(int coX, int coY, Map map)
        {
            if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
            {
                for (int i = coX - lineOfSight; i <= coX + lineOfSight; i++)
                {
                    this.CastRay(coX, coY, i, (coY - lineOfSight), map);
                }
                for (int i = coX - lineOfSight; i <= coX + lineOfSight; i++)
                {
                    this.CastRay(coX, coY, i, (coY + lineOfSight), map);
                }
                for (int i = coY - lineOfSight; i <= coY + lineOfSight; i++)
                {
                    this.CastRay(coX, coY, (coX - lineOfSight), i, map);
                }
                for (int i = coY - lineOfSight; i <= coY + lineOfSight; i++)
                {
                    this.CastRay(coX, coY, (coX + lineOfSight), i, map);
                }
            }
            else
            {
                int imin = coX - lineOfSight;
                int jmin = coY - lineOfSight;
                if (coX - lineOfSight < 0)
                {
                    imin = 0;
                }
                if (coY - lineOfSight < 1)
                {
                    jmin = 1;
                }
                int imax = coX + lineOfSight;
                int jmax = coY + lineOfSight;
                if (coX + lineOfSight >= mapX)
                {
                    imax = mapX;
                }
                if (coY + lineOfSight >= mapY)
                {
                    jmax = mapY;
                }
                for (int i = imin; i <= imax; i++)
                {
                    this.CastRay(coX, coY, i, (coY - lineOfSight), map);
                }
                for (int i = imin; i <= imax; i++)
                {
                    this.CastRay(coX, coY, i, (coY + lineOfSight), map);
                }
                for (int i = jmin; i <= jmax; i++)
                {
                    this.CastRay(coX, coY, (coX - lineOfSight), i, map);
                }
                for (int i = jmin; i <= jmax; i++)
                {
                    this.CastRay(coX, coY, (coX + lineOfSight), i, map);
                }
            }
        }

        public bool GetMap(int x, int y)
        {
            return map[x,y];
        }

        public void Draw(int coX, int coY, Map map)
        {
            if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
            {
                for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                {
                    for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                    {
                        if (this.map[i,j])
                            map.Draw(i, j);
                    }
                }
            }
            else
            {
                int imin = coX - lineOfSight;
                int jmin = coY - lineOfSight;
                if (coX - lineOfSight < 0)
                {
                    imin = 0;
                }
                if (coY - lineOfSight < 1)
                {
                    jmin = 1;
                }
                int imax = coX + lineOfSight;
                int jmax = coY + lineOfSight;
                if (coX + lineOfSight >= mapX)
                {
                    imax = mapX;
                }
                if (coY + lineOfSight >= mapY)
                {
                    jmax = mapY;
                }
                for (int i = imin; i < imax; i++)
                {
                    for (int j = jmin; j < jmax; j++)
                    {
                        if (this.map[i, j])
                            map.Draw(i, j);
                    }
                }
            }
        }

        public void DrawGreyOut(int X, int Y, int direction, Map map)
        {
            int coX;
            int coY;
            switch (direction)
            {
                case 5:
                    break;

                case 1:
                    coX = X++;
                    coY = Y--;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY - lineOfSight])
                            {
                                map.Draw(i, coY - lineOfSight, ConsoleColor.DarkGray);
                                this.map[i, coY - lineOfSight] = false;
                            }   
                        }
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX + lineOfSight, j])
                            {
                                map.Draw(coX + lineOfSight, j, ConsoleColor.DarkGray);
                                this.map[coX + lineOfSight, j] = false;
                            }
                        }
                    }
                    else
                    {
                        int imin = coX - lineOfSight;
                        int jmin = coY - lineOfSight;
                        if (coX - lineOfSight < 0)
                        {
                            imin = 0;
                        }
                        if (coY - lineOfSight < 1)
                        {
                            jmin = 1;
                        }
                        int imax = coX + lineOfSight;
                        int jmax = coY + lineOfSight;
                        if (coX + lineOfSight >= mapX)
                        {
                            imax = mapX;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY;
                        }
                        for (int i = imin; i < imax; i++)
                        {
                            if (this.map[i, jmin])
                            {
                                map.Draw(i, jmin, ConsoleColor.DarkGray);
                                this.map[i, jmin] = false;
                            }  
                        }
                        for (int j = jmin; j < jmax; j++)
                        {
                            if (this.map[imax - 1, j])
                            {
                                map.Draw(imax - 1, j, ConsoleColor.DarkGray);
                                this.map[imax - 1, j] = false;
                            }

                        }
                    }
                    break;

                case 3:
                    coX = X--;
                    coY = Y--;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY - lineOfSight])
                            {
                                map.Draw(i, coY - lineOfSight, ConsoleColor.DarkGray);
                                this.map[i, coY - lineOfSight] = false;
                            }

                        }
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX - lineOfSight, j])
                            {
                                map.Draw(coX - lineOfSight, j, ConsoleColor.DarkGray);
                                this.map[coX - lineOfSight, j] = false;
                            }
                                
                        }
                    }
                    else
                    {
                        int imin = coX - lineOfSight;
                        int jmin = coY - lineOfSight;
                        if (coX - lineOfSight < 0)
                        {
                            imin = 0;
                        }
                        if (coY - lineOfSight < 1)
                        {
                            jmin = 1;
                        }
                        int imax = coX + lineOfSight;
                        int jmax = coY + lineOfSight;
                        if (coX + lineOfSight >= mapX)
                        {
                            imax = mapX;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY;
                        }
                        for (int i = imin; i < imax; i++)
                        {
                            if (this.map[i, jmin])
                            {
                                map.Draw(i, jmin, ConsoleColor.DarkGray);
                                this.map[i, jmin] = false;
                            }  
                        }
                        for (int j = jmin; j < jmax; j++)
                        {
                            if (this.map[imin, j])
                            {
                                map.Draw(imin, j, ConsoleColor.DarkGray);
                                this.map[imin, j] = false;
                            }
                        }
                    }
                    break;

                case 7:
                    coX = X++;
                    coY = Y++;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY + lineOfSight])
                            {
                                map.Draw(i, coY + lineOfSight, ConsoleColor.DarkGray);
                                this.map[i, coY + lineOfSight] = false;
                            }

                        }
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX + lineOfSight, j])
                            {
                                map.Draw(coX + lineOfSight, j, ConsoleColor.DarkGray);
                                this.map[coX + lineOfSight, j] = false;
                            }
                                
                        }
                    }
                    else
                    {
                        int imin = coX - lineOfSight;
                        int jmin = coY - lineOfSight;
                        if (coX - lineOfSight < 0)
                        {
                            imin = 0;
                        }
                        if (coY - lineOfSight < 1)
                        {
                            jmin = 1;
                        }
                        int imax = coX + lineOfSight;
                        int jmax = coY + lineOfSight;
                        if (coX + lineOfSight >= mapX)
                        {
                            imax = mapX;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY;
                        }
                        for (int i = imin; i < imax; i++)
                        {
                            if (this.map[i, jmax - 1])
                            {
                                map.Draw(i, jmax - 1, ConsoleColor.DarkGray);
                                this.map[i, jmax - 1] = false;
                            } 
                        }
                        for (int j = jmin; j < jmax; j++)
                        {
                            if (this.map[imax - 1, j])
                            {
                                map.Draw(imax - 1, j, ConsoleColor.DarkGray);
                                this.map[imax - 1, j] = false;
                            }    
                        }
                    }
                    break;

                case 9:
                    coX = X--;
                    coY = Y++;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY + lineOfSight])
                            {
                                map.Draw(i, coY + lineOfSight, ConsoleColor.DarkGray);
                                this.map[i, coY + lineOfSight] = false;
                            }     
                        }
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX - lineOfSight, j])
                            {
                                map.Draw(coX - lineOfSight, j, ConsoleColor.DarkGray);
                                this.map[coX - lineOfSight, j] = false;
                            }                               
                        }
                    }
                    else
                    {
                        int imin = coX - lineOfSight;
                        int jmin = coY - lineOfSight;
                        if (coX - lineOfSight < 0)
                        {
                            imin = 0;
                        }
                        if (coY - lineOfSight < 1)
                        {
                            jmin = 1;
                        }
                        int imax = coX + lineOfSight;
                        int jmax = coY + lineOfSight;
                        if (coX + lineOfSight >= mapX)
                        {
                            imax = mapX;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY;
                        }
                        for (int i = imin; i < imax; i++)
                        {
                            if (this.map[i, jmax - 1])
                            {
                                map.Draw(i, jmax - 1, ConsoleColor.DarkGray);
                                this.map[i, jmax - 1] = false;
                            }
                                
                        }
                        for (int j = jmin; j < jmax; j++)
                        {
                            if (this.map[imin, j])
                            {
                                map.Draw(imin, j, ConsoleColor.DarkGray);
                                this.map[imin, j] = false;
                            }                               
                        }
                    }
                    break;

                case 2:
                    coX = X;
                    coY = Y--;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY - lineOfSight])
                            {
                                map.Draw(i, coY - lineOfSight, ConsoleColor.DarkGray);
                                this.map[i, coY - lineOfSight] = false;
                            }
                        }
                    }
                    else
                    {
                        int imin = coX - lineOfSight;
                        int jmin = coY - lineOfSight;
                        if (coX - lineOfSight < 0)
                        {
                            imin = 0;
                        }
                        if (coY - lineOfSight < 1)
                        {
                            jmin = 1;
                        }
                        int imax = coX + lineOfSight;
                        if (coX + lineOfSight >= mapX)
                        {
                            imax = mapX;
                        }
                        for (int i = imin; i < imax; i++)
                        {
                            if (this.map[i, jmin])
                            {
                                map.Draw(i, jmin, ConsoleColor.DarkGray);
                                this.map[i, jmin] = false;
                            }

                        }
                    }
                    break;
                case 8:
                    coX = X;
                    coY = Y++;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY + lineOfSight])
                            {
                                map.Draw(i, coY + lineOfSight, ConsoleColor.DarkGray);
                                this.map[i, coY + lineOfSight] = false;
                            }
                        }
                    }
                    else
                    {
                        int imin = coX - lineOfSight;
                        if (coX - lineOfSight < 0)
                        {
                            imin = 0;
                        }
                        int imax = coX + lineOfSight;
                        int jmax = coY + lineOfSight;
                        if (coX + lineOfSight >= mapX)
                        {
                            imax = mapX;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY;
                        }
                        for (int i = imin; i < imax; i++)
                        {
                            if (this.map[i, jmax -1])
                            {
                                map.Draw(i, jmax - 1, ConsoleColor.DarkGray);
                                this.map[i, jmax - 1] = false;
                            }

                        }
                    }
                    break;
                case 6:
                    coX = X--;
                    coY = Y;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX - lineOfSight,j])
                            {
                                map.Draw(coX - lineOfSight, j, ConsoleColor.DarkGray);
                                this.map[coX - lineOfSight, j] = false;
                            }
                        }
                    }
                    else
                    {
                        int imin = coX - lineOfSight;
                        if (coX - lineOfSight < 0)
                        {
                            imin = 0;
                        }
                        int jmin = coY - lineOfSight;
                        if (coY - lineOfSight < 1)
                        {
                            jmin = 1;
                        }
                        int jmax = coY + lineOfSight;
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY;
                        }
                        for (int j = jmin; j < jmax; j++)
                        {
                            if (this.map[imin, j])
                            {
                                map.Draw(imin, j, ConsoleColor.DarkGray);
                                this.map[imin, j] = false;
                            }

                        }
                    }
                    break;
                case 4:
                    coX = X++;
                    coY = Y;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX + lineOfSight, j])
                            {
                                map.Draw(coX + lineOfSight, j, ConsoleColor.DarkGray);
                                this.map[coX + lineOfSight, j] = false;
                            }
                        }
                    }
                    else
                    {
                        int jmin = coY - lineOfSight;
                        if (coY - lineOfSight < 1)
                        {
                            jmin = 1;
                        }
                        int imax = coX + lineOfSight;
                        int jmax = coY + lineOfSight;
                        if (coX + lineOfSight >= mapX)
                        {
                            imax = mapX;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY;
                        }
                        for (int j = jmin; j < jmax; j++)
                        {
                            if (this.map[imax - 1, j])
                            {
                                map.Draw(imax - 1, j, ConsoleColor.DarkGray);
                                this.map[imax - 1, j] = false;
                            }

                        }
                    }
                    break;

            }
        }
    }
}
