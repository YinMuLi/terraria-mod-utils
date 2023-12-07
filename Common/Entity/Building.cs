using Branch.Common.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branch.Common.Entity
{
    /// <summary>
    /// 建筑也可以成为监狱
    /// </summary>
    internal class Building
    {
        public int Width => width;
        public int Height => height;

        private int width;
        private int height;
        private TileSort[,] array;

        public TileSort this[int x, int y]
        {
            get { return array[x, y]; }
            set { array[x, y] = value; }
        }

        public Building(int width, int height)
        {
            this.width = width;
            this.height = height;
            array = new TileSort[height, width];
        }

        public void SetSortColumn(TileSort sort, int column, bool line = true, int start = 0, int end = 0)
        {
            if (line)
            {
                end = height - 1;
            }
            for (int i = start; i <= end; i++)
            {
                array[i, column] = sort;
            }
        }

        public void SetSortRow(TileSort sort, int row, bool line = true, int start = 0, int end = 0)
        {
            if (line)
            {
                end = width - 1;
            }
            for (int i = start; i <= end; i++)
            {
                array[row, i] = sort;
            }
        }

        public void SetSortArea(TileSort sort, Vector2 start, Vector2 end)
        {
            for (int i = (int)start.X; i <= end.X; i++)
            {
                for (int j = (int)start.Y; j <= end.Y; j++)
                {
                    array[i, j] = sort;
                }
            }
        }
    }
}