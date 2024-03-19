using Microsoft.Xna.Framework.Input;
using System;
using Terraria;
using Terraria.UI;

namespace Branch.Common.Interface
{
    internal interface IItemClickable
    {
        [Flags]
        public enum ClickType
        {
            None, Right, Left, Middle
        }

        private static bool preMousePressed_Middle;
        private static bool preMousePressed_Right;
        private static bool preMousePressed_Left;

        ClickType CanClickable(Item item, int index);

        void OnRightClicked(Item item, int index) { }

        void OnMiddleClicked(Item item, int index) { }

        void OnLeftClicked(Item item, int index) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inventory">物品所在的数组</param>
        /// <param name="context">存储的标识</param>
        /// <param name="slot">存储的位置</param>
        void HandleHover(Item[] inventory, int context, int slot)
        {
            if (context is not ItemSlot.Context.InventoryItem) return;
            var item = inventory[slot];
            MouseState mouseState = Mouse.GetState();
            //右键
            if (CanClickable(item, slot).HasFlag(ClickType.Right))
            {
                if (preMousePressed_Right)
                {
                    //当第一次被按下，值为True,满足条件，一直判断知道松开为False
                    preMousePressed_Right = mouseState.RightButton is ButtonState.Pressed;
                }
                if (mouseState.RightButton is ButtonState.Pressed && !preMousePressed_Right)
                {
                    //只要鼠标右键被按下不松开，就会一直运行，preMousePressed解决这个问题
                    preMousePressed_Right = true;
                    OnRightClicked(item, slot);
                }
            }
            //左键
            if (CanClickable(item, slot).HasFlag(ClickType.Left))
            {
                if (preMousePressed_Left)
                {
                    //当第一次被按下，值为True,满足条件，一直判断知道松开为False
                    preMousePressed_Left = mouseState.LeftButton is ButtonState.Pressed;
                }
                if (mouseState.LeftButton is ButtonState.Pressed && !preMousePressed_Left)
                {
                    //只要鼠标右键被按下不松开，就会一直运行，preMousePressed解决这个问题
                    preMousePressed_Left = true;
                    OnLeftClicked(item, slot);
                }
            }
            //中键
            if (CanClickable(item, slot).HasFlag(ClickType.Middle))
            {
                if (preMousePressed_Middle)
                {
                    //当第一次被按下，值为True,满足条件，一直判断知道松开为False
                    preMousePressed_Middle = mouseState.MiddleButton is ButtonState.Pressed;
                }
                if (mouseState.MiddleButton is ButtonState.Pressed && !preMousePressed_Middle)
                {
                    //只要鼠标右键被按下不松开，就会一直运行，preMousePressed解决这个问题
                    preMousePressed_Middle = true;
                    OnMiddleClicked(item, slot);
                }
            }
        }
    }
}