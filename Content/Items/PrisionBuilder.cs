using Branch.Common;
using Branch.Content.Projectiles;
using Microsoft.Xna.Framework;
using rail;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Branch.Common.Utils;
using Branch.Common.Entity;
using System.Collections.Generic;

namespace Branch.Content.Items
{
    internal class PrisionBuilder : ModItem
    {
        private static Building prision;

        public override void Load()
        {
            //不是死服务？
            if (!Main.dedServ)
            {
                prision = new(6, 10);
                prision.SetSortRow(TileSort.Block, 0);
                prision.SetSortRow(TileSort.Block, 5, false, 0, 3);
                prision.SetSortRow(TileSort.Platform, 9);
                prision.SetSortColumn(TileSort.Block, 0, false, 1, 4);
                prision.SetSortColumn(TileSort.Block, 5, false, 1, 5);
                prision.SetSortColumn(TileSort.Platform, 0, false, 7, 9);
                prision.SetSortColumn(TileSort.Platform, 5, false, 7, 9);
                Vector2 start = new(1, 1);
                Vector2 end = new(4, 4);
                prision.SetSortArea(TileSort.Wall, start, end);

                prision[5, 4] = TileSort.Troch;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 1);
            recipe.Register();
        }

        public override void SetDefaults()
        {
            Item.width = 20;//物品掉落时的碰撞体宽度
            Item.height = 20;//物品掉落时碰撞体高度
            Item.value = Item.buyPrice(0, 0, 50, 0);//物品的价值
            Item.rare = ItemRarityID.Purple;//物品的稀有度
            Item.UseSound = SoundID.Item100;//物品使用时声音
            Item.useStyle = ItemUseStyleID.Swing;//物品的使用方式
            Item.useAnimation = 10;
            Item.useTime = 10;
        }

        public override void HoldItem(Player player)
        {
            //DrawPreview(6, 10, player);
        }

        /// <summary>
        /// 绘制房屋大小预览
        /// </summary>
        /// <param name="width">横向长度</param>
        /// <param name="height">纵向长度</param>
        private void DrawPreview(int width, int height, Player player)
        {
            //限制抛射的数量
            if (!(player.ownedProjectileCounts[ModContent.ProjectileType<RectangleProjectile>()] > 30))
            {
                for (int i = -(width - width / 2); i < width / 2; i++)
                {
                    for (int j = -(height - height / 2); j < height / 2; j++)
                    {
                        Vector2 mouse = Main.MouseWorld;
                        mouse.X += i * 16;
                        mouse.Y += j * 16;
                        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item), mouse, Vector2.Zero, ModContent.ProjectileType<RectangleProjectile>(), 0, 0f, player.whoAmI, 0f, 0f);
                    }
                }
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && !player.noBuilding)
            {
                Building(player);
            }
            return true;
        }

        private void Building(Player player)
        {
            Dictionary<TileSort, Vector2> left = new();
            int x = Player.tileTargetX;
            int y = Player.tileTargetY;
            Item block = new Item(ItemID.BambooBlock);
            Item platform = new Item(ItemID.GlassPlatform);
            Item wall = new Item(ItemID.GlassWall);
            for (int i = 0; i < prision.Height; i++)
            {
                for (int j = 0; j < prision.Width; j++)
                {
                    TileSort sort = prision[i, j];
                    int placeX = x - prision.Width / 2 + j;
                    int placeY = y - prision.Height / 2 + i;
                    if (sort == TileSort.Wall)
                    {
                        PlaceWall(wall, player, placeX, placeY);
                    }
                    switch (sort)
                    {
                        case TileSort.Block:
                            TileUtils.PlaceTile(player, block, placeX, placeY);
                            break;

                        case TileSort.Platform:
                            TileUtils.PlaceTile(player, platform, placeX, placeY);
                            break;

                        case TileSort.Troch:
                            left.Add(sort, new(placeX, placeY));
                            break;
                    }
                }
            }
            foreach (var item in left)
            {
                if (item.Key == TileSort.Troch)
                {
                    TileUtils.PlaceTile(player, new Item(ItemID.RainbowTorch), (int)item.Value.X, (int)item.Value.Y);
                }
            }
        }

        /// <summary>
        /// 放置墙体，会把墙体前的方块破坏掉
        /// </summary>
        /// <param name="item">墙物品</param>
        /// <param name="player">玩家</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>bool</returns>
        private bool PlaceWall(Item item, Player player, int x, int y)
        {
            //createWall是该物品墙属性的ID==WallID.xxx
            if (item.createWall > -1)
            {
                TileUtils.KillTile(x, y, player);
                WorldGen.KillWall(x, y);
                if (Main.tile[x, y].WallType == 0)
                {
                    WorldGen.PlaceWall(x, y, item.createWall, true);
                    return true;
                }
            }
            return false;
        }
    }
}