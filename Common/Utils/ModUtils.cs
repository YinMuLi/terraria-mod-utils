using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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

        /// <summary>
        /// 传送玩家到指定地点
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pos">传送的地点</param>
        /// <param name="style">传送的类型（默认随机传送药水）</param>
        public static void ModTeleportion(Player player, Vector2 pos, int style = TeleportationStyleID.TeleportationPotion)
        {
            bool postImmune = player.immune;
            int postImmuneTime = player.immuneTime;
            player.Teleport(pos, style);
            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendData(MessageID.TeleportEntity, number2: player.whoAmI, number3: pos.X, number4: pos.Y, number5: style);
            player.velocity = Vector2.Zero;
            player.immune = postImmune;
            player.immuneTime = postImmuneTime;
            SoundEngine.PlaySound(SoundID.Item6, player.Center);
        }
    }
}