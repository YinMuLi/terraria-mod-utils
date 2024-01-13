using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Branch.Common.UI.System
{
    [Autoload(Side = ModSide.Client)]
    internal class UISystem : ModSystem
    {
        internal static UISystem Instance { get; private set; }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void Load()
        {
            Instance = this;
            if (Main.dedServ) return;
        }

        private static void LoadUI<T>(ref T state, out UserInterface userInterface) where T : UIState
        {
            userInterface = new();
            // SetState() 会执行 Activate()
            userInterface.SetState(state);
        }

        public override void Unload()
        {
            Instance = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible) return;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
        }
    }
}