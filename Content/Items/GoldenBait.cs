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
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(silver: 30);
            Item.bait = 65;
            Item.consumable = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ApprenticeBait)
                .AddIngredient(ItemID.JourneymanBait)
                .AddIngredient(ItemID.MasterBait)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.JourneymanBait, 10)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.MasterBait, 5)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.ApprenticeBait, 20)
                .Register();
        }
    }
}