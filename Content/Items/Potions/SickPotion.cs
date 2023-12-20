using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items.Potions
{
    internal class SickPotion : ModItem
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
            Item.buffType = ModContent.BuffType<Buffs.SickBlade>();
            Item.buffTime = 52000;
        }

        public override void AddRecipes()
        {
            Recipe.Create(Item.type)
                .AddIngredient(ItemID.Fireblossom, 1)//火焰花
                .AddIngredient(ItemID.Shiverthorn, 1)//寒颤棘
                .AddIngredient(ItemID.Ichor, 1)//灵液
                .AddIngredient(ItemID.CursedFlame, 1)//诅咒焰
                .AddIngredient(ItemID.Stinger, 1)//毒刺
                .AddIngredient(ItemID.BottledWater, 1)//水瓶
                .AddTile(TileID.ImbuingStation)//灌注站
                    .Register();
        }
    }
}