using System.Collections.Generic;
using Terraria;
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
            List<Item> list = new();
            ModItem modItem = null;
            //添加魔法存储初始礼包
            string magicStorage = "MagicStorage";

            if (ModLoader.TryGetMod(magicStorage, out Mod _))
            {
                if (ModContent.TryFind(magicStorage, "StorageHeart", out modItem))
                {
                    list.Add(new Item(modItem.Type));
                }
                if (ModContent.TryFind(magicStorage, "CraftingAccess", out modItem))
                {
                    list.Add(new Item(modItem.Type));
                }
                if (ModContent.TryFind(magicStorage, "StorageUnit", out modItem))
                {
                    list.Add(new Item(modItem.Type, 10));
                }
            }
            //更好的生活体验
            string improveGame = "ImproveGame";
            if (ModLoader.TryGetMod(improveGame, out Mod _))
            {
                if (ModContent.TryFind(improveGame, "CreateWand", out modItem))
                {
                    list.Add(new Item(modItem.Type));
                }
                if (ModContent.TryFind(improveGame, "MagickWand", out modItem))
                {
                    list.Add(new Item(modItem.Type));
                }
                if (ModContent.TryFind(improveGame, "SpaceWand", out modItem))
                {
                    list.Add(new Item(modItem.Type));
                }
                if (ModContent.TryFind(improveGame, "WallPlace", out modItem))
                {
                    list.Add(new Item(modItem.Type));
                }
            }
            return list;
        }
    }
}