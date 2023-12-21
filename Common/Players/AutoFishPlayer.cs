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
        private int waitTime = 60;

        /// <summary>
        /// 拉杆
        /// fishingPlole:钓鱼竿
        /// </summary>
        private bool pullFishingPole;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return base.IsLoadingEnabled(mod);
        }

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

        public override void PreUpdate()
        {
            if (Player.whoAmI != Main.myPlayer) return;
            if (pullFishingPole)
            {
                waitTime--;
                if (waitTime == 0)
                {
                    //自动收杆
                    Player.controlUseItem = true;
                    Player.releaseUseItem = true;
                    modInvoke = true;
                    Player.ItemCheck();
                    pullFishingPole = false;
                    waitTime = 60;
                    //10帧后再抛竿
                    if (Player.HeldItem.fishingPole <= 0)
                    {
                        //玩家不是手持鱼竿，关闭自动钓鱼
                        autoMode = false;
                        return;
                    }
                    if (IsBobberActive(Player.whoAmI))
                    {
                        //还有浮漂处于激活，不能抛竿
                        return;
                    }
                    Player.controlUseItem = true;
                    Player.releaseUseItem = true;
                    Player.ItemCheck();
                }
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
            if (self.whoAmI == Main.myPlayer && self.TryGetModPlayer(out AutoFishPlayer player) && !player.modInvoke && sItem.fishingPole > 0)
            {
                //既然它给你了player self就保险一点，获取钓鱼玩家
                //玩家手动抛竿，记录抛竿的位置,启动自动钓鱼
                player.mousePos = Main.MouseWorld.ToPoint();
                player.autoMode = true;
            }
            orig.Invoke(self, i, sItem, weaponDamage);
        }

        private bool OnCheckFishingBobbers(On_Player.orig_ItemCheck_CheckFishingBobbers orig, Player self, bool canUse)
        {
            bool flag = orig.Invoke(self, canUse);//false:收杆
            if (!flag && self.whoAmI == Main.myPlayer && self.TryGetModPlayer(out AutoFishPlayer player) && !player.modInvoke)
            {
                //玩家手动收杆，取消自动钓鱼
                player.autoMode = false;
            }
            return flag;
        }

        //成功咬钩
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (ItemID.Sets.IsFishingCrate[itemDrop])
            {
                pullFishingPole = true;
            }
            base.CatchFish(attempt, ref itemDrop, ref npcSpawn, ref sonar, ref sonarPosition);
        }
    }
}