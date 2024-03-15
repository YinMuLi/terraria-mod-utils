using Terraria;
using Terraria.ID;

namespace Branch.Content.Items.BossSummon
{
    internal class BloodyDoll : BaseBossSummon
    {
        internal override int NPCType => NPCID.WallofFlesh;

        public override bool CanUseItem(Player player)
        {
            //地狱环境环境
            return player.ZoneUnderworldHeight && !NPC.AnyNPCs(NPCID.WallofFlesh);
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.SpawnWOF(player.Center);
            else
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: NPCID.WallofFlesh);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.GuideVoodooDoll).
                AddTile(TileID.DemonAltar).
                Register();
        }
    }
}