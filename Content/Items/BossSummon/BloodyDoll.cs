using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items.BossSummon
{
    internal class BloodyDoll : ModItem
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[NPCID.WallofFlesh] = true;//多人模式能被召唤
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 11; // 松露虫
        }

        public override void SetDefaults()
        {
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Lime;
        }

        public override bool CanUseItem(Player player)
        {
            //地狱环境环境
            return player.ZoneUnderworldHeight && !NPC.AnyNPCs(NPCID.WallofFlesh);
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.Center);
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