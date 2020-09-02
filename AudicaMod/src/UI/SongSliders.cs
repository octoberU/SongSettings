
using AudicaModding;
using Harmony;
using Hmx.Audio;
using MelonLoader;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

internal static class SongSliders
{
    public static GameObject offsetSlider;
    public static GunButton offsetSliderLeft;
    public static GunButton offsetSliderRight;
    public static TextMeshPro offsetSliderLabel;

    public static GameObject targetSpeedSlider;
    public static GunButton targetSpeedSliderLeft;
    public static GunButton targetSpeedSliderRight;
    public static TextMeshPro targetSpeedSliderLabel;

    public static void Init(InGameUI inGameUI)
    {
        var templateSlider = inGameUI.transform.GetChild(15).GetChild(0).GetChild(6).gameObject;
        
        offsetSlider = GameObject.Instantiate(templateSlider);
        GameObject.Destroy(offsetSlider.GetComponentInChildren<Localizer>());
        offsetSliderLeft = offsetSlider.transform.GetChild(3).GetComponentInChildren<GunButton>();
        offsetSliderRight = offsetSlider.transform.GetChild(4).GetComponentInChildren<GunButton>();
        offsetSliderLeft.onHitEvent = new UnityEvent(); offsetSliderRight.onHitEvent = new UnityEvent();

        offsetSliderLeft.onHitEvent.AddListener(new Action(() => { SettingsManager.ChangeInputOffset(SongDataHolder.I.songData.songID, -1); UpdateLabels(); }));
        offsetSliderRight.onHitEvent.AddListener(new Action(() => { SettingsManager.ChangeInputOffset(SongDataHolder.I.songData.songID, 1); UpdateLabels(); }));

        offsetSliderLabel = offsetSlider.transform.GetChild(0).GetComponent<TextMeshPro>();
        offsetSliderLabel.text = "Input Offset";
        offsetSlider.transform.position = new Vector3(-1.817f, -4.18f, 13.982f);
        offsetSlider.transform.localScale = new Vector3(0.4964773f, 0.4964773f, 0.4964773f);
        offsetSlider.transform.rotation = Quaternion.EulerAngles(Vector3.zero);

        targetSpeedSlider = GameObject.Instantiate(templateSlider);
        GameObject.Destroy(targetSpeedSlider.GetComponentInChildren<Localizer>());
        targetSpeedSliderLeft = targetSpeedSlider.transform.GetChild(3).GetComponentInChildren<GunButton>();
        targetSpeedSliderRight = targetSpeedSlider.transform.GetChild(4).GetComponentInChildren<GunButton>();
        targetSpeedSliderLeft.onHitEvent = new UnityEvent(); targetSpeedSliderRight.onHitEvent = new UnityEvent();

        targetSpeedSliderLeft.onHitEvent.AddListener(new Action(() => { SettingsManager.ChangeTargetSpeed(SongDataHolder.I.songData.songID, -0.1f); UpdateLabels(); }));
        targetSpeedSliderRight.onHitEvent.AddListener(new Action(() => { SettingsManager.ChangeTargetSpeed(SongDataHolder.I.songData.songID, 0.1f); UpdateLabels(); }));

        targetSpeedSliderLabel = targetSpeedSlider.transform.GetChild(0).GetComponent<TextMeshPro>();
        targetSpeedSliderLabel.text = "Target Speed";
        targetSpeedSlider.transform.position = new Vector3(1.817f, -4.18f, 13.982f);
        targetSpeedSlider.transform.localScale = new Vector3(0.4964773f, 0.4964773f, 0.4964773f);
        targetSpeedSlider.transform.rotation = Quaternion.EulerAngles(Vector3.zero);
        
        UpdateLabels();
    }

    public static void UpdateLabels()
    {
        string songID = SongDataHolder.I.songData.songID;
        SavedData savedData = SettingsManager.GetSavedData(songID);
        MelonLogger.Log("got saved data");
        if (savedData == null)
        {
            MelonLogger.Log("saved data null");
            targetSpeedSliderLabel.text = "Target Speed: 100%";
            offsetSliderLabel.text = "Input Offset: 0ms";
        }
        else
        {
            MelonLogger.Log("saved data found");
            targetSpeedSliderLabel.text = $"Target Speed: {savedData.targetSpeed.ToString("P")}";
            offsetSliderLabel.text = $"Input Offset: {savedData.inputOffset.ToString()}ms";
        }
            
    }

    [HarmonyPatch(typeof(InGameUI), "SetState", new Type[] { typeof(InGameUI.State), typeof(bool) })]
    private static class ShowSliders
    {
        private static void Prefix(InGameUI __instance, InGameUI.State state, bool instant = false)
        {
            if (state == InGameUI.State.PausePage || state == InGameUI.State.EndGameContinuePage) Show(__instance);
            else Hide();
            if (SettingsManager.needSaving) SettingsManager.SaveSettings();
        }
    }

    [HarmonyPatch(typeof(InGameUI), "ReturnToSongList", new Type[0])]
    private static class HideSliders
    {
        private static void Prefix(InGameUI __instance)
        {
            Hide();
            if (SettingsManager.needSaving) SettingsManager.SaveSettings();
        }
    }

    public static void Hide()
    {
        if (offsetSlider != null)
        {
            offsetSlider.SetActive(false);
            targetSpeedSlider.SetActive(false);
        }
    }

    public static void Show(InGameUI inGameUI)
    {
        if (offsetSlider != null)
        {
            offsetSlider.SetActive(true);
            targetSpeedSlider.SetActive(true);
            UpdateLabels();
        }
        else
        {
            Init(inGameUI);
            offsetSlider.SetActive(true);
            targetSpeedSlider.SetActive(true);
        }
    }

}
