using Branch.Common.Players;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;

namespace Branch.Common.Extensions
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
            layers.Insert(index, new LegacyGameInterfaceLayer($"Branch: {name}", () =>
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
            layers.Insert(index, new LegacyGameInterfaceLayer($"Branch: {state.GetType().Name}", () =>
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
            layers.Insert(index, new LegacyGameInterfaceLayer($"Branch: {name}",
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

        public static BranchPlayer ModPlayer(this Player player) => player.GetModPlayer<BranchPlayer>();
    }
}