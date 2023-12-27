﻿using Branch.Common.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Common.UI
{
    internal class WeatherReportUI : UIState
    {
        public static bool Visible;
        private UIPanel panel;

        public override void OnInitialize()
        {
            //实例化一个面板
            panel = new UIPanel();
            UIUtils.SetRectangle(panel, left: 600f, top: 100f, width: 150f, height: 200f);
            //下雨事件按钮
            //用tr原版图片实例化一个图片按钮
            UIImageButton button = new UIImageButton(ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonPlay"));
            //设置按钮距宽度
            button.Width.Set(22f, 0f);
            //设置按钮高度
            button.Height.Set(22f, 0f);
            //设置按钮距离所属ui部件的最左端的距离
            button.Left.Set(-11f, 0.5f);
            //设置按钮距离所属ui部件的最顶端的距离
            button.Top.Set(-11f, 0.5f);
            //将按钮注册入面板中，这个按钮的坐标将以面板的坐标为基础计算
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
    }
}