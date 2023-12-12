using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    /// <summary>
    /// 便携式日晷，可以调整时间
    /// </summary>
    public class PortableSundial : ModItem
    {
        /// <summary>
        /// 配方：日晷
        /// </summary>
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
            Item.useAnimation = 30;//每次使用时动画播放时间
            Item.useTime = 30;//使用一次所需时间
            Item.UseSound = SoundID.Item100;//物品使用时声音
            Item.useStyle = ItemUseStyleID.Shoot;//物品的使用方式
            Item.mana = 20;//每次使用消耗的法力值
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Main.time = 54000.0;
                CultistRitual.delay = 0.0;
                CultistRitual.recheck = 0.0;
            }
            return true;
        }
    }
}