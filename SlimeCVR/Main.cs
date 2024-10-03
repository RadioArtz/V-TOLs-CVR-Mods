using MelonLoader;
using BTKUILib.UIObjects;
using WindowsInput;
using WindowsInput.Native;

namespace SlimeCVR
{
    public static class BuildInfo
    {
        public const string Name = "SlimeCVR";
        public const string Description = "A mod that allows resetting & pausing slimevr tracking from your quickmenu!";
        public const string Author = "V-TOL";
        public const string Company = null;
        public const string Version = "0.5.0";
        public const string DownloadLink = null;
    }

    public class SlimeMod : MelonMod
    {
        InputSimulator inputSim = new InputSimulator();
        public override void OnInitializeMelon()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            BTKUILib.QuickMenuAPI.PrepareIcon("SlimeCVR", "TempIcon", null);

            var settingsPage = new Page("SlimeCVR", "SlimeVR utils", true, null)
            {
                MenuTitle = "SlimeVR Utilities",
                MenuSubtitle = "Allows resetting & pausing your slimevr tracking!"
            };

            Category trackingSettings = settingsPage.AddCategory("Tracking");
            trackingSettings.AddButton("Yaw Reset", null, null).OnPress += () => { SendKeybind(EKeybind.yawReset); };
            trackingSettings.AddButton("Full Reset", null, null).OnPress += () => { SendKeybind(EKeybind.fullReset); };
            trackingSettings.AddButton("Pause Tracking", null, null).OnPress += () => { SendKeybind(EKeybind.pauseTracking); };
            trackingSettings.AddButton("Mounting Reset", null, null).OnPress += () => { SendKeybind(EKeybind.mountingReset); };
        }

        private void SendKeybind(EKeybind bind)
        {
            switch (bind)
            {
                case EKeybind.yawReset:
                    inputSim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.MENU, VirtualKeyCode.SHIFT }, VirtualKeyCode.VK_U);
                    break;
                case EKeybind.fullReset:
                    inputSim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.MENU, VirtualKeyCode.SHIFT }, VirtualKeyCode.VK_Y);
                    break;
                case EKeybind.pauseTracking:
                    inputSim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.MENU, VirtualKeyCode.SHIFT }, VirtualKeyCode.VK_O);
                    break;
                case EKeybind.mountingReset:
                    inputSim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.MENU, VirtualKeyCode.SHIFT }, VirtualKeyCode.VK_I);
                    break;
            }
        }

        private enum EKeybind
        {
            yawReset = 0,
            fullReset = 1,
            pauseTracking = 2,
            mountingReset = 3
        }
    }
}