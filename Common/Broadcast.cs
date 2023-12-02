using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;

namespace Branch.Common
{
    public class Broadcast
    {
        public static void Print(string message)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), Color.White, -1);
        }
    }
}