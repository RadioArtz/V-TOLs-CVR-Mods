using System.Reflection;
using MelonLoader;
using SlimeCVR;

[assembly: AssemblyTitle(SlimeCVR.BuildInfo.Description)]
[assembly: AssemblyDescription(SlimeCVR.BuildInfo.Description)]
[assembly: AssemblyCompany(SlimeCVR.BuildInfo.Company)]
[assembly: AssemblyProduct(SlimeCVR.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + SlimeCVR.BuildInfo.Author)]
[assembly: AssemblyTrademark(SlimeCVR.BuildInfo.Company)]
[assembly: AssemblyVersion(SlimeCVR.BuildInfo.Version)]
[assembly: AssemblyFileVersion(SlimeCVR.BuildInfo.Version)]
[assembly: MelonInfo(typeof(SlimeCVR.SlimeMod), SlimeCVR.BuildInfo.Name, SlimeCVR.BuildInfo.Version, SlimeCVR.BuildInfo.Author, SlimeCVR.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]