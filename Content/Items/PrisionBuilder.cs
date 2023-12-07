using Branch.Common;
using Branch.Content.Projectiles;
using Microsoft.Xna.Framework;
using rail;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Branch.Common.Utils;

namespace Branch.Content.Items
{
    internal class PrisionBuilder : ModItem
    {
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sundial, 1);
            recipe.AddTile(TileID.WorkBenches);
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
                        Broadcast.Print(height - height / 2);
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
            int x = Player.tileTargetX; int y = Player.tileTargetY;
            TileUtils.PlaceTile(player, new Item(ItemID.Wood), x, y);
            //TryPlaceWall(new Item(ItemID.GlassWall), player, Player.tileTargetX, Player.tileTargetY);
        }

        /// <summary>
        /// 放置墙体，会把墙体前的方块破坏掉
        /// </summary>
        /// <param name="item">墙物品</param>
        /// <param name="player">玩家</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>bool</returns>
        private bool TryPlaceWall(Item item, Player player, int x, int y)
        {
            //createWall是该物品墙属性的ID==WallID.xxx
            if (item.createWall > -1)
            {
                TileUtils.KillTile(x, y, player);
                WorldGen.KillWall(x, y);
                if (Main.tile[x, y].WallType == 0)
                {
                    WorldGen.PlaceWall(x, y, item.createWall, true);
                }
            }
            return false;
        }
    }
}