using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Common.UI
{
    internal class UISystem : ModSystem
    {
        internal static UISystem Instance { get; private set; }
        public WeatherReportUI weatherReport;
        private UserInterface weatherReportInterface;

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
            if (WeatherReportUI.visible)
            {
                weatherReportInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            // 精密线控仪
            int insertIndex = layers.FindIndex(index => index.Name.Equals("Vanilla: Wire Selection"));
            if (insertIndex != -1)
            {
                layers.Insert(insertIndex, new LegacyGameInterfaceLayer("Branch: Weather Report", () =>
                {
                    if (WeatherReportUI.visible)
                    {
                        weatherReport.Draw(Main.spriteBatch);
                    }
                    return true;
                }, InterfaceScaleType.UI));
            }
        }
    }
}