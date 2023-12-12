using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Common.UI
{
    /// <summary>
    /// 添加鞋子饰品插槽
    /// </summary>
    internal class WingSlot : ModAccessorySlot
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return checkItem.shoeSlot > 0;
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            return base.ModifyDefaultSwapSlot(item, accSlotToSwapTo);
        }
    }
}