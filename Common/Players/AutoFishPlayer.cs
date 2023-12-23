using Branch.Common.Configs;
using Branch.Common.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Common.Players
{
    internal class AutoFishPlayer : ModPlayer
    {
        /// <summary>
        /// 本模组调用
        /// </summary>
        internal bool modInvoke;

        internal Point mousePos;
        internal bool autoMode;

        /// <summary>
        /// 收杆延迟时间
        /// </summary>
        internal int pullWaitTimer;

        public override void Load()
        {
            On_Player.ItemCheck_CheckFishingBobbers += OnCheckFishingBobbers;
            On_Player.ItemCheck_Shoot += OnShoot;
        }

        public override void Unload()
        {
            On_Player.ItemCheck_CheckFishingBobbers -= OnCheckFishingBobbers;
            On_Player.ItemCheck_Shoot -= OnShoot;
        }

        public override void OnEnterWorld()
        {
            modInvoke = false;
            autoMode = false;
            pullWaitTimer = 0;
            mousePos = default;
        }

        //一秒60次
        public override void PreUpdate()
        {
            if (Player.whoAmI != Main.myPlayer) return;
            /**
             * 自动模式：自动抛竿，自动收杆
             */
            modInvoke = false;
            if (autoMode)
            {
                if (pullWaitTimer > 0 && --pullWaitTimer == 0)
                {
                    //自动收杆
                    Player.controlUseItem = true;
                    Player.releaseUseItem = true;
                    modInvoke = true;
                    //玩家检查物品，会触发ItemCheck的事件
                    Player.ItemCheck();
                }
                if (Player.HeldItem.fishingPole == 0)
                {
                    //玩家不是手持鱼竿，关闭自动钓鱼
                    autoMode = false;
                    return;
                }
                if (IsBobberActive(Player.whoAmI))
                {
                    //还有浮漂处于激活，即处于钓鱼状态不能抛竿
                    return;
                }
                Main.mouseX = mousePos.X - (int)Main.screenPosition.X;
                Main.mouseY = mousePos.Y - (int)Main.screenPosition.Y;
                Player.controlUseItem = true;
                Player.releaseUseItem = true;
                modInvoke = true;
                Player.ItemCheck();
            }
        }

        /// <summary>
        /// 判断钓鱼用的浮漂是否激活
        /// </summary>
        /// <param name="whoAmI"></param>
        /// <returns>true:激活</returns>
        private bool IsBobberActive(int whoAmI)
        {
            foreach (var p in Main.projectile)
            {
                if (p.owner == whoAmI && p.active && p.bobber)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnShoot(On_Player.orig_ItemCheck_Shoot orig, Player self, int i, Item sItem, int weaponDamage)
        {
            if (ClientConfig.Instance.AutoFish && self.whoAmI == Main.myPlayer && self.TryGetModPlayer(out AutoFishPlayer player) && !player.modInvoke && sItem.fishingPole > 0)
            {
                //既然它给你了player self就保险一点，获取钓鱼玩家
                //玩家手动抛竿，记录抛竿的位置,启动自动钓鱼
                player.mousePos = Main.MouseWorld.ToPoint();
                player.autoMode = true;
                player.pullWaitTimer = 0;//重置拉杆等待时间
            }
            orig.Invoke(self, i, sItem, weaponDamage);
        }

        private bool OnCheckFishingBobbers(On_Player.orig_ItemCheck_CheckFishingBobbers orig, Player self, bool canUse)
        {
            bool flag = orig.Invoke(self, canUse);//false:收杆
            if (!flag && ClientConfig.Instance.AutoFish && self.whoAmI == Main.myPlayer && self.TryGetModPlayer(out AutoFishPlayer player) && !player.modInvoke)
            {
                //玩家手动收杆，取消自动钓鱼
                player.autoMode = false;
            }
            return flag;
        }

        //成功咬钩
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            base.CatchFish(attempt, ref itemDrop, ref npcSpawn, ref sonar, ref sonarPosition);
            if (ClientConfig.Instance.AutoFish && pullWaitTimer == 0 && itemDrop >= 0)
            {
                if (!Player.sonarPotion)
                {
                    //玩家没有声呐药水，直接拉杆
                    pullWaitTimer = (int)(ClientConfig.Instance.PullWaitTimer * 60);
                    return;
                }
                if (ItemID.Sets.IsFishingCrate[itemDrop] && ClientConfig.Instance.CatchCrate)
                {
                    //宝箱
                    pullWaitTimer = (int)(ClientConfig.Instance.PullWaitTimer * 60);
                    return;
                }
                var item = new Item(itemDrop);
                if (item.accessory && ClientConfig.Instance.CatchAccessories)
                {
                    //饰品
                    pullWaitTimer = (int)(ClientConfig.Instance.PullWaitTimer * 60);
                    return;
                }
            }
        }
    }
}