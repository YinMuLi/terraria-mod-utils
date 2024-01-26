using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Items.BossSummon
{
    internal class ButterflyToken : ModItem
    {
        public override string Texture => $"Terraria/Images/Item_{ItemID.EmpressButterfly}";

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[NPCID.HallowBoss] = true;//多人模式能被召唤
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 11; // 松露虫
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Lime;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCID.HallowBoss);
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.Center);
            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.HallowBoss);
            else
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: NPCID.HallowBoss);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
               .AddIngredient(ItemID.EmpressButterfly)
               .AddTile(TileID.WorkBenches)
               .Register();
        }
    }
}