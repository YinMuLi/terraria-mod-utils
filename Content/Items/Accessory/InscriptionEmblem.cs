using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace Branch.Content.Items.Accessory
{
    internal class InscriptionEmblem : ModItem
    {
        private static long price = Item.buyPrice(platinum: 1);//1铂金币

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

        public override bool CanUseItem(Player player) => player.CanAfford(price);

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI != Main.myPlayer) return false;
            for (int i = 3; i <= 9; i++)
            {
                Item item = player.armor[i];
                Reforge(player, Item.prefix, item);
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
                    Reforge(player, Item.prefix, item);
                }
            }
            return true;
        }

        private static void Reforge(Player player, int prefixID, Item item)
        {
            if (item.stack > 0 && item.prefix != prefixID && ItemLoader.CanReforge(item))
            {
                player.BuyItem(price);
                item.ResetPrefix();
                item.Prefix(prefixID);
                item.position = player.Center;//显示文字的位置
                ItemLoader.PostReforge(item);
                PopupText.NewText(PopupTextContext.ItemReforge, item, item.stack, noStack: true);
                SoundEngine.PlaySound(SoundID.Item37);
            }
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