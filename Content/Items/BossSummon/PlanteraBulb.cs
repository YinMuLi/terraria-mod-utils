using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items.BossSummon
{
    /// <summary>
    /// Bulb:电灯泡;(植物)鳞茎;鳞茎状物(如温度计的球部)
    /// </summary>
    internal class PlanteraBulb : ModItem
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[NPCID.Plantera] = true;//多人模式能被召唤
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 11; // 松露虫
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Lime;
        }

        public override bool CanUseItem(Player player)
        {
            //丛林环境
            return player.ZoneJungle && !NPC.AnyNPCs(NPCID.Plantera);
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.Center);
            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
            else
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: NPCID.Plantera);

            return true;
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