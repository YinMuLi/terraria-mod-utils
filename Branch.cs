/**===== 全局引用 =====**/

global using Branch.Common.Utils;

/**===== 全局引用 =====**/

using Branch.Content.Buffs;
using Branch.Content.Items.BossSummon;
using Branch.Content.Items.Misc;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using Terraria.ModLoader;

using static System.Net.Mime.MediaTypeNames;

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
                    ModContent.ItemType<Cake>(),
                    ModContent.BuffType<CommonStationBuff>()
                );
            }
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossList))
            {
                bossList.Call("SubmitEntrySpawnItems", this, new Dictionary<string, object>()
            {
                { "Terraria Plantera", ModContent.ItemType<PlanteraBulb>()},
                { "Terraria HallowBoss", ModContent.ItemType<SummonHallowBoss>()},
                { "Terraria WallofFlesh", ModContent.ItemType<BloodyDoll>()},
                { "Terraria DukeFishron", ModContent.ItemType<SummonDukeFishron>()}
            });
            }
        }
    }
}