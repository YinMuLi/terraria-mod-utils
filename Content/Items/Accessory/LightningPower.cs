using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items.Accessory
{
    internal class LightningPower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.npc.Any(t => t.active && t.boss)) return;
            player.runAcceleration = 20f;
            player.maxRunSpeed = 30f;
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type)
                .AddIngredient(ItemID.MythrilOre, 10)
                .AddIngredient(ItemID.SoulofLight, 5)//光明之魂
                .AddIngredient(ItemID.SoulofNight, 5)//暗影之魂
                .AddTile(TileID.TinkerersWorkbench)//工匠作坊
                .Register();
        }
    }
}