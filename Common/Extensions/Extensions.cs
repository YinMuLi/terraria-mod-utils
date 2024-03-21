using YinMu.Common.Players;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.UI;

namespace YinMu.Common.Extensions
{
    public static class Extensions
    {
        #region UI

        /// <summary>
        /// 需要判断条件
        /// </summary>
        public static void Insert(this List<GameInterfaceLayer> layers, int index, string name, UIState state,
            Func<bool> func)
        {
            // 如果 Insert 是按照向前插入的逻辑，越早插入越晚绘制，也就是越靠近顶层。
            //这里的写法是在index图层之下
            layers.Insert(index, new LegacyGameInterfaceLayer($"YinMu: {name}", () =>
            {
                //绘制UI层的条件
                if (func())
                {
                    state.Draw(Main.spriteBatch);
                }

                return true;
            }, InterfaceScaleType.UI));
        }

        /// <summary>
        /// 需要判断条件
        /// </summary>
        public static void Insert(this List<GameInterfaceLayer> layers, int index, UIState state,
            Func<bool> func)
        {
            // 如果 Insert 是按照向前插入的逻辑，越早插入越晚绘制，也就是越靠近顶层。
            //这里的写法是在index图层之下
            layers.Insert(index, new LegacyGameInterfaceLayer($"YinMu: {state.GetType().Name}", () =>
            {
                //绘制UI层的条件
                if (func())
                {
                    state.Draw(Main.spriteBatch);
                }

                return true;
            }, InterfaceScaleType.UI));
        }

        /// <summary>
        /// 不需要判断条件
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="state"></param>
        public static void Insert(this List<GameInterfaceLayer> layers, int index, string name, UIState state)
        {
            layers.Insert(index, new LegacyGameInterfaceLayer($"YinMu: {name}",
                () =>
                {
                    state.Draw(Main.spriteBatch);
                    return true;
                }
                , InterfaceScaleType.UI));
        }

        public static void DrawLayer(this List<GameInterfaceLayer> layers, string name, Action<int> action)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals($"Vanilla: {name}"));
            if (index != -1)
            {
                action(index);
            }
        }

        #endregion UI

        public static YinMuPlayer ModPlayer(this Player player) => player.GetModPlayer<YinMuPlayer>();

        /// <summary>
        /// 简便添加一个物品的掉落
        /// </summary>
        /// <param name="loot"></param>
        /// <param name="itemID">掉落的物品</param>
        /// <param name="dropRateInt">掉落概率（1/xx分母）</param>
        /// <param name="minQuantity">最小掉落数量,默认是1</param>
        /// <param name="maxQuantity">最大掉落数量，默认是1</param>
        /// <returns></returns>
        public static IItemDropRule Add(this ILoot loot, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1)
        {
            return loot.Add(ItemDropRule.Common(itemID, dropRateInt, minQuantity, maxQuantity));
        }

        /// <summary>
        /// 物品的条件掉落
        /// </summary>
        /// <param name="loot"></param>
        /// <param name="func">掉落规则，条件默认是显示UI，条件描述为空</param>
        /// <param name="itemID"></param>
        /// <param name="dropRateInt"></param>
        /// <param name="minQuantity"></param>
        /// <param name="maxQuantity"></param>
        /// <returns></returns>
        public static IItemDropRule AddIf(this ILoot loot, Func<DropAttemptInfo, bool> func, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1)
        {
            return loot.Add(ItemDropRule.ByCondition(new SimpleDropCondition(func, true, null), itemID, dropRateInt, minQuantity, maxQuantity));
        }
    }

    public class SimpleDropCondition : IItemDropRuleCondition
    {
        private readonly Func<DropAttemptInfo, bool> condition;
        private readonly bool showUI;
        private readonly string description;

        public SimpleDropCondition(Func<DropAttemptInfo, bool> condition, bool showUI, string description)
        {
            this.condition = condition;
            this.showUI = showUI;
            this.description = description;
        }

        public bool CanDrop(DropAttemptInfo info) => condition(info);

        public bool CanShowItemDropInUI() => showUI;

        public string GetConditionDescription() => description;
    }
}