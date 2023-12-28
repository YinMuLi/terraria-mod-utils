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
        private static Weather currentWeather;

        public override void SetDefaults()
        {
            Item.width = 32;//贴图的宽度
            Item.height = 32;//贴图的高度
            Item.rare = ItemRarityID.Red;//物品的稀有度
            Item.useAnimation = 18;//每次使用时动画播放时间
            Item.useTime = 18;//使用一次所需时间
            Item.UseSound = SoundID.Item100;//物品使用时声音
            Item.useStyle = ItemUseStyleID.HoldUp;//物品的使用方式
            Item.autoReuse = false;
            Item.mana = 20;//每次使用消耗的法力值
        }

        public override bool CanUseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                switch (currentWeather)
                {
                    case Weather.Rainning: StartRainning(); break;
                    case Weather.Sandstorm: StartSandstorm(); break;
                    case Weather.BloodMoon: StartBloodMoon(); break;
                    case Weather.SlimeRain: StartSlimeRain(); break;
                }
                return true;
            }
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            string content = currentWeather switch
            {
                Weather.Rainning => Language.GetTextValue("Mods.Items.WeatherReport.Rainning"),
                Weather.Sandstorm => Language.GetTextValue("Mods.Items.WeatherReport.Sandstorm"),
                Weather.BloodMoon => Language.GetTextValue("Mods.Items.WeatherReport.BloodMoon"),
                Weather.SlimeRain => Language.GetTextValue("Mods.Items.WeatherReport.SlimeRain"),
                _ => null
            };
            list.Add(new TooltipLine(Mod, "Weather", content) { OverrideColor = Color.LightGreen });
        }

        private void StartRainning()
        {
            if (Main.IsItRaining) return;
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
            if (!Main.dayTime && !Main.bloodMoon)
            {
                Main.bloodMoon = true;
                ModUtils.SnycWorld();
            }
        }

        private void StartSlimeRain()
        {
            if (Main.slimeRain) return;
            Main.StartSlimeRain();
            //这两个参数是从左下角文字提醒下史莱姆雨到真正下史莱姆雨的时间
            Main.slimeWarningDelay = 1;//默认420
            Main.slimeWarningTime = 1;
            ModUtils.SnycWorld();
        }

        public void OnRightClicked(Item item)
        {
            if (item.type != Type) return;
            SoundEngine.PlaySound(SoundID.Unlock);
            currentWeather = (Weather)(((int)currentWeather + 1) % (int)Weather.Count);//这样写想笑
        }

        public override void AddRecipes()
        {
            Recipe.Create(Item.type)
                .AddIngredient(ItemID.Goldfish, 1)
                .AddIngredient(ItemID.Gel, 10)
                .AddIngredient(ItemID.Gravestone, 1)//墓碑
                .AddIngredient(ItemID.AntlionMandible, 1)//蚁狮上颚
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}