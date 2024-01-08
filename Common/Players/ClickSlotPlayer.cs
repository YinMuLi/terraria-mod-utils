using Branch.Common.Interface;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Common.Players
{
    public class ClickPlayer : ModPlayer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="inventory">物品所在的数组（背包/储钱罐...）</param>
        /// <param name="context">物品的槽标识</param>
        /// <param name="slot">物品的索引</param>
        /// <returns></returns>
        public override bool HoverSlot(Item[] inventory, int context, int slot)
        {
            var item = inventory[slot];
            if (item.ModItem is IItemMiddleClickable clickable)
            {
                clickable.HandleHover(inventory, context, slot);
            }
            return false;
        }
    }
}