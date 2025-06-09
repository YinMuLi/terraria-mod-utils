using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace YinMu.Common.Configs
{
    /// <summary>
    /// 服务器配置，一个人也是服务器
    /// </summary>
    public class ServerConfig : ModConfig
    {
        public static ServerConfig Instance { get; private set; }
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => Instance = this;

        /// <summary>
        /// 是否生成Tom NPC
        /// </summary>
        [DefaultValue(true)]
        public bool SpawnTom;

        ///// <summary>
        ///// 无限制晶塔
        ///// </summary>
        //[DefaultValue(true)]
        //public bool UnrestrainedPylon;

        /// <summary>
        /// 额外翅膀插槽
        /// </summary>
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ExtraWingSlot;

        /// <summary>
        /// Boss召唤物不消耗
        /// </summary>
        [DefaultValue(true)]
        [ReloadRequired]
        public bool BossSpawnNotConsumable;
    }
}