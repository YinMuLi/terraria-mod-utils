using Branch.Common.Utils;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Branch.Common.Commands
{
    internal class CustomCommands : ModCommand
    {
        public override string Command => "line";

        public override CommandType Type => CommandType.World;

        public override string Description => "快速创建平台";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (caller.Player.whoAmI == Main.myPlayer)
            {
                ModUtils.ShowText($"{caller.Player.whoAmI}");
            }
        }
    }
}