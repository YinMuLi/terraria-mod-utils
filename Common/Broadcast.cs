using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;

namespace Branch.Common
{
    public class Broadcast
    {
        public static void Print(object obj)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(obj.ToString()), Color.White, -1);
        }
    }
}