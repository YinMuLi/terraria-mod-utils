/**===== 全局引用 =====**/

global using Branch.Common.Utils;

/**===== 全局引用 =====**/

using Branch.Content.Buffs;
using Branch.Content.Items;
using Terraria.ModLoader;

namespace Branch
{
    public class Branch : Mod
    {
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("ImproveGame", out Mod improveGame))
            {
                improveGame.Call(
                    "AddStation",
                    ModContent.ItemType<FinalStation>(),
                    ModContent.BuffType<FinalStationBuff>()
                );
            }
        }
    }
}