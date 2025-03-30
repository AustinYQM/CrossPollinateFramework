using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

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
            if (!Context.IsWorldReady)
            {
                return;
            }
            this.Monitor.Log($"{Game1.player.name} pressed {e.Button} in {Game1.currentLocation.Name}", LogLevel.Debug);
        }
    }
}

