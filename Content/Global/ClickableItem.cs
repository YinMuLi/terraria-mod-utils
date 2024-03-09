using Branch.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Global
{
    //TODO:失败
    internal class ClickableItem : GlobalItem, IItemClickable
    {
        public ClickType CanClickable(Item item, int index)
        {
            if (item.type == ItemID.VoidLens)
            {
                return ClickType.Left;
            }
            return ClickType.None;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
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
    }
}