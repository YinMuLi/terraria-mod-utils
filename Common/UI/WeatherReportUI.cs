using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Branch.Common.UI
{
    internal class WeatherReportUI : UIState
    {
        public static bool visible;

        public override void OnInitialize()
        {
            //实例化一个面板
            UIPanel panel = new UIPanel();
            //设置面板的宽度
            panel.Width.Set(488f, 0f);
            //设置面板的高度
            panel.Height.Set(568f, 0f);
            //设置面板距离屏幕最左边的距离
            panel.Left.Set(-244f, 0.5f);
            //设置面板距离屏幕最上端的距离
            panel.Top.Set(-284f, 0.5f);
            //将这个面板注册到UIState
            Append(panel);
        }
    }
}