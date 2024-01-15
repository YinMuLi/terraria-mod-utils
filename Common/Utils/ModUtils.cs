using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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
        /// <summary>
        /// 给予玩家其他mod中的物品
        /// </summary>
        /// <param name="player">需要给予的玩家对象</param>
        /// <param name="modName"></param>
        /// <param name="itemName"></param>
        /// <param name="amount"></param>
        public static void GiveModItem(Player player, string modName, string itemName, int amount = 1)
        {
            if (ModContent.TryFind(modName, itemName, out ModItem modItem))
                Item.NewItem(null, player.Center, modItem.Type, amount);
        }
    }
}