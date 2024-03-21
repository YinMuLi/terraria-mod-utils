using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace YinMu.Content.Global
{
    internal class NPCAssist : GlobalNPC
    {
        public override bool CanHitNPC(NPC npc, NPC target)
        {
            //Boss期间NPC无敌
            if (target.townNPC && Main.npc.Any(t => t.active && t.boss))
            {
                return false;
            }
            return base.CanHitNPC(npc, target);
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (!projectile.friendly && npc.townNPC && Main.npc.Any(t => t.active && t.boss))
            {
                return false;
            }
            return base.CanBeHitByProjectile(npc, projectile);
        }
    }
}