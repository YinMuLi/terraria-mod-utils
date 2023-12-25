using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;

namespace Branch.Common.Utils
{
    public static class ModUtils
    {
        public static void ShowText(string msg)
        {
            Main.NewText(msg);
        }

        public static void ShowText(string msg, Color color)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(msg), color);
        }

        /// <summary>
        /// 屏幕上显示信息
        /// </summary>
        /// <param name="msg"></param>
        public static void DrawString(string msg)
        {
            if (msg == string.Empty) return;
        }
    }
}