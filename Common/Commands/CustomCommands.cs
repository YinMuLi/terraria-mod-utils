using Branch.Common.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
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
            int y = Player.tileTargetY;
            SoundEngine.PlaySound(SoundID.Item14, new(Player.tileTargetX, Player.tileTargetY));
            for (int j = 0; j < 30; j++)
            {
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    TileUtils.KillTile(caller.Player, i, y - j);
                }
            }
        }
    }
}