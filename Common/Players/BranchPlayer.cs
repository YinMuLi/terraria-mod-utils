using Branch.Common.Configs;
using Branch.Content.Items.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Common.Players
{
    public partial class BranchPlayer : ModPlayer
    {
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath)//硬核人物？？？
            {
                yield return new Item(ModContent.ItemType<StarterBag>());
            }
        }

        public override bool HoverSlot(Item[] inventory, int context, int slot)
        {
            return base.HoverSlot(inventory, context, slot);
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            DisplayRareCreaturesIndicator();
        }

        public override void PostUpdateMiscEffects()
        {
            ForceBiomes();
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
                    Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Branch/Content/Projectiles/Indicator").Value,
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

        /// <summary>
        /// 喷泉强制改变环境
        /// </summary>
        private void ForceBiomes()
        {
            //法狗
            switch (Main.SceneMetrics.ActiveFountainColor)
            {
                case 0: //纯净喷泉
                    Player.ZoneBeach = true;
                    break;

                case 2: //腐败
                    Player.ZoneCorrupt = true;
                    break;

                case 3: //丛林
                    Player.ZoneJungle = true;
                    break;

                case 4: //神圣
                    if (Main.hardMode)
                        Player.ZoneHallow = true;
                    break;

                case 5: //雪原
                    Player.ZoneSnow = true;
                    break;

                case 6: //绿洲
                    goto case 12;

                case 9: //猩红
                    Player.ZoneCrimson = true;
                    break;

                case 12: //desert fountain
                    Player.ZoneDesert = true;
                    if (Player.Center.Y > 3200f)
                        Player.ZoneUndergroundDesert = true;
                    break;

                default:
                    break;
            }
        }
    }
}