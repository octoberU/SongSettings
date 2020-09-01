using Harmony;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class OriginalSettings
{
    public static float inputOffset;
    public static float targetSpeed;

    [HarmonyPatch(typeof(PlayerPreferences), "Start", new Type[0])]
    private static class GetInitialOffsets
    {
        private static void Postfix(PlayerPreferences __instance)
        {
            inputOffset = __instance.InputOffsetMs.mVal;
            targetSpeed = __instance.TargetSpeedMultiplier.mVal;
            MelonLogger.Log($"Offset: {inputOffset}, Target Speed:{targetSpeed}");
        }
    }

    [HarmonyPatch(typeof(PlayerPreferences.FloatPref), "Adjust", new Type[] {typeof(float) })]
    private static class UpdateOffsets
    {
        private static void Postfix(PlayerPreferences.FloatPref __instance)
        {
            inputOffset = PlayerPreferences.I.InputOffsetMs.mVal;
            targetSpeed = PlayerPreferences.I.TargetSpeedMultiplier.mVal;
            MelonLogger.Log($"Offset: {inputOffset}, Target Speed:{targetSpeed}");
        }
    }
}