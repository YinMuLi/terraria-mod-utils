using Branch.Common.Interface;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Common.Players
{
    internal class ClickablePlayer : ModPlayer
    {
        public override bool HoverSlot(Item[] inventory, int context, int slot)
        {
            var item = inventory[slot];
            if (item.ModItem is IItemRightClickable clickable)
            {
                clickable.HandleHover(inventory, context, slot);
                return true;
            }
            return false;
        }
    }
}