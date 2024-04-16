using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Header("Load Data")]
    public int highestChapter;
    public int highestLevel;
    [Header("Player Data")]
    public int selectedCharacter;
    public int health;
    [Header("Main Menu")]
    public SettingsData settingsData;

    public void UpdateData(GameData data) 
    {
        highestChapter = (int)data.highestChapterCompleted;
        highestLevel = (int)data.highestLevelCompleted;

        selectedCharacter = (int)data.selectedCharacter;
        health = (int)data.health;

        SettingsData settings = new SettingsData();
        settings.masterVolume = (int)data.masterVolume;
        settings.musicVolume = (int)data.musicVolume;
        settings.sfxVolume = (int)data.sfxVolume;
        settings.enemyVolume = (int)data.enemyVolume;

        this.settingsData = settings;

        Save();
    }

    void SyncData(GameData data) 
    {
        data.highestChapterCompleted = (MythologyMayhem.Chapter)highestChapter;
        data.highestLevelCompleted = (MythologyMayhem.Level)highestLevel;

        data.selectedCharacter = (MythologyMayhem.Character)selectedCharacter;
        data.health = (int)health;

        data.masterVolume = (int)settingsData.masterVolume;
        data.musicVolume = (int)settingsData.musicVolume;
        data.sfxVolume = (int)settingsData.sfxVolume;
        data.masterVolume = (int)settingsData.enemyVolume;
    }

    void LoadData(SaveData data) 
    {
        this.highestChapter = data.highestChapter;
        this.highestLevel = data.highestLevel;

        this.selectedCharacter = data.selectedCharacter;
        this.health = data.health;


        SettingsData settings = new SettingsData();
        settings = data.settingsData;

        this.settingsData = settings;

        SyncData(GameManager.instance.gameData);
    }

    public void PushData() 
    {
        if (GameManager.instance != null) 
        {
            GameData data = GameManager.instance.gameData;
            SyncData(data);
        }
    }

    void Save()
    {
        string sceneObjectsString = JsonUtility.ToJson(this, true);

        System.IO.File.WriteAllText(Application.dataPath + "/Global Assets/Resources/SceneData/" + "TestSaveSystem" + ".json", sceneObjectsString);

        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
        #endif


    }
    public void Load()
    {
        if (!System.IO.File.Exists(Application.dataPath + "/Global Assets/Resources/SceneData/" + "TestSaveSystem" + ".json"))
        {
            //this.Save();
            return;
        }
        string sceneObjectsString = System.IO.File.ReadAllText(Application.dataPath + "/Global Assets/Resources/SceneData/" + "TestSaveSystem" + ".json");

        SaveData loadedData = JsonUtility.FromJson<SaveData>(sceneObjectsString);
        LoadData(loadedData);
        GameManager.instance.gameData.loaded = true;
    }

    public void GenerateNewData() 
    {
        SettingsData newSettings = new SettingsData();
        this.settingsData = newSettings;
    }
}

[System.Serializable]
public class SettingsData 
{
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float enemyVolume;
}
