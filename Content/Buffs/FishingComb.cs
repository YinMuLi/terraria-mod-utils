using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Buffs
{
    /// <summary>
    /// 钓鱼药水包增益
    /// </summary>
    public class FishingComb : ModBuff
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
            //先清除单独的药水效果，然后添加药水效果，防止药水效果叠加

            //钓鱼药水
            player.buffImmune[121] = true;
            player.fishingSkill += 15;
            //声呐药水
            player.buffImmune[122] = true;
            player.sonarPotion = true;
            //宝匣药水
            player.buffImmune[123] = true;
            player.cratePotion = true;
        }
    }
}