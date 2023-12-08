using Branch.Common.Utils;

namespace Branch.Common.Entity
{
    public struct TileData
    {
        public TileSort sort;
        public int x;
        public int y;

        public TileData(TileSort sort, int x, int y)
        {
            this.sort = sort;
            this.x = x;
            this.y = y;
        }
    }
}