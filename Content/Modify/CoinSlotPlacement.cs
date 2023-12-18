using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Content.Modify
{
    /// <summary>
    /// 修改一些物品能放置在钱币槽
    /// </summary>
    internal class CoinSlotPlacement : ILoadable
    {
        public void Load(Mod mod)
        {
            On_ItemSlot.PickItemMovementAction += OnPlaceCoinSlot;
        }

        public void Unload()
        {
            On_ItemSlot.PickItemMovementAction -= OnPlaceCoinSlot;
        }

        private int OnPlaceCoinSlot(On_ItemSlot.orig_PickItemMovementAction orig, Item[] inv, int context, int slot, Item checkItem)
        {
            if (context == 1 && checkItem.type == ItemID.PiggyBank)
            {
                //猪猪存钱罐能放置在钱币槽
                return 0;
            }
            return orig(inv, context, slot, checkItem);
        }
    }
}