using MelonLoader;
using BTKUILib.UIObjects;
using UnityEngine;
using UnityEngine.XR;

namespace SVRCam
{
    public static class BuildInfo
    {
        public const string Name = "SVRCam";
        public const string Description = "Adds a full-resolution desktop mirror of your VR-view with motion smoothing, FOV controls and more!";
        public const string Author = "V-TOL";
        public const string Company = null;
        public const string Version = "0.1.5";
        public const string DownloadLink = "https://github.com/RadioArtz/V-TOLs-CVR-Mods";
    }

    public class SVRCamMod : MelonMod
    {
        private bool desktopCamEnabled = false;
        private Camera vrCamera;
        private Camera desktopCamera;
        private GameObject desktopCamContainer;
        private GameObject spawnCam;
        private float fov = 90;
        private float rotationSmoothness = 0.1f;
        private Quaternion targetRotation;
        private float offset = 0.1f;
        private float nearClip = 0.05f;
        public override void OnInitializeMelon()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            BTKUILib.QuickMenuAPI.PrepareIcon("SVRCam", "TempIcon", null);
            var settingsPage = new Page("SVRCam", "SVRCam Settings", true, null)
            {
                MenuTitle = "SVRCam",
                MenuSubtitle = "Adds a Full-Resolution Desktop View Camera!"
            };

            Category modSettings = settingsPage.AddCategory("Settings");

            modSettings.AddToggle("Enable Desktop Cam", "", desktopCamEnabled).OnValueUpdated += (state) =>
            {
                desktopCamEnabled = state;
                UpdateDesktopCam();
            };

            modSettings.AddSlider("FOV", "sets desktop FOV", fov, 30, 179).OnValueUpdated += (value) =>
            {
                fov = value;
                UpdateDesktopCam();
            };

            modSettings.AddSlider("Rotation Smoothness", "Controls the smoothness of camera rotation", rotationSmoothness, 0.0f, 1.0f).OnValueUpdated += (value) =>
            {
                rotationSmoothness = value;
            };

            modSettings.AddSlider("Camera forward offset", "Offset of the camera forwards to account for clipping with the head", offset, -0.3f, 0.3f).OnValueUpdated += (value) =>
            {
                offset = value;
            };

            modSettings.AddSlider("Near Clip Plane", "Adjust the Near Clip Plane", nearClip, 0.01f, 0.1f).OnValueUpdated += (value) =>
            {
                nearClip = value;
                UpdateDesktopCam();
            };
        }

        private void UpdateDesktopCam()
        {
            if (spawnCam.activeSelf)
                return;
            if (vrCamera == null)
                SetupDesktopCam();
            if (desktopCamera != null)
            {
                desktopCamera.enabled = desktopCamEnabled;
                XRSettings.showDeviceView = !desktopCamEnabled;
                desktopCamera.fieldOfView = fov;
                desktopCamera.nearClipPlane = nearClip;
            }
        }

        private void SetupDesktopCam()
        {
            vrCamera = GameObject.Find("_PLAYERLOCAL/[CameraRigVR]/[Offset] User PlaySpace/[Offset] Seated Play/Camera").GetComponent<Camera>();
            if (vrCamera == null)
                return;

            spawnCam = GameObject.Find("_PLAYERLOCAL/CameraSpawn/CVR Camera 2.0");
            if (spawnCam == null)
                return;

            if (desktopCamera == null)
            {
                desktopCamContainer = new GameObject("SmoothCam");
                desktopCamContainer.transform.SetParent(GameObject.Find("_PLAYERLOCAL/[CameraRigVR]/[Offset] User PlaySpace/[Offset] Seated Play").transform, false);
                desktopCamera = desktopCamContainer.AddComponent<Camera>();
            }

            desktopCamera.targetDisplay = 0;
            desktopCamera.stereoTargetEye = StereoTargetEyeMask.None;
            desktopCamera.fieldOfView = fov;
            desktopCamera.aspect = 16f / 9;
            desktopCamera.nearClipPlane = nearClip;
            desktopCamera.depth = 0;
            desktopCamera.renderingPath = RenderingPath.Forward;
            desktopCamContainer.tag = "MainCamera";
            desktopCamContainer.layer = vrCamera.gameObject.layer;

            targetRotation = vrCamera.transform.rotation;

            vrCamera.stereoTargetEye = StereoTargetEyeMask.Both;
            vrCamera.depth = 1;
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();
            if (desktopCamContainer == null) 
                return;
            if (spawnCam.activeSelf && desktopCamEnabled)
            {
                desktopCamEnabled = false;
                UpdateDesktopCam();
                MelonLogger.Warning("Ingame camera has been enabled. Turning off SmoothCam mirroring. Yell at the mod author to implement this to only happen when ingame cam mirroring is enabled!");
            }

            if (desktopCamEnabled && desktopCamera != null && vrCamera != null)
            {
                targetRotation = vrCamera.transform.rotation;
                desktopCamContainer.transform.position = vrCamera.transform.position + vrCamera.transform.forward * offset;
                desktopCamContainer.transform.rotation = Quaternion.Slerp(targetRotation, desktopCamContainer.transform.rotation, (1 - Mathf.Pow(1 - rotationSmoothness,2f)) * 0.975f);
            }
        }
    }
}