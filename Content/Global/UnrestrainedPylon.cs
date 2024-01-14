using Branch.Common.Configs;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Branch.Content.Global
{
    /// <summary>
    /// 无限制的晶塔
    /// </summary>
    internal class UnrestrainedPylon : GlobalPylon
    {
        public static ServerConfig configs => ServerConfig.Instance;

        //不限制放置个数
        public override bool? PreCanPlacePylon(int x, int y, int tileType, TeleportPylonType pylonType)
        {
            if (configs.UnrestrainedPylon)
            {
                return true;
            }
            return base.PreCanPlacePylon(x, y, tileType, pylonType);
        }

        //传送时NPC个数
        public override bool? ValidTeleportCheck_PreNPCCount(TeleportPylonInfo pylonInfo, ref int defaultNecessaryNPCCount)
        {
            if (configs.UnrestrainedPylon)
            {
                return true;
            }
            return base.ValidTeleportCheck_PreNPCCount(pylonInfo, ref defaultNecessaryNPCCount);
        }

        //战斗时是否可以传送
        public override bool? ValidTeleportCheck_PreAnyDanger(TeleportPylonInfo pylonInfo)
        {
            if (configs.UnrestrainedPylon)
            {
                return true;
            }
            return base.ValidTeleportCheck_PreAnyDanger(pylonInfo);
        }

        //不限制传送环境
        public override bool? ValidTeleportCheck_PreBiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
        {
            if (configs.UnrestrainedPylon)
            {
                return true;
            }
            return base.ValidTeleportCheck_PreBiomeRequirements(pylonInfo, sceneData);
        }
    }
}