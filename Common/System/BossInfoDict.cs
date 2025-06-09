using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YinMu.Common.System
{
    internal class BossInfoDict : ModSystem
    {
        public class BossInfo
        {
            public string key;
            public List<int> spawnItems;
        }

        public static Dictionary<string, BossInfo> AllBoosInfo = new Dictionary<string, BossInfo>();

        public override void PostAddRecipes()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist)
             && (bossChecklist.Call(["GetBossInfoDictionary", Mod, 1.6])
             is Dictionary<string, Dictionary<string, object>> bossInfoDict))
            {
                foreach (KeyValuePair<string, Dictionary<string, object>> bossEntry in bossInfoDict)
                {
                    string key = bossEntry.Key;
                    Dictionary<string, object> infoData = bossEntry.Value;
                    List<int> spawnItems = ((infoData.ContainsKey("spawnItems") && infoData["spawnItems"] is List<int> items) ? items : new List<int>());
                    Func<bool> downedFunc = ((infoData.ContainsKey("downed") && infoData["downed"] is Func<bool> func) ? func : null);
                    List<int> npcIDs = ((infoData.ContainsKey("npcIDs") && infoData["npcIDs"] is List<int> ids) ? ids : new List<int>());
                    AllBoosInfo.Add(key, new BossInfo
                    {
                        key = key,
                        spawnItems = spawnItems,
                    });
                }
            }
        }

        public static List<int> GetAllBossSpawnItems()
        {
            var list = new List<int>();
            foreach (var bossInfo in AllBoosInfo)
            {
                list.AddRange(bossInfo.Value.spawnItems);
            }
            return list;
        }
    }
}