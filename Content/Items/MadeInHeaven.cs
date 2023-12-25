﻿using Branch.Common.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    /// <summary>
    /// 天堂制造：可以调节时间
    /// </summary>
    public class MadeInHeaven : ModItem
    {
        /// <summary>
        /// 中午时刻
        /// </summary>
        private const int HALF_NOON = 27000;

        /// <summary>
        /// 半夜时刻
        /// </summary>
        private const int HALF_NEIGHT = 16200;

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sundial, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override void SetDefaults()
        {
            Item.width = 32;//贴图的宽度
            Item.height = 32;//贴图的高度
            Item.value = Item.buyPrice(0, 1, 0, 0);//物品的价值
            Item.rare = ItemRarityID.Red;//物品的稀有度
            Item.useAnimation = 9;//每次使用时动画播放时间
            Item.useTime = 9;//使用一次所需时间
            Item.UseSound = SoundID.Item100;//物品使用时声音
            Item.useStyle = ItemUseStyleID.HoldUp;//物品的使用方式
            Item.autoReuse = false;
            Item.mana = 20;//每次使用消耗的法力值
        }

        public override bool? UseItem(Player player)
        {
            if (player.dangerSense) return false;
            /**
             * Main.dayTime:白天为真，晚上为假
             * Main.time:白天介于0-54000 晚上介于0-32400
             */
            if (Main.dayTime && Main.time < HALF_NOON)
            {
                //清晨到中午
                Main.SkipToTime(HALF_NOON, true);
                ModUtils.ShowText("正午", Color.Yellow);
            }
            else if (Main.dayTime)
            {
                //中午到黄昏
                Main.SkipToTime(0, false);
                ModUtils.ShowText("黄昏", Color.Yellow);
            }
            else if (!Main.dayTime && Main.time < HALF_NEIGHT)
            {
                //黄昏到半夜
                Main.SkipToTime(HALF_NEIGHT, false);
                ModUtils.ShowText("半夜", Color.Yellow);
            }
            else if (!Main.dayTime)
            {
                //半夜到清晨
                Main.SkipToTime(0, true);
                ModUtils.ShowText("清晨", Color.Yellow);
            }

            return true;
        }
    }
}