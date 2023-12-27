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
    /// 2.修改游戏缩放
    /// </summary>
    internal class EventCenter : ILoadable
    {
        private const float MIN_GAME_ZOOM = 0.87f;//经过测试
        private const float MAX_GAME_ZOOM = 4f;

        public void Load(Mod mod)
        {
            On_ItemSlot.PickItemMovementAction += OnPlaceCoinSlot;
            On_Main.UpdateViewZoomKeys += OnUpdateViewZoom;
            IL_Main.DoDraw += OnChangeZoomBounds;
        }

        public void Unload()
        {
            On_ItemSlot.PickItemMovementAction -= OnPlaceCoinSlot;
            On_Main.UpdateViewZoomKeys -= OnUpdateViewZoom;
            IL_Main.DoDraw -= OnChangeZoomBounds;
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

        private void OnUpdateViewZoom(On_Main.orig_UpdateViewZoomKeys orig, Main self)
        {
            if (Main.inFancyUI)
            {
                //猜测：拍照模式
                return;
            }
            float num = 0.01f * Main.GameZoomTarget;
            if (PlayerInput.Triggers.Current.ViewZoomIn)
            {
                Main.GameZoomTarget = Math.Clamp(Main.GameZoomTarget + num, MIN_GAME_ZOOM, MAX_GAME_ZOOM);
            }
            if (PlayerInput.Triggers.Current.ViewZoomOut)
            {
                Main.GameZoomTarget = Math.Clamp(Main.GameZoomTarget - num, MIN_GAME_ZOOM, MAX_GAME_ZOOM);
            }
        }

        private void OnChangeZoomBounds(ILContext il)
        {
            //补丁
            var c = new ILCursor(il);
            if (!c.TryGotoNext(MoveType.After,
            i => i.MatchLdsfld<Main>("GameViewMatrix"),
            i => i.MatchLdsfld<Main>("ForcedMinimumZoom"),
            i => i.MatchLdsfld<Main>("GameZoomTarget"),
            i => i.MatchLdcR4(1))) return;
            c.Prev.Operand = MIN_GAME_ZOOM;
            c.Index++;
            c.Prev.Operand = MAX_GAME_ZOOM;
        }
    }
}