using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Global
{
    internal class NPCAssist : GlobalNPC
    {
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !npc.boss) return;
        }
    }
}