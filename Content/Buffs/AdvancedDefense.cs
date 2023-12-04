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
    }
}