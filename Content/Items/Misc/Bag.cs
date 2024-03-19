using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items.Misc
{
    /// <summary>
    ///
    /// </summary>
    internal class Bag : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 25;
            Item.height = 34;
            Item.rare = ItemRarityID.Purple;
            Item.noUseGraphic = true;//使用时不显示物品
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.maxStack = 1;
            Item.UseSound = SoundID.Item130;
        }

        public override bool CanRightClick() => true;

        //放置右键点击消耗此物品
        public override bool ConsumeItem(Player player) => false;

        public override void RightClick(Player player)
        {
            Main.NewText("dssd");
        }
    }
}