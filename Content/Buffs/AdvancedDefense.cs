using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Buffs
{
    /// <summary>
    /// 高级防御增益buff
    /// </summary>
    internal class AdvancedDefense : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //不是debuff
            Main.debuff[Type] = false;
            //debuff时不可被护士去除
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //再生
            player.buffImmune[BuffID.Regeneration] = true;
            player.lifeRegen += 2;
            //生命力:每有100点生命值增加20点最大上限
            player.buffImmune[BuffID.Lifeforce] = true;
            player.lifeForce = true;
            player.statLifeMax2 += (player.statLifeMax / 100) * 20;
            //铁皮
            player.buffImmune[BuffID.Ironskin] = true;
            player.statDefense += 8;
            //耐力
            player.buffImmune[BuffID.Endurance] = true;
            player.endurance += 0.1f;
            //荆棘
            player.buffImmune[BuffID.Thorns] = true;
            if ((double)player.thorns < 1.0) player.thorns = 0.34f;
            //保暖药水
            player.buffImmune[BuffID.Warmth] = true;
            player.resistCold = true;
        }
    }
}