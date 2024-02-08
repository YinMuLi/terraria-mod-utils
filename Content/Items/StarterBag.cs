using Branch.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items
{
    internal class StarterBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            ModItem modItem = null;
            //添加魔法存储初始礼包
            string magicStorage = "MagicStorage";

            if (ModLoader.TryGetMod(magicStorage, out Mod _))
            {
                if (ModContent.TryFind(magicStorage, "StorageHeart", out modItem))
                {
                    itemLoot.Add(modItem.Type);
                }
                if (ModContent.TryFind(magicStorage, "CraftingAccess", out modItem))
                {
                    itemLoot.Add(modItem.Type);
                }
                if (ModContent.TryFind(magicStorage, "StorageUnit", out modItem))
                {
                    itemLoot.Add(modItem.Type, 1, 10, 10);
                }
            }
        }
    }
}