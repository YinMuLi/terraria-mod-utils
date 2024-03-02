using Branch.Common.Configs;
using MonoMod.Cil;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Content.Global
{
    internal class LoadableCenter : ILoadable
    {
        private float minZoom => ClientConfig.Instance.MinZoom;
        private const float MAX_ZOOM = 4f;

        public void Unload()
        {
        }

        public void Load(Mod mod)
        {
            On_ItemSlot.PickItemMovementAction += PlaceCoinSlot;
            On_Main.UpdateViewZoomKeys += UpdateViewZoom;
            On_Item.CanFillEmptyAmmoSlot += (_, _) => false; //拾取到的物品都不放在弹药栏
            On_Player.QuickStackAllChests += QuickStackAllChests;
            //On_WorldGen.ScoreRoom_IsThisRoomOccupiedBySomeone += (_, _, _) => false;//一个房间可以被多个NPC使用
            IL_Main.DoDraw += PatchZoomBounds;
            //IL_Player.ItemCheck_CheckFishingBobber_PickAndConsumeBait += PatchNoConsumeBait;
            //IL_Player.QuickStackAllChests += PatchQuickStack;
        }

        private void QuickStackAllChests(On_Player.orig_QuickStackAllChests orig, Player self)
        {
            orig(self);
            if (!ClientConfig.Instance.SwitchInventory) return;
            //交换物品栏-->截取[更好的体验]
            void Switch(Player self)
            {
                if (self.ItemTimeIsZero && self.itemAnimation is 0)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        (self.inventory[i], self.inventory[i + 10]) = (self.inventory[i + 10], self.inventory[i]);
                        if (Main.netMode is NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncEquipment, number: Main.myPlayer, number2: i,
                                number3: Main.LocalPlayer.inventory[i].prefix);
                            NetMessage.SendData(MessageID.SyncEquipment, number: Main.myPlayer, number2: i + 10,
                                number3: Main.LocalPlayer.inventory[i].prefix);
                        }
                    }
                }
            }
            Switch(self);
            orig(self);
            Switch(self);
        }

        private int PlaceCoinSlot(On_ItemSlot.orig_PickItemMovementAction orig, Item[] inv, int context, int slot, Item checkItem)
        {
            if (context == 1 && checkItem.type == ItemID.PiggyBank)
            {
                //猪猪存钱罐能放置在钱币槽
                return 0;
            }
            return orig(inv, context, slot, checkItem);
        }

        private void UpdateViewZoom(On_Main.orig_UpdateViewZoomKeys orig, Main self)
        {
            if (Main.inFancyUI) return;
            //泰拉瑞亚源码
            float num = 0.01f;//源码是0.02f
            if (PlayerInput.Triggers.Current.ViewZoomIn)
            {
                Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget + num, minZoom, MAX_ZOOM);
            }
            if (PlayerInput.Triggers.Current.ViewZoomOut)
            {
                Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget - num, minZoom, MAX_ZOOM);
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
        //private void PatchNoConsumeBait(ILContext il)
        //{
        //    var c = new ILCursor(il);
        //    //获取消耗饵料的类型

        //    //IL_0112: ldfld int32 Terraria.Item::'type'
        //    //IL_0117: stind.i4
        //    //IL_0118: ldarg.3
        //    //IL_0119: ldind.i4
        //    //IL_011A: ldc.i4    2895
        //    if (!c.TryGotoNext(MoveType.After,
        //        i => i.MatchLdcI4(2895)
        //        )) return;
        //    c.Index -= 4;
        //    int itemtype = -1;
        //    c.EmitDelegate<Func<int, int>>(returnvalue =>
        //    {
        //        itemtype = returnvalue;
        //        return returnvalue;
        //    });
        //    //item.stack--;
        //    //IL_0179: ldloc.3
        //    //IL_017A: dup
        //    //IL_017B: ldfld int32 Terraria.Item::stack
        //    //IL_0180: ldc.i4.1  //把消耗1改为0
        //    //IL_0181: sub
        //    //IL_0182: stfld int32 Terraria.Item::stack
        //    //IL_0187: ldloc.3
        //    //IL_0188: ldfld int32 Terraria.Item::stack
        //    //IL_018D: ldc.i4.0
        //    //IL_018E: bgt.s

        //    if (!c.TryGotoNext(MoveType.After,
        //        i => i.Match(OpCodes.Dup),
        //        i => i.MatchLdfld<Item>(nameof(Item.stack)),
        //        i => i.Match(OpCodes.Ldc_I4_1)
        //        )) return;
        //    c.EmitDelegate<Func<int, int>>(returnValue =>
        //    {
        //        if (itemtype == ModContent.ItemType<GoldenBait>())
        //        {
        //            return 0;
        //        }
        //        return returnValue;
        //    });
        //}

        //private void PatchQuickStack(ILContext il)
        //{
        //    //修改快速堆叠从第一行开始
        //    var c = new ILCursor(il);
        //    //第一处单人模式
        //    //IL_01EF: bne.un    IL_038D
        //    //IL_01F4: ldc.i4.s  10  <--修改
        //    //IL_01F6: stloc.s V_15
        //    //IL_01F8: br IL_02A5
        //    if (!c.TryGotoNext(MoveType.After,
        //        i => i.Match(OpCodes.Bne_Un),
        //        i => i.Match(OpCodes.Ldc_I4_S))) return;
        //    c.EmitDelegate<Func<int, int>>(returnvalue =>
        //    {
        //        return 0;
        //    });
        //    //第二处多人模式
        //    //IL_038C: ret
        //    //IL_038D: ldc.i4.s  10  <--修改
        //    //IL_038F: stloc.s V_17
        //    //IL_0391: br IL_0445
        //    if (!c.TryGotoNext(MoveType.After,
        //        i => i.Match(OpCodes.Ret),
        //        i => i.Match(OpCodes.Ldc_I4_S))) return;
        //    c.EmitDelegate<Func<int, int>>(returnvalue =>
        //    {
        //        return 0;
        //    });
        //}
    }
}