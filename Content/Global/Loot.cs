using Branch.Common.Extensions;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Global
{
    internal class Loot : GlobalItem
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

                case ItemID.PlanteraBossBag:

                    //世纪之花宝藏袋添加生命果
                    //条件是开袋时判断一次,但是数量是一开设置好的，无法动态修改。
                    loot.AddIf((info) => Player.LifeFruitMax > Main.LocalPlayer.ConsumedLifeFruit
                    , ItemID.LifeFruit, minQuantity: 10, maxQuantity: 15);
                    break;
            }
        }
    }
}