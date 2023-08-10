using Lofelt.NiceVibrations;

namespace RoddGames.Uis
{
    public class BackButton : BaseButtonWithGameEvent
    {
        protected override void HandleOnButtonClicked()
        {
            base.HandleOnButtonClicked();
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
        }
    }
}