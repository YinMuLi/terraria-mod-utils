using Branch.Common.Extensions;
using Branch.Common.Interface;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Content.Global
{
    //TODO:失败
    internal class ClickableItem : GlobalItem, IItemClickable
    {
        public ClickType CanClickable(Item item, int index)
        {
            if (item.DamageType == DamageClass.Summon)
            {
                return ClickType.Middle;
            }
            return ClickType.None;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        public void OnLeftClicked(Item item, int index)
        {
            for (int i = 0; i < 40; i++)
            {
                if (Main.LocalPlayer.bank4.item[i].stack == 0)
                {
                    Main.LocalPlayer.bank4.item[i] = Main.mouseItem.Clone();
                    Main.mouseItem.TurnToAir();
                }
            }
        }

        public void OnMiddleClicked(Item item, int index)
        {
            int count = Main.LocalPlayer.maxMinions - (int)Main.LocalPlayer.slotsMinions;
            for (int i = 0; i < count; i++)
            {
                Main.LocalPlayer.ModPlayer().Summon(item);
            }
        }
    }
}