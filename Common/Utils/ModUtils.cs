using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
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
        /// 给予玩家物品
        /// </summary>
        /// <param name="player">给予玩家对象</param>
        /// <param name="itemID">物品ID</param>
        /// <param name="amount">物品的个数（默认是1）</param>
        public static void GiveItem(Player player, int itemID, int amount = 1)
        {
            Item.NewItem(null, player.Center, itemID, amount);
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

        /// <summary>
        /// 重铸物品前缀
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <param name="prefixID"></param>
        /// <param name="showMessage">显示重铸信息</param>
        /// <param name="playSound">播放重铸声音</param>
        public static void Reforge(Player player, Item item, int prefixID, bool showMessage = false, bool playSound = false)
        {
            if (item.stack == 1 && item.prefix != prefixID && ItemLoader.CanReforge(item))
            {
                item.ResetPrefix();
                item.Prefix(prefixID);
                ItemLoader.PostReforge(item);
                if (showMessage && player != null)
                {
                    item.position = player.Center;//显示文字的位置
                    PopupText.NewText(PopupTextContext.ItemReforge, item, item.stack, noStack: true);
                }
                if (playSound) SoundEngine.PlaySound(SoundID.Item37);
            }
        }

        public static WorldFileData GetWorldFileData(string worldUniqueId)
        {
            if (!Main.WorldList.Any()) Main.LoadWorlds();
            foreach (var worldData in Main.WorldList)
            {
                if (worldData.UniqueId.ToString() == worldUniqueId) return worldData;
            }
            return null;
        }
    }
}