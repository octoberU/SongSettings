using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal static class InGameHooks
{
    //public static bool needRestart;
    [HarmonyPatch(typeof(InGameUI), "Restart", new Type[0])]
    private static class Restart
    {
        private static void Postfix(InGameUI __instance)
        {
            ApplySettingsToSong();
        }
    }

    [HarmonyPatch(typeof(InGameUI), "ReturnToSongList", new Type[0])]
    private static class Return
    {
        private static void Postfix(InGameUI __instance)
        {
            RevertToOriginal();
        }
    }

    [HarmonyPatch(typeof(LaunchPanel), "Play", new Type[0])]
    private static class Play
    {
        private static void Postfix(InGameUI __instance)
        {
            ApplySettingsToSong();
        }
    }

    public static void ApplySettingsToSong()
    {
        var saveData = SettingsManager.GetSavedData(SongDataHolder.I.songData.songID);
        if (saveData == null)
        {
            RevertToOriginal();
        }
        else
        {
            PlayerPreferences.I.TargetSpeedMultiplier.Set(OriginalSettings.targetSpeed * saveData.targetSpeed);
            PlayerPreferences.I.InputOffsetMs.Set(OriginalSettings.inputOffset + (float)saveData.inputOffset);
        }
    }

    public static void RevertToOriginal()
    {
        PlayerPreferences.I.TargetSpeedMultiplier.Set(OriginalSettings.targetSpeed);
        PlayerPreferences.I.InputOffsetMs.Set(OriginalSettings.inputOffset);
    }
}
