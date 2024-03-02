using Branch.Common.Configs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Branch.Content.Global
{
    internal class NotConsumable : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.BossSpawnNotConsumable;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //Boss召唤物
            if (IsBossSpawn(item) && item.consumable)
            {
                foreach (var line in tooltips)
                {
                    if (line.Mod.Equals("Terraria") && line.Name.Equals("Consumable"))
                    {
                        line.Text = Language.GetTextValue("Mods.Items.NotConsumable");
                        line.OverrideColor = Color.Aqua;
                    }
                }
            }
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (IsBossSpawn(item) && item.consumable)
            {
                return false;
            }
            return base.ConsumeItem(item, player);
        }

        /// <summary>
        /// 是否和模组中的物品类型相等
        /// </summary>
        /// <param name="mod">模组实例</param>
        /// <param name="item">物品</param>
        /// <param name="compareItemName">模组中比较物品名称</param>
        /// <returns>True:相等</returns>
        private bool IsModItemEquals(Mod mod, Item item, string compareItemName)
        {
            if (mod != null && mod.TryFind<ModItem>(compareItemName, out ModItem modItem))
            {
                return item.type == modItem.Type;
            }
            return false;
        }

        private bool IsBossSpawn(Item item)
        {
            //灾厄...很笨的方法
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                if (IsModItemEquals(calamity, item, "MiracleFruit")) return false; //奇迹之果
                if (IsModItemEquals(calamity, item, "BloodOrange")) return false; //血橙
                if (IsModItemEquals(calamity, item, "Elderberry")) return false; //旧神浆果
                if (IsModItemEquals(calamity, item, "Dragonfruit")) return false; //龙果
                if (IsModItemEquals(calamity, item, "EnchantedStarfish")) return false; //附魔星鱼
                if (IsModItemEquals(calamity, item, "CometShard")) return false; //彗星碎片
                if (IsModItemEquals(calamity, item, "EtherealCore")) return false; //飘渺之核
                if (IsModItemEquals(calamity, item, "PhantomHeart")) return false; //幻影之心
            }
            return item.type switch

            {
                ItemID.LifeCrystal => false,
                ItemID.LifeFruit => false,
                ItemID.ManaCrystal => false,
                ItemID.CellPhone => false,
                ItemID.PDA => false,
                ItemID.MagicMirror => false,
                ItemID.IceMirror => false,
                ItemID.TreasureMap => false,
                ItemID.TruffleWorm => false,//松露虫
                _ => ItemID.Sets.SortingPriorityBossSpawns[item.type] >= 0
            };
        }
    }
}