using Branch.Common.Configs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Branch.Content.Modify
{
    /// <summary>
    /// 鱼竿多线
    /// </summary>
    public class MultipleLure : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            //bobber:钓鱼用的漂浮
            if (projectile.bobber && projectile.owner == Main.myPlayer && source is EntitySource_ItemUse)
            {
                for (int i = 0; i < ModConfig.Instance.LuresAmount; i++)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis("MultipleLure"),
                           projectile.position,
                           projectile.velocity.RotatedBy(0.01 * i),
                           projectile.type,
                           projectile.damage,
                           projectile.knockBack,
                           projectile.owner);
                }
                projectile.active = false;
            }
        }
    }
}