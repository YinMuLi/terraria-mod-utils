using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Branch.Common.Configs
{
    public class BranchConfig : ModConfig
    {
        public static BranchConfig Instance { get; private set; }
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => Instance = this;

        /// <summary>
        /// 鱼线数量
        /// </summary>
        [Range(0, 50)]
        [Increment(1)]
        [DefaultValue(5)]
        [Slider]
        public int LuresAmount;

        /// <summary>
        /// 是否生成Tom NPC
        /// </summary>
        [DefaultValue(true)]
        public bool SpawnTom;

        /// <summary>
        /// 无限制晶塔
        /// </summary>
        [DefaultValue(true)]
        public bool UnrestrainedPylon;

        /// <summary>
        /// 额外翅膀插槽
        /// </summary>
        [DefaultValue(true)]
        public bool ExtraWingSlot;
    }
}