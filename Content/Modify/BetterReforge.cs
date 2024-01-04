using Branch.Common.Players;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Branch.Content.Modify
{
    internal class BetterReforge : GlobalItem
    {
        public override bool InstancePerEntity => true;
        private HashSet<int> historyPrefix = new();
        private const string KEY = "HistoryPrefix";

        public override void SaveData(Item item, TagCompound tag)
        {
            if (historyPrefix.Count > 0) tag[KEY] = historyPrefix.ToList();
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            historyPrefix = tag.GetList<int>(KEY).ToHashSet();
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write((ushort)historyPrefix.Count);
            foreach (var prefix in historyPrefix)
            {
                writer.Write((ushort)prefix);
            }
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            historyPrefix.Clear();
            ushort count = reader.ReadUInt16();
            for (int i = 0; i < count; i++)
            {
                historyPrefix.Add(reader.ReadUInt16());
            }
        }

        public override void PostReforge(Item item)
        {
            if (historyPrefix.Contains(item.prefix))
            {
                //获取价值
                int value = item.value;
                Player p = Main.LocalPlayer;
                ItemLoader.ReforgePrice(item, ref value, ref p.discountAvailable);
                //退款
                var coinCount = Utils.CoinsSplit(value / 5);
                Main.LocalPlayer.GetModPlayer<ReforgePlayer>()?.Refund(coinCount);
                return;
            }
            historyPrefix.Add(item.prefix);
        }
    }
}