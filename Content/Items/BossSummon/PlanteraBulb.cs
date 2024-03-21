using Terraria;
using Terraria.ID;

namespace YinMu.Content.Items.BossSummon
{
    /// <summary>
    /// Bulb:电灯泡;(植物)鳞茎;鳞茎状物(如温度计的球部)
    /// </summary>
    internal class PlanteraBulb : BaseBossSummon
    {
        internal override int NPCType => NPCID.Plantera;

        public override bool CanUseItem(Player player)
        {
            //丛林环境
            return player.ZoneJungle && base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.JungleSpores, 15).
                AddIngredient(ItemID.SoulofLight, 10).
                AddIngredient(ItemID.SoulofNight, 10).
                AddTile(TileID.DemonAltar).
                Register();
        }
    }
}