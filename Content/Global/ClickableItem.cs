using Branch.Common.Extensions;
using Branch.Common.Interface;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Branch.Content.Global
{
    internal class ClickableItem : GlobalItem, IItemClickable
    {
        public ClickType CanClickable(Item item, int index)
        {
            if (item.DamageType == DamageClass.Summon)
            {
                return ClickType.Middle;
            }
            return ClickType.None;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        public void OnMiddleClicked(Item item, int index)
        {
            float perSlot = ItemID.Sets.StaffMinionSlotsRequired[item.type];
            int count = (int)(Main.LocalPlayer.maxMinions / perSlot);
            SoundEngine.PlaySound(item.UseSound, Main.LocalPlayer.Center);
            for (int i = 0; i < count; i++)
            {
                Main.LocalPlayer.ModPlayer().Summon(item);
            }
        }
    }
}