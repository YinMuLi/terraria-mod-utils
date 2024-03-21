using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YinMu.Common.Commands
{
    internal class InfoCommand : ModCommand
    {
        public override string Command => "info";

        public override CommandType Type => CommandType.Chat;

        public override string Description => "显示基本信息";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText(Language.GetTextValue("Mods.Misc.Info").FormatWith(caller.Player.name, caller.Player.numberOfDeathsPVE), Color.Green);
        }
    }
}