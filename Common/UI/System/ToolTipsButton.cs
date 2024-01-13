using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace Branch.Common.UI.System
{
    /// <summary>
    /// UI的绘制层数要与Mouse Text低一层，不然没法显示
    /// </summary>
    internal class ToolTipsButton : UIColoredImageButton
    {
        private string hoverText;

        public ToolTipsButton(Asset<Texture2D> texture, string hoverText) : base(texture)
        {
            this.hoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (IsMouseHovering)
            {
                Main.hoverItemName = hoverText;
            }
        }
    }
}