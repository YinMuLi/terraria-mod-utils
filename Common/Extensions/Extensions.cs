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

        public static void Insert(this List<GameInterfaceLayer> layers, int index, string name, UIState state,
            Func<bool> func)
        {
            // 如果 Insert 是按照向前插入的逻辑，越早插入越晚绘制，也就是越靠近顶层。
            //这里的写法是在index图层之下
            layers.Insert(index, new LegacyGameInterfaceLayer($"Branch: {name}", () =>
            {
                if (func())
                {
                    state.Draw(Main.spriteBatch);
                }

                return true;
            }, InterfaceScaleType.UI));
        }

        //public static void Insert(this List<GameInterfaceLayer> layers, int index, string name,
        //    GameInterfaceDrawMethod func)
        //{
        //    layers.Insert(index + 1, new LegacyGameInterfaceLayer($"Branch: {name}", func, InterfaceScaleType.UI));
        //}

        public static void FindVanilla(this List<GameInterfaceLayer> layers, string name, Action<int> action)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals($"Vanilla: {name}"));
            if (index != -1)
            {
                action(index);
            }
        }

        #endregion UI
    }
}