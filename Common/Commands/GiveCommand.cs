using Terraria.ModLoader;

namespace Branch.Common.Commands
{
    internal class GiveCommand : ModCommand
    {
        public override string Command => "give";

        public override CommandType Type => CommandType.Chat;

        public override string Description => "物品ID";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            //if (args.Length < 1) return;
            //if (int.TryParse(args[0], out int type))
            //{
            //    ModUtils.GiveItem(caller.Player, type);
            //}
        }
    }
}