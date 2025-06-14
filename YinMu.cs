/**===== 全局引用 =====**/

global using YinMu.Common.Utils;

/**===== 全局引用 =====**/

using YinMu.Content.Buffs;
using YinMu.Content.Items.BossSummon;
using YinMu.Content.Items.Misc;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using Terraria.ModLoader;

using static System.Net.Mime.MediaTypeNames;

namespace YinMu
{
    public class YinMu : Mod
    {
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("ImproveGame", out Mod improveGame))
            {
                improveGame.Call(
                    "AddStation",
                    ModContent.ItemType<Content.Items.Misc.MiracleCake>(),
                    ModContent.BuffType<Content.Buffs.SugarRush>()
                );
            }
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                bossChecklist.Call("SubmitEntrySpawnItems", this, new Dictionary<string, object>()
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