using Branch.Common.Interface;
using Branch.Content.Items.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public override void PostUpdateMiscEffects()
        {
            ForceBiomes();
        }

        public override void PostUpdate()
        {
            base.PostUpdate();
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
        /// 召唤仆从
        /// </summary>
        /// <param name="item">召唤武器</param>
        internal void Summon(Item item)
        {
            //召唤仆从
            Player.ItemCheck_Shoot(Player.whoAmI, item, Player.GetWeaponDamage(item));
            //添加仆从对应Buff栏信息
            Player.ItemCheck_ApplyPetBuffs(item);
        }

        /// <summary>
        /// 喷泉强制改变环境
        /// </summary>
        private void ForceBiomes()
        {
            //法狗
            switch (Main.SceneMetrics.ActiveFountainColor)
            {
                case WaterStyleID.Purity: //纯净喷泉
                    Player.ZoneBeach = true;
                    break;

                case WaterStyleID.Corrupt: //腐败
                    Player.ZoneCorrupt = true;
                    break;

                case WaterStyleID.Jungle: //丛林
                    Player.ZoneJungle = true;
                    break;

                case WaterStyleID.Hallow: //神圣
                    if (Main.hardMode)
                        Player.ZoneHallow = true;
                    break;

                case WaterStyleID.Snow: //雪原
                    Player.ZoneSnow = true;
                    break;

                case WaterStyleID.Crimson: //猩红
                    Player.ZoneCrimson = true;
                    break;

                case WaterStyleID.Desert: //沙漠
                case WaterStyleID.UndergroundDesert://地下沙漠
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