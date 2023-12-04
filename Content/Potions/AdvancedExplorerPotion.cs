using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Potions
{
    /// <summary>
    /// 高级探险者药水
    /// </summary>
    internal class AdvancedExplorerPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Master;
            Item.buffType = ModContent.BuffType<Buffs.AdvancedExplorer>();
            Item.buffTime = 52000;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            //危险感知
            recipe.AddIngredient(ItemID.TrapsightPotion, 1);
            recipe.AddIngredient(ItemID.HunterPotion, 1);
            //洞穴探险
            recipe.AddIngredient(ItemID.SpelunkerPotion, 1);
            recipe.AddIngredient(ItemID.NightOwlPotion, 1);
            //挖矿
            recipe.AddIngredient(ItemID.MiningPotion, 1);
            recipe.AddIngredient(ItemID.GillsPotion, 1);
            recipe.AddIngredient(ItemID.FlipperPotion, 1);
            recipe.AddIngredient(ItemID.WaterWalkingPotion, 1);
            recipe.AddIngredient(ItemID.ObsidianSkinPotion, 1);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }
    }
}