using System;
using System.ComponentModel;
using System.Threading;
using Terraria.ModLoader.Config;

namespace YinMu.Common.Configs
{
    /// <summary>
    /// 多人模式下，客户端配置
    /// </summary>
    internal class ClientConfig : ModConfig
    {
        public static ClientConfig Instance { get; private set; }
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => Instance = this;

        /// <summary>
        /// 快速堆叠后交换物品栏
        /// </summary>
        [Header("Common")]
        [DefaultValue(true)]
        public bool SwitchInventory;

        [Increment(0.1f)]
        [Range(0.3f, 4f)]
        [DefaultValue(0.3f)]
        public float MinZoom;

        /// <summary>
        /// 切换装备时不交换装饰品
        /// </summary>
        [DefaultValue(true)]
        public bool NoSwapDecoration;

        [Header("Fishing")]

        /// <summary>
        /// 鱼线数量
        /// </summary>
        [Range(1, 200)]
        [DefaultValue(5)]
        public int LuresAmount;

        [DefaultValue(true)]
        public bool QuickFishing;
    }
}