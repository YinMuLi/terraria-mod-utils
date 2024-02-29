using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;

namespace Branch.Common.Interface
{
    internal interface IItemClickable
    {
        private static bool preMousePressed;

        public void OnRightClicked(Item item);

        public void HandleHover(Item[] inventory, int context, int slot)
        {
            if (context is not ItemSlot.Context.InventoryItem) return;
            var item = inventory[slot];
            MouseState mouseState = Mouse.GetState();
            if (preMousePressed)
            {
                //当第一次被按下，值为True,满足条件，一直判断知道松开为False
                preMousePressed = mouseState.MiddleButton is ButtonState.Pressed;
            }
            if (mouseState.MiddleButton is ButtonState.Pressed && !preMousePressed)
            {
                //只要鼠标右键被按下不松开，就会一直运行，preMousePressed解决这个问题
                preMousePressed = true;
                OnRightClicked(item);
            }
        }
    }
}