using Branch.Common.Utils;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Common.Commands
{
    /// <summary>
    /// 清除方块指令
    /// </summary>
    internal class ClearCommand : ModCommand
    {
        public override string Command => "clear";

        public override CommandType Type => CommandType.World;

        public override string Description => "width:宽度 height:高度 (右和上为正方向)";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length < 2)
            {
                throw new UsageException("至少需要两个参数");
            }
            if (int.TryParse(args[0], out int width) && int.TryParse(args[1], out int height) && width != 0 && height != 0)
            {
                int flagX = width < 0 ? -1 : 1;
                int flagY = height < 0 ? -1 : 1;
                for (int j = 0; j < Math.Abs(height); j++)
                {
                    for (int i = 0; i < Math.Abs(width); i++)
                    {
                        TileUtils.KillTile(caller.Player, Player.tileTargetX + i * flagX, Player.tileTargetY - j * flagY);
                    }
                }
            }
            else
            {
                throw new UsageException("参数不合法");
            }
        }
    }
}