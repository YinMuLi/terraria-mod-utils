using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Branch.Common.Configs
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

        /// <summary>
        /// 是否在右侧信息栏显示额外的仆从信息
        /// </summary>
        [DefaultValue(true)]
        public bool ShowMinion;

        [ReloadRequired]
        [Increment(0.1f)]
        [Range(0.3f, 4f)]
        [DefaultValue(0.3f)]
        public float MinZoom;

        [Header("Fishing")]
        /// <summary>
        /// 自动钓鱼
        /// </summary>
        [DefaultValue(true)]
        public bool AutoFish;

        /// <summary>
        /// 鱼线数量
        /// </summary>
        [Range(1, 200)]
        [DefaultValue(5)]
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