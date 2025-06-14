using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YinMu.Content.Buffs
{
    /// <summary>
    /// 糖分爆发
    /// </summary>
    internal class SugarRush : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //篝火：增加附近玩家 10% 的当前生命再生，并额外增加 0.5 每秒生命回复
            player.buffImmune[87] = true;
            Main.SceneMetrics.HasCampfire = true;
            //心灯：持续为附近的玩家每秒提供 1 生命
            player.buffImmune[89] = true;
            Main.SceneMetrics.HasHeartLantern = true;
            //蜂蜜：+1生命恢复
            player.buffImmune[48] = true;
            player.honey = true;
            //施法桌：+1最大随从
            player.buffImmune[150] = true;
            ++player.maxMinions;
            //利器站：所有近战武器的盔甲穿透提高 12
            player.buffImmune[159] = true;
            player.GetArmorPenetration(DamageClass.Melee) += 12;
            //水晶球：+20 最大魔力 +5 % 魔法伤害 + 2 % 魔法暴击率 −2 % 魔力消耗
            player.buffImmune[29] = true;
            player.GetDamage(DamageClass.Magic) += 0.05f;
            player.GetCritChance(DamageClass.Magic) += 2;
            player.statManaMax2 += 20;
            player.manaCost -= 0.02f;
            //弹药箱(加不加无所谓)
            player.buffImmune[93] = true;
            player.ammoBox = true;
            //蛋糕块：+20% 移动速度和 +20% 挖矿速度
            player.buffImmune[192] = true;
            player.pickSpeed -= 0.2f;
            player.moveSpeed += 0.2f;
            //星星瓶：每秒额外回复1魔力
            player.buffImmune[158] = true;
            player.manaRegenBonus += 1;
            //战争桌：玩家的哨兵上限提高 1
            player.buffImmune[348] = true;
            ++player.maxTurrets;
            //巴斯特雕像：增加 5 点防御
            player.buffImmune[215] = true;
            player.statDefense += 5;
        }
    }
}