using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    internal class GoldenBait : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(silver: 30);
            Item.bait = 65;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ApprenticeBait, 100)
                .AddIngredient(ItemID.JourneymanBait, 100)
                .AddIngredient(ItemID.MasterBait, 100)
                .Register();
        }
    }
}