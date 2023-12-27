using Branch.Common.Utils;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Branch.Common.Interface
{
    /// <summary>
    /// 容器中的物品不需要关闭UI右击点击
    /// </summary>
    internal interface IItemRightClickable
    {
        private static bool preMousePressed;

        public void OnRightClicked(Item item);

        public void HandleHover(Item[] inventory, int context, int slot)
        {
            var item = inventory[slot];
            MouseState mouseState = Mouse.GetState();
            if (preMousePressed)
            {
                //当第一次被按下，值为True,满足条件，一直判断知道松开为False
                preMousePressed = mouseState.RightButton is ButtonState.Pressed;
            }
            if (mouseState.RightButton is ButtonState.Pressed && !preMousePressed)
            {
                //只要鼠标右键被按下不松开，就会一直运行，preMousePressed解决这个问题
                preMousePressed = true;
                OnRightClicked(item);
            }
        }
    }
}