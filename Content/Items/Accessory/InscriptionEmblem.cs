using Microsoft.Xna.Framework;
using Terraria;
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

        public override bool AltFunctionUse(Player player) => true;

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.lastDeathPostion != Vector2.Zero)
            {
                ModUtils.ModTeleportion(player, player.lastDeathPostion);
                return true;
            }
            if (player.whoAmI != Main.myPlayer) return false;
            for (int i = 3; i <= 9; i++)
            {
                ModUtils.Reforge(player, player.armor[i], Item.prefix, true, true);
            }
            //模组饰品栏
            var accessoryPlayer = player.GetModPlayer<ModAccessorySlotPlayer>();
            var loader = LoaderManager.Get<AccessorySlotLoader>();
            for (int i = 0; i < accessoryPlayer.SlotCount; i++)
            {
                if (loader.ModdedIsItemSlotUnlockedAndUsable(i, player))
                {
                    var slot = loader.Get(i, player);
                    ModUtils.Reforge(player, slot.FunctionalItem, Item.prefix, true, true);
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar)
                .Register();
        }
    }
}