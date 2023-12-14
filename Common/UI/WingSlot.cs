using Branch.Common.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Branch.Common.UI
{
    /// <summary>
    /// 添加翅膀饰品插槽
    /// </summary>
    internal class WingSlot : ModAccessorySlot
    {
        public override string Name => "WingSlot";

        public override string FunctionalTexture => $"Terraria/Images/Item_{ItemID.FishronWings}";

        public override bool IsLoadingEnabled(Mod mod)
        {
            return BranchConfig.Instance.ExtraWingSlot;
        }

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return checkItem.wingSlot > 0;
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            return item.wingSlot > 0;
        }

        public override void OnMouseHover(AccessorySlotType context)
        {
            Main.hoverItemName = context switch
            {
                AccessorySlotType.FunctionalSlot => Language.GetTextValue("Mods.UI.WingSlot.Function"),
                AccessorySlotType.VanitySlot => Language.GetTextValue("Mods.UI.WingSlot.Vanity"),
                AccessorySlotType.DyeSlot => Language.GetTextValue("Mods.UI.WingSlot.Dye"),
            };
            base.OnMouseHover(context);
        }
    }
}