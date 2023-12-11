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
                    "AddStation",//加入你自己的增益站1
                    ModContent.ItemType<FinalStation>(),
                    ModContent.BuffType<FinalStationBuff>()
                );
            }
        }
    }
}