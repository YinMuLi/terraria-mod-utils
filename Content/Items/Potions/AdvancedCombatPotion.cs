using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Branch.Content.Items.Potions
{
    /// <summary>
    /// 高级战斗药水
    /// </summary>
    internal class AdvancedCombatPotion : ModItem
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
            Item.buffType = ModContent.BuffType<Buffs.AdvancedCombat>();
            Item.buffTime = 52000;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type)
                .AddIngredient(ItemID.WrathPotion, 1)//怒气.
                .AddIngredient(ItemID.RagePotion, 1)//暴怒
                .AddIngredient(ItemID.ArcheryPotion, 1)//箭术药水
                .AddIngredient(ItemID.MagicPowerPotion, 1)//魔能药水
                .AddIngredient(ItemID.SummoningPotion, 1)//召唤药水
                .AddTile(TileID.AlchemyTable)
                .Register();
        }
    }
}