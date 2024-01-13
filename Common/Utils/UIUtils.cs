using Terraria.UI;

namespace Branch.Common.Utils
{
    public static partial class ModUtils
    {
        /// <summary>
        /// 设置UI元素的位置和大小
        /// </summary>
        /// <param name="element"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void SetRectangle(UIElement element, float left, float top, float width, float height)
        {
            //第二个参数的位置百分比，如果是0.5f就是在居中的位置再发生偏移
            element.Left.Set(left, 0f);
            element.Top.Set(top, 0f);
            element.Width.Set(width, 0f);
            element.Height.Set(height, 0f);
        }
    }
}