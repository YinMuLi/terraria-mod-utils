using Terraria;
using Terraria.ModLoader;

namespace Branch.Content.Buffs
{
    /// <summary>
    /// 超级debuff
    /// </summary>
    internal class SuperDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.meleeBuff[Type] = true;// 设为true后将会与原版武器灌注冲突（只会存在一种武器灌注）
            Main.persistentBuff[Type] = true;// 死亡后不会清除buff
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            //中毒
            npc.poisoned = true;
            //着火了
            npc.onFire = true;
            //困惑
            npc.confused = true;
            //诅咒狱火
        }
    }
}