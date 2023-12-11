using Branch.Content.Titles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    internal class FinalStation : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<FinalStationTile>());
            Item.width = 48;
            Item.height = 48;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            //TODO:任意篝火
            Recipe.Create(Item.type)
                .AddIngredient(ItemID.Campfire, 1)
                .AddIngredient(ItemID.HeartLantern, 1)
                .AddIngredient(ItemID.HoneyBucket, 1)
                .AddIngredient(ItemID.HoneyBucket, 1)
                .AddIngredient(ItemID.BewitchingTable, 1)
                .AddIngredient(ItemID.SharpeningStation, 1)
                .AddIngredient(ItemID.CrystalBall, 1)
                .AddIngredient(ItemID.AmmoBox, 1)
                .AddIngredient(ItemID.SliceOfCake, 1)
                .AddIngredient(ItemID.StarinaBottle, 1)
                .AddIngredient(ItemID.WarTable, 1)
                .AddIngredient(ItemID.CatBast, 1)//雕像
                .Register();
        }
    }
}