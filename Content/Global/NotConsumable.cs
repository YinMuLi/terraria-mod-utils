using Branch.Common.Configs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Branch.Content.Global
{
    internal class NotConsumable : GlobalItem
    {
        private static int[] invaildBoss =
        {
            ItemID.Sets.SortingPriorityBossSpawns[ItemID.LifeCrystal],
            ItemID.Sets.SortingPriorityBossSpawns[ItemID.LifeFruit],
            ItemID.Sets.SortingPriorityBossSpawns[ItemID.ManaCrystal],
            ItemID.Sets.SortingPriorityBossSpawns[ItemID.CellPhone],
            ItemID.Sets.SortingPriorityBossSpawns[ItemID.PDA],
            ItemID.Sets.SortingPriorityBossSpawns[ItemID.MagicMirror],
            ItemID.Sets.SortingPriorityBossSpawns[ItemID.IceMirror],
            ItemID.Sets.SortingPriorityBossSpawns[ItemID.TreasureMap],
        };

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.BossSpawnNotConsumable;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
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

        private bool IsBossSpawn(Item item)
        {
            return ItemID.Sets.SortingPriorityBossSpawns[item.type] >= 0
                && Array.IndexOf(invaildBoss, ItemID.Sets.SortingPriorityBossSpawns[item.type]) == -1;
        }
    }
}