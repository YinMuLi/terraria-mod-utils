﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YinMu.Content.Items.Potions
{
    internal class EnhancedSurvivalPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
        }

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
            Item.buffType = ModContent.BuffType<Buffs.EnhancedSurvival>();
            Item.buffTime = 52000;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.RegenerationPotion, 1);
            recipe.AddIngredient(ItemID.LifeforcePotion, 1);
            recipe.AddIngredient(ItemID.IronskinPotion, 1);
            recipe.AddIngredient(ItemID.EndurancePotion, 1);
            recipe.AddIngredient(ItemID.ThornsPotion, 1);
            recipe.AddIngredient(ItemID.WarmthPotion, 1);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }
    }
}