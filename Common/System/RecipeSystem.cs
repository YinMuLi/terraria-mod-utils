using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Common.System
{
    internal class RecipeSystem : ModSystem
    {
        public override void AddRecipeGroups()
        {
            string any = Language.GetTextValue("LegacyMisc.37");
            //任何邪恶锭
            int[] items = new int[] { ItemID.DemoniteBar, ItemID.CrimtaneBar };
            RecipeGroup group = new RecipeGroup(() => $"{any} {Lang.GetItemName(ItemID.DemoniteBar)}", items);
            RecipeGroup.RegisterGroup("Branch:AnyDemoniteBar", group);
        }

        public override void AddRecipes()
        {
            #region 猩红和腐败之间的物品能够相互转换

            AddConvertRecipe(ItemID.Ebonkoi, ItemID.Hemopiranha);// 黑檀锦鲤-血腥食人鱼
            AddConvertRecipe(ItemID.DemoniteOre, ItemID.CrimtaneOre);// 魔矿-猩红矿
            AddConvertRecipe(ItemID.TissueSample, ItemID.ShadowScale);//组织样本-暗影鳞片
            AddConvertRecipe(ItemID.Vertebrae, ItemID.RottenChunk);//腐肉-椎骨
            AddConvertRecipe(ItemID.CursedFlame, ItemID.Ichor);//诅咒焰-灵液
            AddConvertRecipe(ItemID.WormFood, ItemID.BloodySpine);//蠕虫小吃-血腥脊椎

            #endregion 猩红和腐败之间的物品能够相互转换

            //墓碑
            Recipe.Create(ItemID.Gravestone)
                .AddIngredient(ItemID.StoneBlock, 10)
                .Register();
            //粉凝胶
            Recipe.Create(ItemID.PinkGel)
                .AddIngredient(ItemID.Gel, 10)
                .Register();
        }

        /// <summary>
        /// 添加不同世界之间一比一之间的转换
        /// </summary>
        private static void AddConvertRecipe(int itemID, int convertItemID)
        {
            Recipe.Create(itemID)
                .AddIngredient(convertItemID)
                .AddTile(TileID.DemonAltar)//邪恶祭坛
                .DisableDecraft()//禁止微光
                .Register();

            Recipe.Create(convertItemID)
                .AddIngredient(itemID)
                .AddTile(TileID.DemonAltar)
                .DisableDecraft()
                .Register();
        }
    }
}