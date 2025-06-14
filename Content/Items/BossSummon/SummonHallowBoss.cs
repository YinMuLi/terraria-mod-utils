using Terraria.ID;

namespace YinMu.Content.Items.BossSummon
{
    internal class SummonHallowBoss : BaseBossSummon
    {
        //public override string Texture => $"Terraria/Images/Item_{ItemID.EmpressButterfly}";

        internal override int NPCType => NPCID.HallowBoss;

        public override void AddRecipes()
        {
            CreateRecipe()
               .AddIngredient(ItemID.EmpressButterfly)
               .AddTile(TileID.WorkBenches)
               .Register();
        }
    }
}