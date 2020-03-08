using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    class Levels
    {
        private int dLevel = 0;
        private Map[] maps;
        private VisibleMap[] vmaps;

        public Levels(int level)
        {
            dLevel = level;
            maps = new Map[dLevel];
            vmaps = new VisibleMap[dLevel];
            dLevel = 0;
        }

        public void InitLevels()
        {
            for (int i = 0; i < maps.Length; i++)
            {
                maps[i] = new Map(120, 29);
                maps[i].GenerateDungeonRooms(5 + (i / 10));
                maps[i].GenerateDungeonDoors();
                maps[i].GenerateDungeonPaths();
                maps[i].RemoveExcessiveDoors();
                maps[i].Colorize();
                vmaps[i] = new VisibleMap(120, 29);
            }
        }

        public Map getCurrentMap()
        {
            return maps[dLevel];
        }

        public VisibleMap getCurrentVisibleMap()
        {
            return vmaps[dLevel];
        }

        public void goDownStairs()
        {
            dLevel++;
        }

        public void goUpStairs()
        {
            dLevel--;
        }

        public int getDLevel()
        {
            return dLevel;
        }

        public void setDLevel(int dLevel)
        {
            this.dLevel = dLevel;
        }
    }
}

