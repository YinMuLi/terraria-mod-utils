using Branch.Common.Interface;
using Branch.Common.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    /// <summary>
    /// 本来想搞一个UI的但发现气候数量太少了，索性搞成像贝壳电话那样的形式
    /// </summary>
    internal class WeatherReport : ModItem, IItemRightClickable
    {
        public static Weather currentWeather;

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
            if (player.whoAmI == Main.myPlayer && !Main.dedServ)
            {
                if (player.altFunctionUse == 2)
                {
                    //右键
                    RightClick();
                    return false;
                }
                else
                {
                    switch (currentWeather)
                    {
                        case Weather.Rainning: StartRainning(); break;
                        case Weather.Sandstorm: StartSandstorm(); break;
                        case Weather.BloodMoon: StartBloodMoon(); break;
                    }
                    return true;
                }
            }
            return false;
        }

        private void RightClick()
        {
            SoundEngine.PlaySound(SoundID.Unlock);
            currentWeather = (Weather)(((int)currentWeather + 1) % (int)Weather.Count);//这样写想笑
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            string content = currentWeather switch
            {
                Weather.Rainning => Language.GetTextValue("Mods.Items.WeatherReport.Rainning"),
                Weather.Sandstorm => Language.GetTextValue("Mods.Items.WeatherReport.Sandstorm"),
                Weather.BloodMoon => Language.GetTextValue("Mods.Items.WeatherReport.BloodMoon"),
                _ => null
            };
            list.Add(new TooltipLine(Mod, "Weather", content) { OverrideColor = Color.LightGreen });
        }

        private void StartRainning()
        {
            //灯笼夜
            LanternNight.ManualLanterns = false;
            LanternNight.GenuineLanterns = false;
            //下12小时雨
            int day = 86400;
            int hour = day / 24;
            Main.rainTime = hour * 12;
            Main.raining = true;
            Main.maxRaining = Main.cloudAlpha = 0.9f;
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData);
                Main.SyncRain();
            }
        }

        private void StartSandstorm()
        {
            if (Sandstorm.Happening) return;
            Sandstorm.StartSandstorm();
            ModUtils.SnycWorld();
        }

        private void StartBloodMoon()
        {
            if (!Main.dayTime)
            {
                Main.bloodMoon = true;
                ModUtils.SnycWorld();
            }
        }

        public void OnRightClicked(Item item)
        {
            if (item.type != Type) return;
            RightClick();
        }
    }
}