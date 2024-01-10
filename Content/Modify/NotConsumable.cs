using Branch.Common.Configs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Branch.Content.Modify
{
    internal class NotConsumable : GlobalItem
    {
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
            return item.type switch
            {
                ItemID.LifeCrystal => false,
                ItemID.ManaCrystal => false,
                ItemID.CellPhone => false,
                ItemID.PDA => false,
                ItemID.MagicMirror => false,
                ItemID.IceMirror => false,
                ItemID.TreasureMap => false,
                _ => ItemID.Sets.SortingPriorityBossSpawns[item.type] >= 0
            };
        }
    }
}