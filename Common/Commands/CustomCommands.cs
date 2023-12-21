using Branch.Common.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Common.Commands
{
    internal class CustomCommands : ModCommand
    {
        public override string Command => "clear";

        public override CommandType Type => CommandType.World;

        public override string Description => "width:宽度 height:高度";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length < 2)
            {
                throw new UsageException("至少需要两个参数");
            }
            if (int.TryParse(args[0], out int width) && int.TryParse(args[1], out int height))
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        TileUtils.KillTile(caller.Player, Player.tileTargetX + i, Player.tileTargetY - j);
                    }
                }
            }
        }
    }
}