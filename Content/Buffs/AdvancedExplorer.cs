using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Content.Buffs
{
    /// <summary>
    /// 高级探险者buff增益
    /// </summary>
    internal class AdvancedExplorer : ModBuff
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
            //洞穴探险药水
            player.buffImmune[9] = true;
            player.findTreasure = true;
            //夜猫子药水
            player.buffImmune[12] = true;
            player.nightVision = true;
            //挖矿药水
            player.buffImmune[104] = true;
            player.pickSpeed -= 0.25f;
            //狩猎药水
            player.buffImmune[17] = true;
            player.detectCreature = true;
            //危险感知药水
            player.buffImmune[111] = true;
            player.dangerSense = true;
            //鱼鳃药水
            player.buffImmune[4] = true;
            player.gills = true;
            //水上漂药水
            player.buffImmune[15] = true;
            player.waterWalk = true;
            //脚蹼
            player.buffImmune[109] = true;
            player.ignoreWater = true;
            player.accFlipper = true;
            //黑曜石皮肤
            player.buffImmune[BuffID.ObsidianSkin] = true;
            player.fireWalk = true;
            player.lavaImmune = true;
        }
    }
}