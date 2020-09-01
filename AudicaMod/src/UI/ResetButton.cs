using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class ResetButton
{
    [HarmonyPatch(typeof(OptionsMenu), "ShowPage", new Type[] {typeof(OptionsMenu.Page) })]
    private static class AddResetbutton
    {
        private static void Postfix(OptionsMenu __instance, OptionsMenu.Page page)
        {
            if(page == OptionsMenu.Page.Calibration)
            {
                var button = __instance.AddButton(0, "Reset SongSettings", new Action(() => { SettingsManager.settings = new List<SavedData>(); }), null, "Resets all of the per song calibration changes you made in the pause menu");
                __instance.scrollable.AddRow(button.gameObject);
            }
        }
    }
}
