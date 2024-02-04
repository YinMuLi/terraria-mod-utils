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
            //显示内部编号
            //tooltips.Add(new TooltipLine(Mod, "InternalID",
            //    $"{Language.GetTextValue("Mods.Tips.InternalID").FormatWith(item.type)}")
            //{ OverrideColor = Color.Gray });
        }

        //public override bool? UseItem(Item item, Player player)
        //{
        //    if (item.type == ItemID.CelestialSigil && NPC.MoonLordCountdown > 1)
        //    {
        //        NPC.MoonLordCountdown = 1;
        //        NetMessage.SendData(MessageID.MoonlordHorror, number: NPC.MoonLordCountdown);
        //    }
        //    return base.UseItem(item, player);
        //}

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
                ItemID.LifeFruit => false,
                ItemID.ManaCrystal => false,
                ItemID.CellPhone => false,
                ItemID.PDA => false,
                ItemID.MagicMirror => false,
                ItemID.IceMirror => false,
                ItemID.TreasureMap => false,
                ItemID.TruffleWorm => false,//松露虫
                _ => ItemID.Sets.SortingPriorityBossSpawns[item.type] is >= 0 and
                 not 20 and not 21,
            };
        }
    }
}