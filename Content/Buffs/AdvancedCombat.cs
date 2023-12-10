using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Content.Buffs
{
    /// <summary>
    /// 高级战斗药水Buff
    /// </summary>
    internal class AdvancedCombat : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //怒气：增加10%伤害
            player.buffImmune[117] = true;
            player.GetDamage(DamageClass.Generic) += 0.1f;
            //暴怒-增加10%暴击率
            player.buffImmune[115] = true;
            player.GetCritChance(DamageClass.Melee) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
            //箭术
            player.buffImmune[16] = true;
            player.archery = true;
            //魔能-魔法伤害提高20%
            player.buffImmune[7] = true;
            player.GetDamage(DamageClass.Magic) += 0.2f;
            //召唤药水
            player.buffImmune[110] = true;
            ++player.maxMinions;
        }
    }
}