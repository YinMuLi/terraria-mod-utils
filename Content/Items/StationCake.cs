using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    internal class StationCake : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.width = 18;
            Item.height = 28;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.buffType = ModContent.BuffType<Buffs.OriginalStationBuff>();
            Item.buffTime = 108000;
            return;
        }

        //TODO:增加合成配方
    }
}