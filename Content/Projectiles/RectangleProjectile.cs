using Branch.Content.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Color = Microsoft.Xna.Framework.Color;

namespace Branch.Content.Projectiles
{
    /// <summary>
    /// 懒人mod
    /// </summary>
    internal class RectangleProjectile : ModProjectile

    {
        private Vector2 oldMouse;

        public override void SetDefaults()
        {
            base.Projectile.width = 16;
            base.Projectile.height = 16;
            base.Projectile.ignoreWater = true;
            base.Projectile.tileCollide = false;
            base.Projectile.timeLeft = 10;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            lightColor.A = 200;
            return lightColor;
        }

        public override void OnSpawn(IEntitySource source)
        {
            oldMouse = Main.MouseWorld;
            base.OnSpawn(source);
        }

        public override void AI()
        {
            Player player = Main.player[this.Projectile.owner];

            Vector2 mouse = Main.MouseWorld;
            Vector2 delta = mouse - oldMouse;
            this.Projectile.position += delta;
            oldMouse = mouse;
            Projectile projectile = this.Projectile;
            projectile.timeLeft++;
            if (player.HeldItem.type != ModContent.ItemType<PrisionBuilder>())
            {
                this.Projectile.Kill();
            }
            this.Projectile.hide = this.Projectile.owner != Main.myPlayer;
        }
    }
}