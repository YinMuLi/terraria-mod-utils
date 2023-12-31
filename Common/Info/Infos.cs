using Humanizer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Branch.Common.Info
{
    /// <summary>
    /// Minion:仆从 Sentry:哨兵 Turrets:炮塔
    /// </summary>
    internal class MinionInfo : InfoDisplay
    {
        public override bool Active() => true;

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            int maxMinion = Main.LocalPlayer.maxMinions;
            //有的召唤物单位不是1,保留两位小数
            float minionCount = (float)Math.Round(Main.LocalPlayer.slotsMinions, 2);

            int sentryCount = 0;//哨兵
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                var proj = Main.projectile[i];
                if (proj.active && proj.sentry && proj.owner == Main.myPlayer)
                {
                    sentryCount++;
                }
            }
            return Language.GetTextValue("Mods.UI.InfoDisplay.Minion")
                .FormatWith(minionCount, maxMinion, sentryCount, Main.LocalPlayer.maxTurrets);
        }
    }
}