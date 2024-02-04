using Branch.Content.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
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
        }
    }
}