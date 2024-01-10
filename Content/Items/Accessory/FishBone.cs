using Branch.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items.Accessory
{
    internal class FishBone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        private void Effect(Player player)
        {
            player.fishingSkill += 80;//渔力+80
            //钓鱼袋
            player.accFishingLine = true;//不断线
            player.accTackleBox = true;//钓鱼箱：不消耗鱼饵的几率
            player.accLavaFishing = true;//熔岩钓鱼
            //药水
            player.sonarPotion = true;//声呐药水
            player.cratePotion = true;//宝匣药水
        }

        //装饰栏生效
        public override void UpdateVanity(Player player) => Effect(player);

        public override void UpdateAccessory(Player player, bool hideVisual) => Effect(player);

        public override void AddRecipes()
        {
            Recipe.Create(Type)
                .AddIngredient(ItemID.LavaproofTackleBag, 1)
                .AddIngredient(ItemID.MasterBait, 10)
                .AddIngredient(ItemID.SoulofLight, 5)//光明之魂
                .AddIngredient(ItemID.SoulofNight, 5)//暗影之魂
                .AddTile(TileID.TinkerersWorkbench)//工匠作坊
                .Register();
        }
    }
}