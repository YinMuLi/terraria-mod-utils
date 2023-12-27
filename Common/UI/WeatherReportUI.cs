using Branch.Common.Utils;
using Branch.Content.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Common.UI
{
    internal class WeatherReportUI : UIState
    {
        public static bool Visible { get; private set; }

        private UIPanel panel;
        private WeatherReport weatherReport;

        public override void OnInitialize()
        {
            //实例化一个面板
            panel = new UIPanel();
            UIUtils.SetRectangle(panel, left: 600f, top: 100f, width: 150f, height: 200f);
            //下雨事件按钮
            //用tr原版图片实例化一个图片按钮
            UIHoverImageButton button = new(ModContent.Request<Texture2D>("Branch/Assets/Images/UI/Rain"), "下雨");
            //将按钮注册入面板中，这个按钮的坐标将以面板的坐标为基础计算
            UIUtils.SetRectangle(button, 0f, 0f, 32f, 32f);
            panel.Append(button);
            //将这个面板注册到UIState
            Append(panel);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            //防止点击项目
            // Otherwise, we can check a child element instead
            if (panel.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            //防止滚轮滚动快捷栏项目
            // Otherwise, we can check a child element instead
            if (panel.IsMouseHovering)
            {
                PlayerInput.LockVanillaMouseScroll("Brach/Weather Report"); // The passed in string can be anything.
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (weatherReport == null)
            {
                Close();
                return;
            }
            base.Update(gameTime);
        }

        public void Open(WeatherReport weatherReport)
        {
            this.weatherReport = weatherReport;
            Visible = true;
        }

        public void Close()
        {
            weatherReport = null;
            Visible = false;
        }
    }
}