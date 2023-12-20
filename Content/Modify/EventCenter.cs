using Branch.Common.Utils;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Content.Modify
{
    /// <summary>
    /// 取消订阅事件
    /// 1.猪猪存钱罐能放置在钱币槽
    /// </summary>
    internal class EventCenter : ILoadable
    {
        public void Load(Mod mod)
        {
            On_ItemSlot.PickItemMovementAction += OnPlaceCoinSlot;
            //On_Main.UpdateViewZoomKeys += OnUpdateViewZoom;
            //IL_Main.DoDraw += OnChangeZoomBounds;
        }

        public void Unload()
        {
            On_ItemSlot.PickItemMovementAction -= OnPlaceCoinSlot;
            //On_Main.UpdateViewZoomKeys -= OnUpdateViewZoom;
            //IL_Main.DoDraw -= OnChangeZoomBounds;
        }

        private int OnPlaceCoinSlot(On_ItemSlot.orig_PickItemMovementAction orig, Item[] inv, int context, int slot, Item checkItem)
        {
            if (context == 1 && checkItem.type == ItemID.PiggyBank)
            {
                //猪猪存钱罐能放置在钱币槽
                return 0;
            }
            return orig(inv, context, slot, checkItem);
        }

        //private void OnUpdateViewZoom(On_Main.orig_UpdateViewZoomKeys orig, Main self)
        //{
        //    if (Main.inFancyUI)
        //    {
        //        //猜测：拍照模式
        //        return;
        //    }
        //    float num = 0.01f * Main.GameZoomTarget;
        //    if (PlayerInput.Triggers.Current.ViewZoomIn)
        //    {
        //        Main.GameZoomTarget += num;
        //    }
        //    if (PlayerInput.Triggers.Current.ViewZoomOut)
        //    {
        //        Main.GameZoomTarget -= num;
        //    }
        //    ModUtils.ShowText($"{Main.GameZoomTarget}");
        //    //orig(self);
        //}

        //private void OnChangeZoomBounds(ILContext il)
        //{
        //    var c = new ILCursor(il);
        //    c.Index++;
        //}
    }
}