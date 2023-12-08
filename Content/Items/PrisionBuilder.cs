using Branch.Common.Entity;
using Branch.Common.Utils;
using Branch.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    internal class PrisionBuilder : ModItem
    {
        private static TileSort[,] prision;

        public override void Load()
        {
            //不是死服务？
            if (!Main.dedServ)
            {
                //没有任何物块
                TileSort a = TileSort.None;
                //有方块
                TileSort b = TileSort.Block;
                //平台
                TileSort c = TileSort.Platform;
                //墙
                TileSort d = TileSort.Wall;
                //工作台
                TileSort e = TileSort.Wall | TileSort.Workbench;
                //椅子
                TileSort f = TileSort.Chair | TileSort.Wall;
                //火把
                TileSort g = TileSort.Troch;
                prision = new TileSort[10, 6] {
                    { b,b,b,b,b,b},
                    { b,d,d,d,d,b},
                    { b,d,d,d,d,b},
                    { b,d,d,d,d,b},
                    { b,f,e,d,d,b},
                    { b,b,b,b,g,b},
                    { c,a,a,a,a,c},
                    { c,a,a,a,a,c},
                    { c,a,a,a,a,c},
                    { c,c,c,c,c,c},
                };
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                prision = null;
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
            List<TileData> left = new();
            Item block = new Item(ItemID.BambooBlock);
            Item platform = new Item(ItemID.GlassPlatform);
            Item wall = new Item(ItemID.GlassWall);
            for (int i = 0; i < prision.GetLength(0); i++)
            {
                for (int j = 0; j < prision.GetLength(1); j++)
                {
                    TileSort sort = prision[i, j];
                    int x = Player.tileTargetX - prision.GetLength(1) / 2 + j;
                    int y = Player.tileTargetY - prision.GetLength(0) / 2 + i;
                    if (sort.HasFlag(TileSort.Wall))
                    {
                        PlaceWall(wall, player, x, y);
                    }
                    if (sort.HasFlag(TileSort.Block))
                    {
                        TileUtils.PlaceTile(player, block, x, y);
                    }
                    if (sort.HasFlag(TileSort.Platform))
                    {
                        TileUtils.PlaceTile(player, platform, x, y);
                    }
                    if (sort.HasFlag(TileSort.Troch) || sort.HasFlag(TileSort.Chair) || sort.HasFlag(TileSort.Workbench))
                    {
                        left.Add(new(sort, x, y));
                    }
                }
            }

            for (int i = 0; i < left.Count; i++)
            {
                if (left[i].sort.HasFlag(TileSort.Troch))
                {
                    TileUtils.PlaceTile(player, new(ItemID.RainbowTorch), left[i].x, left[i].y);
                }
                if (left[i].sort.HasFlag(TileSort.Workbench))
                {
                    TileUtils.PlaceTile(player, new(ItemID.WorkBench), left[i].x, left[i].y);
                }
                if (left[i].sort.HasFlag(TileSort.Chair))
                {
                    TileUtils.PlaceTile(player, new(ItemID.BambooChair), left[i].x, left[i].y);
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