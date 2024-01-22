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
            Item.width = 24;
            Item.height = 24;
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
            //更好的生活体验
            string improveGame = "ImproveGame";
            if (ModLoader.TryGetMod(improveGame, out Mod _))
            {
                if (ModContent.TryFind(improveGame, "CreateWand", out modItem))
                {
                    itemLoot.Add(modItem.Type);
                }
                if (ModContent.TryFind(improveGame, "MagickWand", out modItem))
                {
                    itemLoot.Add(modItem.Type);
                }
                if (ModContent.TryFind(improveGame, "SpaceWand", out modItem))
                {
                    itemLoot.Add(modItem.Type);
                }
                if (ModContent.TryFind(improveGame, "WallPlace", out modItem))
                {
                    itemLoot.Add(modItem.Type);
                }
            }
            itemLoot.Add(ItemID.ManaCrystal);
        }
    }
}