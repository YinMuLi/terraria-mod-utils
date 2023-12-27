using Branch.Common.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    internal class WeatherReport : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 25;//贴图的宽度
            Item.height = 25;//贴图的高度
            Item.value = Item.buyPrice(0, 1, 0, 0);//物品的价值
            Item.rare = ItemRarityID.Red;//物品的稀有度
            Item.useAnimation = 18;//每次使用时动画播放时间
            Item.useTime = 18;//使用一次所需时间
            Item.UseSound = SoundID.Item100;//物品使用时声音
            Item.useStyle = ItemUseStyleID.HoldUp;//物品的使用方式
            Item.autoReuse = false;
            Item.mana = 20;//每次使用消耗的法力值
        }

        //允许右键使用
        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.altFunctionUse == 2 && !Main.dedServ)
            {
                WeatherReportUI.Visible = !WeatherReportUI.Visible;
                return false;
            }
            return true;
        }
    }
}