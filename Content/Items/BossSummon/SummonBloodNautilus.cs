using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace YinMu.Content.Items.BossSummon
{
    internal class SummonBloodNautilus : ModItem
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[NPCID.BloodNautilus] = true;//多人模式能被召唤
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
            //困难模式血月
            return Main.hardMode && Main.bloodMoon && !NPC.AnyNPCs(NPCID.BloodNautilus);
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.Center);
            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BloodNautilus);
            else
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: NPCID.BloodNautilus);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.BloodMoonStarter).
                AddTile(TileID.DemonAltar).
                Register();
        }
    }
}