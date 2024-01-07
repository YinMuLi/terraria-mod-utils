using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Common.Players
{
    public partial class BranchPlayer : ModPlayer
    {
        /// <summary>
        /// Refund:退款
        /// </summary>
        /// <param name="coinCount"></param>
        internal void Refund(int[] coinCount)
        {
            if (coinCount[0] > 0) Player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.CopperCoin, coinCount[0]);
            if (coinCount[1] > 0) Player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.SilverCoin, coinCount[1]);
            if (coinCount[2] > 0) Player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.GoldCoin, coinCount[2]);
            if (coinCount[3] > 0) Player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.PlatinumCoin, coinCount[3]);
        }
    }
}