using Branch.Common.System;
using Branch.Content.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Common.Players
{
    public partial class BranchPlayer : ModPlayer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="inventory">物品所在的数组（背包/储钱罐...）</param>
        /// <param name="context">物品的槽标识</param>
        /// <param name="slot">物品的索引</param>
        /// <returns></returns>
        //public override bool HoverSlot(Item[] inventory, int context, int slot)
        //{
        //    var item = inventory[slot];
        //    if (item.ModItem is IItemMiddleClickable clickable)
        //    {
        //        clickable.HandleHover(inventory, context, slot);
        //    }
        //    return false;
        //}
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath)//硬核人物？？？
            {
                yield return new Item(ModContent.ItemType<StarterBag>());
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            //处理快捷键
            if (Player.DeadOrGhost) return;
            if (KeybindSystem.TeleportDeathPoint.JustPressed && Player.lastDeathPostion != Vector2.Zero)
            {
                bool postImmune = Player.immune;
                int postImmuneTime = Player.immuneTime;
                Player.Teleport(Player.lastDeathPostion, TeleportationStyleID.TeleportationPotion);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.TeleportEntity, number2: Player.whoAmI, number3: Player.lastDeathPostion.X, number4: Player.lastDeathPostion.Y, number5: TeleportationStyleID.TeleportationPotion);
                Player.velocity = Vector2.Zero;
                Player.immune = postImmune;
                Player.immuneTime = postImmuneTime;
                SoundEngine.PlaySound(SoundID.Item6, Player.Center);
            }
        }
    }
}