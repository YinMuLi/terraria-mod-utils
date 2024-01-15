using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;

namespace Branch.Common.Utils
{
    public static partial class ModUtils
    {
        public static void ShowText(string msg, Color color)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(msg), color);
        }

        public static void SnycWorld()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }

        //CombatText.NewText(Player.getRect(), Color.LightYellow, Language.GetTextValue($"{BossLogUI.LangLog}.Records.NewRecord"), true);
    }
}