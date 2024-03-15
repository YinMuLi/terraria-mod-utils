using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Branch.Content.Items.BossSummon
{
    internal class SummonDukeFishron : BaseBossSummon
    {
        internal override int NPCType => NPCID.DukeFishron;

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player) && player.ZoneBeach;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)spawnPosition.X, (int)spawnPosition.Y, NPCID.DukeFishron);
            }
            else
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: NPCType);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TruffleWorm)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}