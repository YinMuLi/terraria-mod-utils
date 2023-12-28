using Branch.Common.Interface;
using Branch.Common.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    internal class WarOrder : ModItem, IItemRightClickable
    {
        private static WarEvent currentWar;

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

        public void OnRightClicked(Item item)
        {
            if (item.type != Type) return;
            SoundEngine.PlaySound(SoundID.Unlock);
            currentWar = (WarEvent)(((int)currentWar + 1) % (int)WarEvent.Count);
        }

        public override bool CanUseItem(Player player)
        {
            return player.statLifeMax >= 200;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                switch (currentWar)
                {
                    case WarEvent.MartianMadness: Main.StartInvasion(InvasionID.MartianMadness); break;//火星暴乱
                    case WarEvent.GoblinArmy: Main.StartInvasion(InvasionID.GoblinArmy); break;//哥布林入侵
                    case WarEvent.PirateInvasion: Main.StartInvasion(InvasionID.PirateInvasion); break;//海盗入侵
                    case WarEvent.PumpkinMoon: Main.startPumpkinMoon(); break;
                    case WarEvent.SnowMoon: Main.startSnowMoon(); break;
                    case WarEvent.Eclipse: Main.eclipse = true; break;
                }
                return true;
            }
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            string content = currentWar switch
            {
                WarEvent.PumpkinMoon => Language.GetTextValue("Mods.Items.WarOrder.PumpkinMoon"),
                WarEvent.SnowMoon => Language.GetTextValue("Mods.Items.WarOrder.SnowMoon"),
                WarEvent.PirateInvasion => Language.GetTextValue("Mods.Items.WarOrder.PirateInvasion"),
                WarEvent.MartianMadness => Language.GetTextValue("Mods.Items.WarOrder.MartianMadness"),
                WarEvent.GoblinArmy => Language.GetTextValue("Mods.Items.WarOrder.GoblinArmy"),
                WarEvent.Eclipse => Language.GetTextValue("Mods.Items.WarOrder.Eclipse"),
                _ => null
            };
            list.Add(new TooltipLine(Mod, "WarEvent", content) { OverrideColor = Color.LightGreen });
        }
    }
}