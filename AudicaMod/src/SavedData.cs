using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using System.IO;
using MelonLoader.TinyJSON;

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
}

internal static class SettingsManager
{
    public static HashSet<SavedData> settings;
    public static string settingsPath = Application.dataPath + "/../" + "/UserData/" + "SongSettings.json";
    public static void ChangeInputOffset(string songID, int newOffset)
    {
        var savedData = GetSavedData(songID);
        if (savedData.Equals(default(SavedData)))
        {
            var newEntry = new SavedData(songID);
            newEntry.inputOffset = newOffset;
            settings.Add(newEntry);
        }
        else
        {
            savedData.inputOffset = newOffset;
        }
    }

    public static void ChangeTargetSpeed(string songID, float targetSpeed)
    {
        var savedData = GetSavedData(songID);
        if (savedData.Equals(default(SavedData)))
        {
            var newEntry = new SavedData(songID);
            newEntry.targetSpeed = targetSpeed;
            settings.Add(newEntry);
        }
        else
        {
            savedData.targetSpeed = targetSpeed;
        }
    }

    public static void LoadSettings()
    {
        if (File.Exists(settingsPath))
        {
            string text = File.ReadAllText(settingsPath);
            settings = JSON.Load(text).Make<HashSet<SavedData>>();
        }
        else
        {
            settings = new HashSet<SavedData>();
        }
    }

    public static void SaveSettings()
    {
        string text = JSON.Dump(settings);
        File.WriteAllText(settingsPath, text);
    }

    public static SavedData GetSavedData(string songID)
    {
        return settings.FirstOrDefault(data => data.songID == songID);
    }
}