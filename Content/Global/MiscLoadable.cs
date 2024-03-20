using Branch.Common.Configs;
using Branch.Common.Extensions;
using Branch.Common.Players;
using Humanizer;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Content.Global
{
    internal class MiscLoadable : ILoadable
    {
        private float minZoom => ClientConfig.Instance.MinZoom;
        private const float MAX_ZOOM = 4f;//屏幕放大最大尺寸
        private Asset<Texture2D> NoChainButtonTexture;
        private Asset<Texture2D> ChainButtonTexture;
        private Asset<Texture2D> PlayChainButtonTexture;

        public void Unload()
        {
            ChainButtonTexture = null;
            NoChainButtonTexture = null;
            PlayChainButtonTexture = null;
        }

        public void Load(Mod mod)
        {
            NoChainButtonTexture = mod.Assets.Request<Texture2D>("Assets/UI/NoChainButton", AssetRequestMode.AsyncLoad);
            ChainButtonTexture = mod.Assets.Request<Texture2D>("Assets/UI/ChainButton", AssetRequestMode.AsyncLoad);
            PlayChainButtonTexture = mod.Assets.Request<Texture2D>("Assets/UI/PlayChainButton", AssetRequestMode.AsyncLoad);
            On_ItemSlot.PickItemMovementAction += PickItemMovementAction;
            On_Main.UpdateViewZoomKeys += UpdateViewZoom;
            On_Item.CanFillEmptyAmmoSlot += (_, _) => false; //拾取到的物品都不放在弹药栏
            On_Player.QuickStackAllChests += QuickStackAllChests;
            //On_WorldGen.ScoreRoom_IsThisRoomOccupiedBySomeone += (_, _, _) => false;//一个房间可以被多个NPC使用
            IL_Main.DoDraw += Patch_ZoomBounds;
            IL_UICharacterListItem.ctor += Patch_UICharacterListItem_Ctor;//构造函数
            IL_UIWorldListItem.ctor += Patch_UIWorldListItem_Ctor;
            IL_Player.TrySwitchingLoadout += Patch_TrySwitchingLoadout;//
        }

        private void Patch_TrySwitchingLoadout(ILContext il)
        {
            //IL_006D: callvirt instance void Terraria.EquipmentLoadout::Swap(class Terraria.Player)
            //<--
            //IL_0072: ldarg.0
            //IL_0073: ldarg.1
            //IL_0074: stfld
            var c = new ILCursor(il);
            if (!c.TryGotoNext(MoveType.After,
                i => i.Match(OpCodes.Callvirt),
                i => i.MatchLdarg0(),
                i => i.MatchLdarg1()
                )) return;
            c.Index--;
            c.EmitLdarg(0);//玩家
            c.EmitLdarg(1);//loadoutIndex
            c.EmitDelegate<Action<Player, int>>((player, loadoutIndex) =>
            {
                if (!ClientConfig.Instance.NoSwapDecoration) return;

                //染料
                Item[] currentDye = player.dye;
                Item[] preDye = player.Loadouts[player.CurrentLoadoutIndex].Dye;
                for (int i = 0; i < currentDye.Length; i++)
                {
                    if (currentDye[i].IsAir)
                    {
                        Utils.Swap<Item>(ref currentDye[i], ref preDye[i]);
                    }
                }
                //装饰
                Item[] currentArmor = player.armor;
                Item[] preArmor = player.Loadouts[player.CurrentLoadoutIndex].Armor;
                for (int i = 10; i < currentArmor.Length; i++)
                {
                    if (currentArmor[i].IsAir)
                    {
                        Utils.Swap<Item>(ref currentArmor[i], ref preArmor[i]);
                    }
                }
            });
        }

        /// <summary>
        /// 世界界面构造函数
        /// </summary>
        /// <param name="il"></param>
        private void Patch_UIWorldListItem_Ctor(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(MoveType.After,
            i => i.MatchLdloc0(),
            i => i.Match(OpCodes.Ldc_R4, 4f),//Why 4f?
            i => i.MatchAdd(),
            i => i.MatchStloc0()
            )) return;
            c.Index -= 4;
            c.EmitLdarg0();
            c.EmitLdarg1();
            c.EmitLdarg2();
            c.EmitLdarg3();
            c.EmitLdloca(0);
            c.EmitDelegate((UIWorldListItem self, WorldFileData data, int orderInList, bool canBePlayed, ref float num) =>
            {
                if (!canBePlayed) return;
                BranchPlayer modPlayer = Main.ActivePlayerFileData.Player.ModPlayer();
                UIImageButton linkButton = new(ChainButtonTexture)
                {
                    VAlign = 1f,
                    Left = StyleDimension.FromPixelsAndPercent(num, 0f)
                };
                //连接玩家或者解除绑定玩家
                linkButton.OnLeftClick += (_, _) =>
                {
                    if (modPlayer.linkWorldID == data.UniqueId.ToString())
                        //解除绑定
                        modPlayer.linkWorldID = null;
                    else
                        //连接玩家
                        modPlayer.linkWorldID = data.UniqueId.ToString();

                    bool prevMapEnabled = Main.mapEnabled;
                    Main.mapEnabled = false;
                    Player.SavePlayer(Main.ActivePlayerFileData, true);
                    Main.mapEnabled = prevMapEnabled;
                };
                linkButton.OnMouseOver += (_, _) =>
                {
                    if (modPlayer.linkWorldID == data.UniqueId.ToString())
                        self._buttonLabel.SetText(Language.GetTextValue("Mods.UI.LinkWorld.Unlink")
                            .FormatWith(modPlayer.Player.name));
                    else
                        self._buttonLabel.SetText(Language.GetTextValue("Mods.UI.LinkWorld.Link")
                            .FormatWith(modPlayer.Player.name));
                };
                linkButton.OnMouseOut += self.ButtonMouseOut;
                linkButton.OnUpdate += (_) =>
                {
                    if (modPlayer.linkWorldID == data.UniqueId.ToString())
                        linkButton.SetImage(ChainButtonTexture);
                    else
                        linkButton.SetImage(NoChainButtonTexture);
                };

                self.Append(linkButton);
                num += 24f;
            });
        }

        /// <summary>
        /// 玩家界面构造函数
        /// </summary>
        /// <param name="il"></param>
        private void Patch_UICharacterListItem_Ctor(ILContext il)
        {
            //num+=4
            //IL_0443: ldloc.0
            //IL_0444: ldc.r4    4
            //IL_0449: add
            //stloc.0

            var c = new ILCursor(il);
            if (!c.TryGotoNext(MoveType.After,
            i => i.MatchLdloc0(),
            i => i.Match(OpCodes.Ldc_R4, 4f),//Why 4f?
            i => i.MatchAdd(),
            i => i.MatchStloc0()
            )) return;
            //stloc.0
            //->找到的是这里？
            //......
            c.Index -= 4;
            c.EmitLdarg0();//把方法第n个参数装入堆栈,在非静态函数中，第0个参数是一个隐含的参数，代表this
            c.EmitLdarg1();//PlayerFileData
            c.EmitLdarg2();//snapPointIndex
            //Ldloc 将指定索引处的局部变量加载到计算堆栈上。
            //Ldloca 将位于特定索引处的局部变量的地址加载到计算堆栈上。能用ref
            c.EmitLdloca(0);//推送本地变量就是num（少打了一个a，检查一个小时...哭）
            c.EmitDelegate((UICharacterListItem self, PlayerFileData data, int snapPointIndex, ref float num) =>
            {
                BranchPlayer modPlayer = data.Player.ModPlayer();
                WorldFileData worldData = ModUtils.GetWorldFileData(modPlayer.linkWorldID);
                //连接的世界被删除了
                if (modPlayer.linkWorldID != null && (worldData == null || !worldData.IsValid))
                    modPlayer.linkWorldID = null;
                //多人游戏不可用
                if (modPlayer.linkWorldID == null && Main.menuMultiplayer && !Main.menuServer)
                    return;
                UIImageButton linkButton = new(modPlayer.linkWorldID != null ? PlayChainButtonTexture : NoChainButtonTexture)
                {
                    VAlign = 1f,
                    Left = StyleDimension.FromPixelsAndPercent(num, 0f)
                };
                linkButton.OnLeftClick += (evt, element) =>
                {
                    if (modPlayer.linkWorldID == null) return;
                    data.SetAsActive();
                    UIWorldListItem world = new(worldData, 0, true);
                    world.PlayGame(evt, element);
                };
                linkButton.OnMouseOut += self.ButtonMouseOut;
                linkButton.OnMouseOver += (_, _) =>
                {
                    if (modPlayer.linkWorldID == null)
                        self._buttonLabel.SetText(Language.GetTextValue("Mods.UI.LinkWorld.NoWorld"));
                    else
                        self._buttonLabel.SetText(Language.GetTextValue("Mods.UI.LinkWorld.PlayWorld")
                            .FormatWith(worldData.Name));
                };
                self.Append(linkButton);
                num += 24f;
            });
        }

        private void Patch_ZoomBounds(ILContext il)
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
            //if (context == ItemSlot.Context.InventoryCoin && checkItem.type == ItemID.PiggyBank)
            //{
            //    //猪猪存钱罐能放置在钱币槽
            //    return 0;
            //}
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
    }
}