using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;

namespace Branch.Common.Utils
{
    public static class ModUtils
    {
        /// <summary>
        /// 聊天框显示信息
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowText(string msg)
        {
            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(msg), Color.White, -1);
            Main.NewText(msg);
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