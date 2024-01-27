using Terraria.ModLoader;

namespace Branch.Common.System
{
    internal class KeybindSystem : ModSystem
    {
        public static ModKeybind TeleportDeathPoint { get; private set; }

        public override void Load()
        {
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to English users is in en-US.hjson
            TeleportDeathPoint = KeybindLoader.RegisterKeybind(Mod, "TeleportDeathPoint", "NumPad9");
        }

        public override void Unload()
        {
            TeleportDeathPoint = null;
        }
    }
}