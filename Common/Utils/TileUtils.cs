using Terraria;
using Terraria.ID;

namespace Branch.Common.Utils
{
    internal class TileUtils
    {
        /// <summary>
        /// 放置物块(方块，平台，建筑物)
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void PlaceTile(Player player, Item item, int x, int y)
        {
            //TODO:物块的魔杖判断
            if (Main.tile[x, y].HasTile)
            {
                if (!WorldGen.ReplaceTile(x, y, (ushort)item.createTile, item.placeStyle))
                {
                    //替换不成功，就摧毁
                    KillTile(player, x, y);
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
        /// 摧毁物块，需要玩家有能挖动物块的镐子
        /// </summary>
        /// <param name="player"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool KillTile(Player player, int x, int y)
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

        /// <summary>
        /// 摧毁这个位置上的方块和墙体
        /// </summary>
        /// <param name="player"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool KillAll(Player player, int x, int y)
        {
            WorldGen.KillWall(x, y);
            return KillTile(player, x, y);
        }
    }
}