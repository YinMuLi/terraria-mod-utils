using System.ComponentModel;
using System.Threading.Tasks.Dataflow;
using Terraria.ModLoader.Config;

namespace Branch.Common.Configs
{
    public class ModConfig : Terraria.ModLoader.Config.ModConfig
    {
        public static ModConfig Instance { get; private set; }
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => Instance = this;

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

        [Header("Fishing")]
        /// <summary>
        /// 自动钓鱼
        /// </summary>
        [DefaultValue(true)]
        public bool AutoFish;

        /// <summary>
        /// 鱼线数量
        /// </summary>
        [Range(0, 50)]
        [Increment(1)]
        [DefaultValue(5)]
        [Slider]
        public int LuresAmount;

        /// <summary>
        /// 拉杆延迟时间
        /// </summary>
        [Range(0f, 1f)]
        [Increment(0.1f)]
        [DefaultValue(0.5f)]
        public float PullWaitTimer;

        /// <summary>
        /// 捕获宝箱
        /// </summary>
        [DefaultValue(true)]
        public bool CatchCrate;

        /// <summary>
        /// 捕获饰品
        /// </summary>
        [DefaultValue(false)]
        public bool CatchAccessories;
    }
}