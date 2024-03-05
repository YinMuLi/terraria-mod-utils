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
            On_ItemSlot.PickItemMovementAction += PickItemMovementAction;
            On_Main.UpdateViewZoomKeys += UpdateViewZoom;
            On_Item.CanFillEmptyAmmoSlot += (_, _) => false; //拾取到的物品都不放在弹药栏
            On_Player.QuickStackAllChests += QuickStackAllChests;
            //On_WorldGen.ScoreRoom_IsThisRoomOccupiedBySomeone += (_, _, _) => false;//一个房间可以被多个NPC使用
            IL_Main.DoDraw += PatchZoomBounds;
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

        private int PickItemMovementAction(On_ItemSlot.orig_PickItemMovementAction orig, Item[] inv, int context, int slot, Item checkItem)
        {
            if (context == ItemSlot.Context.InventoryCoin && checkItem.type == ItemID.PiggyBank)
            {
                //猪猪存钱罐能放置在钱币槽
                return 0;
            }
            //武器能放置在装饰栏...不支持模组的
            if (checkItem.damage >= 0 && context is ItemSlot.Context.EquipArmorVanity or ItemSlot.Context.EquipAccessoryVanity)
            {
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
    }
}