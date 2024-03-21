using YinMu.Common.Interface;
using YinMu.Content.Items.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace YinMu.Common.Players
{
    public partial class YinMuPlayer : ModPlayer
    {
        internal string linkWorldID;

        public override void SaveData(TagCompound tag)
        {
            tag["LinkWord"] = linkWorldID;
        }

        public override void LoadData(TagCompound tag)
        {
            tag.TryGet("LinkWord", out linkWorldID);
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath)//硬核人物？？？
            {
                yield return new Item(ModContent.ItemType<StarterBag>());
            }
        }

        public override bool HoverSlot(Item[] inventory, int context, int slot)
        {
            Item item = inventory[slot];
            foreach (var globalItem in from i in GlobalList<GlobalItem>.Globals where i is IItemClickable select i)
            {
                ((IItemClickable)globalItem).HandleHover(inventory, context, slot);
            }
            return base.HoverSlot(inventory, context, slot);
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            DisplayRareCreaturesIndicator();
        }

        /// <summary>
        /// 显示指示稀有生物指针
        /// </summary>
        public void DisplayRareCreaturesIndicator()
        {
            if (Main.gameMenu) return;//暂停界面
            float detectRange = Math.Min(Main.screenWidth, Main.screenHeight) / 2f;//检测范围
            Vector2 playerPos = Player.Center - Main.screenPosition;
            if (Player.accCritterGuide && Player.accCritterGuideNumber >= 0 && Player.accCritterGuideNumber < Main.npc.Length)
            {
                //accCritterGuideNumber：当前记录的NPC在Main.npc里的索引
                //稀有生物体分析仪检测到稀有生物
                NPC npc = Main.npc[Player.accCritterGuideNumber];
                if (npc != null && npc.active)
                {
                    Texture2D tex = TextureAssets.Npc[npc.type].Value;
                    Vector2 direction = npc.Center - Player.Center;//方向
                    //把贴图转为水平方向，加了90度
                    float rotation = direction.ToRotation() + (float)(Math.PI / 2);//弧度
                    direction.Normalize();
                    direction *= 80;
                    //origin:旋转中心点，左上角为参考系中心，图片内部坐标
                    Main.spriteBatch.Draw(ModContent.Request<Texture2D>("YinMu/Content/Projectiles/Indicator").Value,
                        direction + playerPos, null, Color.White, rotation, TextureAssets.Cursors[1].Size() / 2, Vector2.One, SpriteEffects.None, 0);
                }
            }
        }

        /// <summary>
        /// Refund:退款
        /// </summary>
        /// <param name="coinCount"></param>
        internal void Refund(int[] coinCount)
        {
            if (coinCount[0] > 0) Player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.CopperCoin, coinCount[0]);
            if (coinCount[1] > 0) Player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.SilverCoin, coinCount[1]);
            if (coinCount[2] > 0) Player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.GoldCoin, coinCount[2]);
            if (coinCount[3] > 0) Player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.PlatinumCoin, coinCount[3]);
        }
    }
}