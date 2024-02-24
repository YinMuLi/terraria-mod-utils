using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items.Misc
{
    /// <summary>
    /// 天堂制造：可以调节时间
    /// </summary>
    public class MadeInHeaven : ModItem
    {
        /// <summary>
        /// 正午时刻
        /// </summary>
        private const int NOON = 27000;

        /// <summary>
        /// 午夜时刻
        /// </summary>
        private const int MID_NEIGHT = 16200;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("Branch:AnyDemoniteBar", 10)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override void SetDefaults()
        {
            Item.width = 32;//贴图的宽度
            Item.height = 32;//贴图的高度
            Item.value = Item.buyPrice(0, 1, 0, 0);//物品的价值
            Item.rare = ItemRarityID.Red;//物品的稀有度
            Item.useAnimation = 18;//每次使用时动画播放时间
            Item.useTime = 18;//使用一次所需时间
            Item.UseSound = SoundID.Item4;//物品使用时声音
            Item.useStyle = ItemUseStyleID.HoldUp;//物品的使用方式
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            //不是BOSS战时可以使用
            return !Main.npc.Any(t => t.active && t.boss);
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.lastDeathPostion != Vector2.Zero)
            {
                ModUtils.ModTeleportion(player, player.lastDeathPostion);
            }
            else
            {
                /**
                * Main.dayTime:白天为真，晚上为假
                * Main.time:白天介于0-54000 晚上介于0-32400
                */
                if (Main.dayTime && Main.time < NOON)
                {
                    //清晨到中午
                    Main.SkipToTime(NOON, true);
                    ModUtils.ShowText("正午", Color.Yellow);
                }
                else if (Main.dayTime)
                {
                    //中午到黄昏
                    Main.SkipToTime(0, false);
                    ModUtils.ShowText("黄昏", Color.Yellow);
                }
                else if (!Main.dayTime && Main.time < MID_NEIGHT)
                {
                    //黄昏到半夜
                    Main.SkipToTime(MID_NEIGHT, false);
                    ModUtils.ShowText("午夜", Color.Yellow);
                }
                else if (!Main.dayTime)
                {
                    //半夜到清晨
                    Main.SkipToTime(0, true);
                    ModUtils.ShowText("黎明", Color.Yellow);
                }
            }

            return true;
        }
    }
}