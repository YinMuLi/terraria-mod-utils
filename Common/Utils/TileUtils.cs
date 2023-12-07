using Terraria;
using Terraria.ID;

namespace Branch.Common.Utils
{
    internal class TileUtils
    {
        /// <summary>
        /// 先摧毁物块，在放置物块
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static void PlaceTile(Player player, Item item, int x, int y)
        {
            //TODO:物块的魔杖判断
            if (Main.tile[x, y].HasTile)
            {
                if (!WorldGen.ReplaceTile(x, y, (ushort)item.createTile, item.placeStyle))
                {
                    //替换不成功，就摧毁
                    KillTile(x, y, player);
                    //mute:放置物块是否有声音
                    WorldGen.PlaceTile(x, y, item.createTile, true, true, player.whoAmI, item.placeStyle);
                }
            }
            else
            {
                WorldGen.PlaceTile(x, y, item.createTile, true, true, player.whoAmI, item.placeStyle);
            }
        }

        /// <summary>
        /// 是否与[x,y]位置的物块一样
        /// </summary>
        /// <param name="item"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool IsSameTile(Item item, int x, int y)
        {
            int createTile = item.createTile;
            Tile originTile = Main.tile[x, y];

            // 平台
            if (TileID.Sets.Platforms[originTile.TileType] && originTile.TileType == createTile)
                return originTile.TileFrameY == item.placeStyle * 18;

            // 箱子
            if (TileID.Sets.BasicChest[originTile.TileType] && TileID.Sets.BasicChest[createTile])
            {
                if (originTile.TileFrameX / 36 == item.placeStyle)
                {
                    return originTile.TileType == createTile;
                }
                return false;
            }

            // 梳妆台
            if (TileID.Sets.BasicDresser[originTile.TileType] && TileID.Sets.BasicDresser[createTile])
            {
                if (originTile.TileFrameX / 54 == item.placeStyle)
                {
                    return originTile.TileType == createTile;
                }
                return false;
            }

            // 是不是一样的物块
            if (Main.tile[x, y].TileType != createTile)
                return false;

            return false;
        }

        /// <summary>
        /// 摧毁物块，需要玩家有能挖动物块的镐子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool KillTile(int x, int y, Player player)
        {
            //TileType:这个位置tile的ID
            Tile tile = Main.tile[x, y];
            if (tile.HasTile && !Main.tileHammer[tile.TileType])
            {
                if (player.HasEnoughPickPowerToHurtTile(x, y))
                {
                    if (TileID.Sets.Grass[tile.TileType] || TileID.Sets.GrassSpecial[tile.TileType] || Main.tileMoss[tile.TileType] || TileID.Sets.tileMossBrick[tile.TileType])
                    {
                        //如果这个地方长草或者其它什么东西需要摧毁两次
                        player.PickTile(x, y, 1000);
                    }
                    player.PickTile(x, y, 1000);
                }
            }
            return !Main.tile[x, y].HasTile;
        }
    }
}