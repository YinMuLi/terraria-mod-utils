using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Common.Players
{
    internal class CustomPlayer : ModPlayer

    {
        /// <summary>
        /// 疾病之刃
        /// </summary>
        public bool sickBlade = false;

        public override void ResetEffects()
        {
            sickBlade = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (sickBlade)
            {
                //60：一秒 300：5秒

                //涂油
                target.AddBuff(BuffID.Oiled, 300);
                //狱炎
                target.AddBuff(BuffID.OnFire3, 300);
                //着火了
                target.AddBuff(BuffID.OnFire, 300);
                //中毒
                target.AddBuff(BuffID.Poisoned, 300);
                //酸性毒液
                target.AddBuff(BuffID.Venom, 300);
                //诅咒狱火
                target.AddBuff(BuffID.CursedInferno, 300);
                //灵液，减甲
                target.AddBuff(BuffID.Ichor, 300);
                //迈达斯,死后掉落跟多的钱
                target.AddBuff(BuffID.Midas, 300);
                //暗影焰
                target.AddBuff(BuffID.ShadowFlame, 300);
                //冻伤
                target.AddBuff(BuffID.Frostburn, 300);
                target.AddBuff(BuffID.Frostburn2, 300);
            }
        }
    }
}