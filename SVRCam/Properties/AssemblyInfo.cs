using System.Reflection;
using MelonLoader;
using SVRCam;

[assembly: AssemblyTitle(SVRCam.BuildInfo.Description)]
[assembly: AssemblyDescription(SVRCam.BuildInfo.Description)]
[assembly: AssemblyCompany(SVRCam.BuildInfo.Company)]
[assembly: AssemblyProduct(SVRCam.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + SVRCam.BuildInfo.Author)]
[assembly: AssemblyTrademark(SVRCam.BuildInfo.Company)]
[assembly: AssemblyVersion(SVRCam.BuildInfo.Version)]
[assembly: AssemblyFileVersion(SVRCam.BuildInfo.Version)]
[assembly: MelonInfo(typeof(SVRCamMod), SVRCam.BuildInfo.Name, SVRCam.BuildInfo.Version, SVRCam.BuildInfo.Author, SVRCam.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]