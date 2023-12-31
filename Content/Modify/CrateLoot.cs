﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Branch.Content.Modify
{
    internal class CrateLoot : GlobalItem
    {
        public override bool InstancePerEntity => false;

        public override void ModifyItemLoot(Item item, ItemLoot loot)
        {
            //这里loot的概率都是1/xxx计算的参数是分母
            switch (item.type)
            {
                case ItemID.GoldenCrate:
                    loot.Add(ItemDropRule.Common(ItemID.Diamond, 5, 1, 2));//(1-2)20% 钻石
                    loot.Add(ItemDropRule.Common(ItemID.Ruby, 5, 1, 2));//(1-2)20% 鲁比
                    loot.Add(ItemDropRule.Common(ItemID.Emerald, 5, 1, 2));//(1-2)20% 翡翠
                    loot.Add(ItemDropRule.Common(ItemID.Sapphire, 5, 1, 2));//(1-2)20% 萨菲
                    break;
            }
        }
    }
}