using Branch.Common.Utils;
using Branch.Content.Buffs;
using Branch.Content.Items;
using System;
using Terraria;
using Terraria.GameInput;
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