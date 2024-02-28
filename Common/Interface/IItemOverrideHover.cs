using Terraria;

namespace Branch.Common.Interface
{
    internal interface IItemOverrideHover
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="inventory">物品所在的存储数组</param>
        /// <param name="context">物品槽的标识</param>
        /// <param name="slot">物品所在槽的索引</param>
        /// <returns>是否禁用原版。true为禁用，false不禁用</returns>
        bool OverrideHover(Item[] inventory, int context, int slot);
    }
}