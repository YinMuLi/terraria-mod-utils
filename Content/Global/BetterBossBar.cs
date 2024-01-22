using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace Branch.Content.Global
{
    internal class BetterBossBar : GlobalBossBar
    {
        public override void PostDraw(SpriteBatch spriteBatch, NPC npc, BossBarDrawParams drawParams)
        {
            string percentText = $"{npc.FullName} : ({(npc.life / (float)npc.lifeMax * 100f).ToString("F2")}%)";
            var font = FontAssets.MouseText.Value;
            Vector2 size = font.MeasureString(percentText);
            spriteBatch.DrawString(font, percentText, drawParams.BarCenter - size / 2 + new Vector2(0, -30), Color.White);
        }
    }
}