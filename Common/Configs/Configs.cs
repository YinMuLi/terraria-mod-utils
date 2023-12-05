using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Branch.Common.Configs
{
    public class Configs : ModConfig
    {
        public static Configs Instance { get; private set; }
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
    }
}