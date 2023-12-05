using Terraria.Localization;

namespace Branch.Common.Utils
{
    public static class ModUtils
    {
        /// <summary>
        /// 获取角色对话
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="index">对话编号</param>
        /// <returns>string</returns>
        public static string GetChatText(string name, int index)
        {
            return Language.GetTextValue($"Mods.NPCs.{name}.Chat{index}");
        }
    }
}