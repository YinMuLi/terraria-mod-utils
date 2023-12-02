using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Modify
{
    /// <summary>
    /// 修改商人商店
    /// </summary>
    public class MerchantShop : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType != NPCID.Merchant) return;
            AddShopItem(shop);
        }

        private void AddShopItem(NPCShop shop)
        {
            var items = new short[]
            {
            //声呐药水
            ItemID.SonarPotion,
            //钓鱼药水
            ItemID.FishingPotion,
            //宝匣药水
            ItemID.CratePotion,
            //洞穴探险药水
            ItemID.SpelunkerPotion,
            //大师诱饵
            ItemID.MasterBait,
            //渔夫一套
            ItemID.AnglerHat,
            ItemID.AnglerVest,
            ItemID.AnglerPants,
            ItemID.GoldenFishingRod,
            ItemID.GoldenBugNet,
            ItemID.AnglerEarring,
            ItemID.TackleBox,
            ItemID.FishermansGuide,
            ItemID.WeatherRadio,
            ItemID.Sextant,
            //防熔岩钓钩
            ItemID.LavaFishingHook,
            //优质钓鱼线
            ItemID.HighTestFishingLine,
            ItemID.SuperAbsorbantSponge,
            ItemID.BottomlessBucket,
            };
            for (int i = 0; i < items.Length; i++)
            {
                shop.Add(new Item(items[i]));
            }
        }
    }
}