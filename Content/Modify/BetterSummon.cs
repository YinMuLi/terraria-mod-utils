using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Content.Modify
{
    /// <summary>
    /// 右键使用召唤武器，一键召唤所有仆从
    /// </summary>
    internal class BetterSummon : GlobalItem
    {
        public override bool? UseItem(Item item, Player player)
        {
            if (item.DamageType == DamageClass.Summon)
            {
                for (int i = 0; i < Main.LocalPlayer.maxMinions - Main.LocalPlayer.slotsMinions; i++)
                {
                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.LocalPlayer.position, Vector2.Zero, item.shoot, 0, 0, player.whoAmI);
                }
                return true;
            }
            return base.UseItem(item, player);
        }
    }
}