using Branch.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            Item.value = Item.buyPrice(0, 1, 0, 0);//物品的价值
            Item.rare = ItemRarityID.Purple;//物品的稀有度
            Item.UseSound = SoundID.Item100;//物品使用时声音
            Item.useStyle = ItemUseStyleID.Swing;//物品的使用方式
            Item.mana = 20;//每次使用消耗的法力值
        }

        public override void HoldItem(Player player)
        {
            DrawPreview(5, 10, player);
        }

        /// <summary>
        /// 绘制房屋大小预览
        /// </summary>
        /// <param name="width">横向长度</param>
        /// <param name="height">纵向长度</param>
        private void DrawPreview(int width, int height, Player player)
        {
            if (!(player.ownedProjectileCounts[ModContent.ProjectileType<RectangleProjectile>()] > 30))
            {
                for (int i = -(width - width / 2); i <= width / 2; i++)
                {
                    for (int j = -(height - height / 2); j <= height / 2; j++)
                    {
                        Vector2 mouse = Main.MouseWorld;
                        mouse.X += i * 16;
                        mouse.Y += j * 16;
                        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item), mouse, Vector2.Zero, ModContent.ProjectileType<RectangleProjectile>(), 0, 0f, player.whoAmI, 0f, 0f);
                    }
                }
            }
        }
    }
}