using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YinMu.Content.Buffs
{
    internal class DryadsBlessing : ModBuff
    {
        public override string Texture => $"Terraria/Images/Buff_{BuffID.DryadsWard}";

        public override void SetStaticDefaults()
        {
            //不是debuff
            Main.debuff[Type] = false;
            //debuff时不可被护士去除
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffImmune[BuffID.DryadsWard] = true;
            player.lifeRegen += 6;
            player.statDefense += 8;
            //去除视觉效果：当移动时，脚下出现绿色火花轨迹。
            //player.dryadWard = true;
            if (player.thorns < 1f)
            {
                player.thorns += 0.5f;
            }
        }
    }
}