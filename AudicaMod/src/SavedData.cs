using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[Serializable]
class SavedData
{
    public string songID;
    public int inputOffset;
    public float targetSpeed;

    public SavedData(string songID)
    {
        this.songID = songID;
        this.inputOffset = 0;
        this.targetSpeed = 1f;
    }

    [JsonConstructor]
    public SavedData(string songID, int inputOffset, float targetSpeed)
    {
        this.songID = songID;
        this.inputOffset = inputOffset;
        this.targetSpeed = targetSpeed;
    }
}


internal static class SettingsManager
{
    public static List<SavedData> settings;
    public static string settingsPath = Application.dataPath + "/../" + "/UserData/" + "SongSettings.json";
    public static bool needSaving = false;
    public static void ChangeInputOffset(string songID, int inputOffset)
    {
        var savedData = GetSavedData(songID);
        if (savedData == null)
        {
            var newEntry = new SavedData(songID);
            newEntry.inputOffset += inputOffset;
            settings.Add(newEntry);
        }
        else
        {
            savedData.inputOffset += inputOffset;
            if (savedData.inputOffset < -50) savedData.inputOffset = -50;
            if (savedData.inputOffset > 50) savedData.inputOffset = 50;
        }
        needSaving = true;
    }

    public static void ChangeTargetSpeed(string songID, float targetSpeed)
    {
        var savedData = GetSavedData(songID);
        if (savedData == null)
        {
            var newEntry = new SavedData(songID);
            newEntry.targetSpeed += targetSpeed;
            settings.Add(newEntry);
        }
        else
        {
            savedData.targetSpeed += targetSpeed;
            if (savedData.targetSpeed < 0.5f) savedData.targetSpeed = 0.5f;
            if (savedData.targetSpeed > 2f) savedData.targetSpeed = 2f;
        }
        needSaving = true;
    }

    public static void LoadSettings()
    {
        if (File.Exists(settingsPath))
        {
            string text = File.ReadAllText(settingsPath);
            settings = JsonConvert.DeserializeObject<List<SavedData>>(text);
        }
        else
        {
            settings = new List<SavedData>();
        }
    }

    public static void SaveSettings()
    {
        string text = JsonConvert.SerializeObject(settings);
        File.WriteAllText(settingsPath, text);
    }

    public static SavedData GetSavedData(string songID)
    {
        return settings.FirstOrDefault(data => data.songID == songID);
    }
}