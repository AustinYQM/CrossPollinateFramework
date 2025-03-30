using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace CrossPollinateFramework
{
    internal sealed class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
             
        }
    }
}

