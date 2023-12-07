using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;

namespace Branch.Common.Utils
{
    public static class ModUtils
    {
        /// <summary>
        /// 聊天框显示信息
        /// </summary>
        /// <param name="obj"></param>
        public static void ShowText(string msg)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(msg), Color.White, -1);
        }
    }
}