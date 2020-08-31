using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;
using AudicaModding;

[assembly: AssemblyTitle(SongSettings.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(SongSettings.BuildInfo.Company)]
[assembly: AssemblyProduct(SongSettings.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + SongSettings.BuildInfo.Author)]
[assembly: AssemblyTrademark(SongSettings.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(SongSettings.BuildInfo.Version)]
[assembly: AssemblyFileVersion(SongSettings.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonModInfo(typeof(SongSettings), SongSettings.BuildInfo.Name, SongSettings.BuildInfo.Version, SongSettings.BuildInfo.Author, SongSettings.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonModGame(null, null)]