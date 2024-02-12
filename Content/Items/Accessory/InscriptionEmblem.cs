using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace Branch.Content.Items.Accessory
{
    internal class InscriptionEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.accessory = true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI != Main.myPlayer) return false;
            //TODO：联机同步？
            if (!player.BuyItem(Item.buyPrice(platinum: 1))) return false;
            SoundEngine.PlaySound(SoundID.Item37, player.position);
            for (int i = 3; i <= 9; i++)
            {
                Item item = player.armor[i];
                if (item != null && item.accessory && item.prefix != Item.prefix)
                {
                    item.Prefix(Item.prefix);
                }
            }
            //模组饰品栏
            var accessoryPlayer = player.GetModPlayer<ModAccessorySlotPlayer>();
            var loader = LoaderManager.Get<AccessorySlotLoader>();
            for (int i = 0; i < accessoryPlayer.SlotCount; i++)
            {
                if (loader.ModdedIsItemSlotUnlockedAndUsable(i, player))
                {
                    var slot = loader.Get(i, player);
                    Item item = slot.FunctionalItem;
                    if (item != null && item.accessory && item.prefix != Item.prefix)
                    {
                        item.Prefix(Item.prefix);
                    }
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar)
                .AddIngredient(ItemID.SoulofNight)
                .AddIngredient(ItemID.SoulofLight)
                .Register();
        }
    }
}