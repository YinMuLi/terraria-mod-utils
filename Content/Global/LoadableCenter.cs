using Branch.Common.Configs;
using Branch.Content.Items;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.Utils;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Content.Global
{
    /// <summary>
    /// 取消订阅事件
    /// 1.猪猪存钱罐能放置在钱币槽
    /// 2.修改游戏缩放
    /// </summary>
    internal class LoadableCenter : ILoadable
    {
        private float minZoom => ClientConfig.Instance.MinZoom;
        private const float MAX_ZOOM = 4f;

        public void Load(Mod mod)
        {
            On_ItemSlot.PickItemMovementAction += OnPlaceCoinSlot;
            On_Main.UpdateViewZoomKeys += OnUpdateViewZoom;
            IL_Main.DoDraw += PatchZoomBounds;
            IL_Player.ItemCheck_CheckFishingBobber_PickAndConsumeBait += PatchNoConsumeBait;
        }

        public void Unload()
        {
            On_ItemSlot.PickItemMovementAction -= OnPlaceCoinSlot;
            On_Main.UpdateViewZoomKeys -= OnUpdateViewZoom;
            IL_Main.DoDraw -= PatchZoomBounds;
            IL_Player.ItemCheck_CheckFishingBobber_PickAndConsumeBait -= PatchNoConsumeBait;
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
                Main.GameZoomTarget = Math.Clamp(Main.GameZoomTarget + num, minZoom, MAX_ZOOM);
            }
            if (PlayerInput.Triggers.Current.ViewZoomOut)
            {
                Main.GameZoomTarget = Math.Clamp(Main.GameZoomTarget - num, minZoom, MAX_ZOOM);
            }
        }

        private void PatchZoomBounds(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(MoveType.After,
            i => i.MatchLdsfld<Main>("GameViewMatrix"),
            i => i.MatchLdsfld<Main>("ForcedMinimumZoom"),
            i => i.MatchLdsfld<Main>("GameZoomTarget"),
            i => i.MatchLdcR4(1))) return;
            c.Prev.Operand = minZoom;
            c.Index++;
            c.Prev.Operand = MAX_ZOOM;
        }

        /// <summary>
        /// 不消耗鱼饵
        /// </summary>
        /// <param name="il"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void PatchNoConsumeBait(ILContext il)
        {
            var c = new ILCursor(il);
            //获取消耗饵料的类型

            //IL_0112: ldfld int32 Terraria.Item::'type'
            //IL_0117: stind.i4
            //IL_0118: ldarg.3
            //IL_0119: ldind.i4
            //IL_011A: ldc.i4    2895
            if (!c.TryGotoNext(MoveType.After,
                i => i.MatchLdcI4(2895)
                )) return;
            c.Index -= 4;
            int itemtype = -1;
            c.EmitDelegate<Func<int, int>>(returnvalue =>
            {
                itemtype = returnvalue;
                return returnvalue;
            });
            //item.stack--;
            //IL_0179: ldloc.3
            //IL_017A: dup
            //IL_017B: ldfld int32 Terraria.Item::stack
            //IL_0180: ldc.i4.1  //把消耗1改为0
            //IL_0181: sub
            //IL_0182: stfld int32 Terraria.Item::stack
            //IL_0187: ldloc.3
            //IL_0188: ldfld int32 Terraria.Item::stack
            //IL_018D: ldc.i4.0
            //IL_018E: bgt.s

            if (!c.TryGotoNext(MoveType.After,
                i => i.Match(OpCodes.Dup),
                i => i.MatchLdfld<Item>(nameof(Item.stack)),
                i => i.Match(OpCodes.Ldc_I4_1)
                )) return;
            c.EmitDelegate<Func<int, int>>(returnValue =>
            {
                if (itemtype == ModContent.ItemType<GoldenBait>())
                {
                    return 0;
                }
                return returnValue;
            });
        }
    }
}