//using Branch.Content.Buffs;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ID;
//using Terraria.Localization;
//using Terraria.ModLoader;
//using Terraria.ObjectData;

//namespace Branch.Content.Titles
//{
//    //TODO:开启关闭增益
//    internal class FinalStationTile : ModTile
//    {
//        public override void SetStaticDefaults()
//        {
//            Main.tileFrameImportant[Type] = true;//不是1x1的贴图
//            Main.tileLighted[Type] = true;//拥有光源
//            Main.tileWaterDeath[Type] = false;
//            Main.tileLavaDeath[Type] = false;
//            Main.tileNoAttach[Type] = true;//其他tile不能依附在此tile上，比如火把
//            TileID.Sets.DisableSmartCursor[Type] = true;//禁用智能光标

//            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
//            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 18 };
//            TileObjectData.addTile(Type);

//            LocalizedText name = CreateMapEntryName();
//            AddMapEntry(new Color(215, 60, 0), name);
//            DustType = 30;//被敲击时的粉尘
//            //一帧动画的高度 74个像素高度
//            AnimationFrameHeight = 74;
//        }

//        public override void AnimateTile(ref int frame, ref int frameCounter)
//        {
//            //不播放第一帧动画
//            if (++frameCounter > 5)
//            {
//                frameCounter = 0;
//                frame++;
//                if (frame > 19)
//                {
//                    frame = 1;
//                }
//            }
//        }

//        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
//        {
//            r = 1f;
//            g = 20 / 255f;
//            b = 147 / 255f;
//        }

//        //不能被爆炸摧毁
//        public override bool CanExplode(int i, int j)
//        {
//            return false;
//        }

//        public override void NearbyEffects(int i, int j, bool closer)
//        {
//            Player player = Main.LocalPlayer;
//            if (player is null)
//                return;
//            if (!player.dead && player.active)
//                player.AddBuff(ModContent.BuffType<CommonStationBuff>(), 20);
//        }
//    }
//}