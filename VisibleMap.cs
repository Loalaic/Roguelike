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

        bool[,] cmap;

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

            cmap = new bool[lineOfSight,lineOfSight];
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

            cmap = new bool[lineOfSight, lineOfSight];
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
                    imax = mapX - 1;
                }
                if (coY + lineOfSight >= mapY)
                {
                    jmax = mapY - 1;
                }
                for (int i = imin; i <= imax; i++)
                {
                    this.CastRay(coX, coY, i, jmin, map);
                }
                for (int i = imin; i <= imax; i++)
                {
                    this.CastRay(coX, coY, i, jmax, map);
                }
                for (int i = jmin; i <= jmax; i++)
                {
                    this.CastRay(coX, coY, imin, i, map);
                }
                for (int i = jmin; i <= jmax; i++)
                {
                    this.CastRay(coX, coY, imax, i, map);
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
                    imax = mapX - 1;
                }
                if (coY + lineOfSight >= mapY)
                {
                    jmax = mapY - 1;
                }
                for (int i = imin; i <= imax; i++)
                {
                    for (int j = jmin; j <= jmax; j++)
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
                    coX = X+1;
                    coY = Y-1;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY - lineOfSight])
                            {
                                map.Draw(i, coY - lineOfSight, ConsoleColor.DarkGray);
                            }   
                        }
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX + lineOfSight, j])
                            {
                                map.Draw(coX + lineOfSight, j, ConsoleColor.DarkGray);
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
                            imax = mapX - 1;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY - 1;
                        }
                        for (int i = imin; i <= imax; i++)
                        {
                            if (this.map[i, jmin])
                            {
                                map.Draw(i, jmin, ConsoleColor.DarkGray);
                            }  
                        }
                        for (int j = jmin; j <= jmax; j++)
                        {
                            if (this.map[imax, j])
                            {
                                map.Draw(imax, j, ConsoleColor.DarkGray);
                            }

                        }
                    }
                    break;

                case 3:
                    coX = X-1;
                    coY = Y-1;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY - lineOfSight])
                            {
                                map.Draw(i, coY - lineOfSight, ConsoleColor.DarkGray);
                            }

                        }
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX - lineOfSight, j])
                            {
                                map.Draw(coX - lineOfSight, j, ConsoleColor.DarkGray);
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
                            imax = mapX - 1;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY - 1;
                        }
                        for (int i = imin; i <= imax; i++)
                        {
                            if (this.map[i, jmin])
                            {
                                map.Draw(i, jmin, ConsoleColor.DarkGray);
                            }  
                        }
                        for (int j = jmin; j <= jmax; j++)
                        {
                            if (this.map[imin, j])
                            {
                                map.Draw(imin, j, ConsoleColor.DarkGray);
                            }
                        }
                    }
                    break;

                case 7:
                    coX = X+1;
                    coY = Y+1;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY + lineOfSight])
                            {
                                map.Draw(i, coY + lineOfSight, ConsoleColor.DarkGray);
                            }

                        }
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX + lineOfSight, j])
                            {
                                map.Draw(coX + lineOfSight, j, ConsoleColor.DarkGray);
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
                            imax = mapX - 1;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY - 1;
                        }
                        for (int i = imin; i <= imax; i++)
                        {
                            if (this.map[i, jmax])
                            {
                                map.Draw(i, jmax, ConsoleColor.DarkGray);
                            } 
                        }
                        for (int j = jmin; j <= jmax; j++)
                        {
                            if (this.map[imax, j])
                            {
                                map.Draw(imax, j, ConsoleColor.DarkGray);
                            }    
                        }
                    }
                    break;

                case 9:
                    coX = X-1;
                    coY = Y+1;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY + lineOfSight])
                            {
                                map.Draw(i, coY + lineOfSight, ConsoleColor.DarkGray);
                            }     
                        }
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX - lineOfSight, j])
                            {
                                map.Draw(coX - lineOfSight, j, ConsoleColor.DarkGray);
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
                            imax = mapX - 1;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY - 1;
                        }
                        for (int i = imin; i <= imax; i++)
                        {
                            if (this.map[i, jmax])
                            {
                                map.Draw(i, jmax, ConsoleColor.DarkGray);
                            }
                                
                        }
                        for (int j = jmin; j <= jmax; j++)
                        {
                            if (this.map[imin, j])
                            {
                                map.Draw(imin, j, ConsoleColor.DarkGray);
                            }                               
                        }
                    }
                    break;

                case 2:
                    coX = X;
                    coY = Y-1;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY - lineOfSight])
                            {
                                map.Draw(i, coY - lineOfSight, ConsoleColor.DarkGray);
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
                            imax = mapX - 1;
                        }
                        for (int i = imin; i <= imax; i++)
                        {
                            if (this.map[i, jmin])
                            {
                                map.Draw(i, jmin, ConsoleColor.DarkGray);
                            }

                        }
                    }
                    break;
                case 8:
                    coX = X;
                    coY = Y+1;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int i = coX - lineOfSight; i < coX + lineOfSight; i++)
                        {
                            if (this.map[i, coY + lineOfSight])
                            {
                                map.Draw(i, coY + lineOfSight, ConsoleColor.DarkGray);
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
                            imax = mapX - 1;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY - 1;
                        }
                        for (int i = imin; i <= imax; i++)
                        {
                            if (this.map[i, jmax])
                            {
                                map.Draw(i, jmax, ConsoleColor.DarkGray);
                            }

                        }
                    }
                    break;
                case 6:
                    coX = X-1;
                    coY = Y;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX - lineOfSight,j])
                            {
                                map.Draw(coX - lineOfSight, j, ConsoleColor.DarkGray);
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
                            jmax = mapY - 1;
                        }
                        for (int j = jmin; j <= jmax; j++)
                        {
                            if (this.map[imin, j])
                            {
                                map.Draw(imin, j, ConsoleColor.DarkGray);
                            }

                        }
                    }
                    break;
                case 4:
                    coX = X+1;
                    coY = Y;
                    if (coX - lineOfSight >= 0 & coY - lineOfSight >= 1 & coX + lineOfSight < mapX & coY + lineOfSight < mapY)
                    {
                        for (int j = coY - lineOfSight; j < coY + lineOfSight; j++)
                        {
                            if (this.map[coX + lineOfSight, j])
                            {
                                map.Draw(coX + lineOfSight, j, ConsoleColor.DarkGray);
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
                            imax = mapX - 1;
                        }
                        if (coY + lineOfSight >= mapY)
                        {
                            jmax = mapY - 1;
                        }
                        for (int j = jmin; j <= jmax; j++)
                        {
                            if (this.map[imax, j])
                            {
                                map.Draw(imax, j, ConsoleColor.DarkGray);
                            }

                        }
                    }
                    break;

            }
        }
    }
}
