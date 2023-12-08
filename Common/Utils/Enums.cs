using System;

namespace Branch.Common.Utils
{
    /// <summary>
    /// 瓦片类别
    /// </summary>
    [Flags]
    public enum TileSort
    {
        None,

        /// <summary>
        /// 火把
        /// </summary>
        Troch,

        Wall,
        NoWall,
        Block,
        Platform,
        Workbench,
        Chair
    }
}