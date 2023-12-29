using Branch.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Content.Buffs
{
    /// <summary>
    /// 疾病附魔
    /// </summary>
    internal class SickBlade : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.meleeBuff[Type] = true;// 设为true后将会与原版武器灌注冲突（只会存在一种武器灌注）
            Main.persistentBuff[Type] = true;// 死亡后不会清除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DamagePlayer>().sickBlade = true;
        }
    }
}