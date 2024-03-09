using Branch.Common.Configs;
using Humanizer;
using Microsoft.Xna.Framework;
using System.Linq;
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
        public override LocalizedText DisplayName => Language.GetText("BuffName.Summoning");//召唤

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            //int maxMinion = Main.LocalPlayer.maxMinions;
            //有的召唤物单位不是1,保留两位小数
            //float minionCount = (float)Math.Round(Main.LocalPlayer.slotsMinions, 2);
            //int sentryCount = Main.projectile.Count(proj => proj.active && proj.owner == Main.LocalPlayer.whoAmI && proj.sentry);
            return Language.GetTextValue("Mods.UI.InfoDisplay.Minion")
                .FormatWith(Main.LocalPlayer.slotsMinions, Main.LocalPlayer.maxMinions,
                Main.projectile.Count(proj => proj.active && proj.owner == Main.LocalPlayer.whoAmI && proj.sentry)
                , Main.LocalPlayer.maxTurrets);
        }
    }
}