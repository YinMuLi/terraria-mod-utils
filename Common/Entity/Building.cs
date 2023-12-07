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
            array = new TileSort[width, height];
        }

        public void SetSort(TileSort sort, int x, int y)
        {
        }

        public void SetSortColumn(TileSort sort, int column, int? start, int? end)
        {
        }

        public void SetSortRow(TileSort sort, int row, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                array[row, i] = sort;
            }
        }

        public void SetSortArea(TileSort sort, Vector2 start, Vector2 end)
        {
        }
    }
}