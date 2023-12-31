﻿using Branch.Common.Extensions;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Common.UI
{
    [Autoload(Side = ModSide.Client)]
    internal class UISystem : ModSystem
    {
        internal static UISystem Instance { get; private set; }
        public WeatherReportUI weatherReport;
        private UserInterface weatherReportInterface;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void Load()
        {
            Instance = this;
            if (Main.dedServ) return;
            // SetState() 会执行 Activate()
            weatherReport = new WeatherReportUI();
            weatherReportInterface = new UserInterface();
            weatherReportInterface.SetState(weatherReport);
        }

        public override void Unload()
        {
            Instance = null;
            weatherReport = null;
            weatherReportInterface = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible) return;
            if (WeatherReportUI.Visible)
            {
                weatherReportInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            // 精密线控仪
            layers.FindVanilla("Mouse Text", index =>
            {
                layers.Insert(index, "Weather Report", weatherReport, () => WeatherReportUI.Visible);
            });
        }
    }
}