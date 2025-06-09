using YinMu.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YinMu.Content.Items.Misc
{
    internal class Cake : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTurn = true;
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.buffType = ModContent.BuffType<CommonStationBuff>();
            Item.buffTime = 108000;
        }

        //public override void AddRecipes()
        //{
        //    //TODO:任意篝火
        //    Recipe.Create(Item.type)
        //        .AddIngredient(ItemID.Campfire, 1)
        //        .AddIngredient(ItemID.HeartLantern, 1)
        //        .AddIngredient(ItemID.HoneyBucket, 1)
        //        .AddIngredient(ItemID.HoneyBucket, 1)
        //        .AddIngredient(ItemID.BewitchingTable, 1)
        //        .AddIngredient(ItemID.SharpeningStation, 1)
        //        .AddIngredient(ItemID.CrystalBall, 1)
        //        .AddIngredient(ItemID.AmmoBox, 1)
        //        .AddIngredient(ItemID.SliceOfCake, 1)
        //        .AddIngredient(ItemID.StarinaBottle, 1)
        //        .AddIngredient(ItemID.WarTable, 1)
        //        .AddIngredient(ItemID.CatBast, 1)//雕像
        //        .Register();
        //}
    }
}