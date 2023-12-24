using Branch.Common.Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Branch.Common.Players
{
    internal class InfoPlayers : ModPlayer
    {
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
            if (Main.gameMenu) return;//暂停界面
            float detectRange = Math.Min(Main.screenWidth, Main.screenHeight) / 2f;//检测范围
            Vector2 playerPos = Player.Center - Main.screenPosition;
            if (Player.accCritterGuide && Player.accCritterGuideNumber >= 0 && Player.accCritterGuideNumber < Main.npc.Length)
            {
                //accCritterGuideNumber：当前记录的NPC在Main.npc里的索引
                //稀有生物体分析仪检测到稀有生物
                NPC npc = Main.npc[Player.accCritterGuideNumber];
                if (npc != null && npc.active)
                {
                    Texture2D tex = TextureAssets.Npc[npc.type].Value;
                    Vector2 direction = npc.Center - Player.Center;//方向
                    float radian = direction.ToRotation();//弧度
                    direction.Normalize();
                    direction *= 20 * ClientConfig.Instance.CursorDistance;
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.DeathText.Value, "->", direction + playerPos, Color.White, radian, FontAssets.DeathText.Value.MeasureString("->") / 2f, Vector2.One);
                }
            }
        }
    }
}