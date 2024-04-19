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
    public PlayerData playerData;
    [Header("Main Menu")]
    public SettingsData settingsData;
    [Header("Level Data")]
    public GreekLibrary_2DData GL2D;
    public GreekLibrary_3DData GL3D;
    public GreekAthens_2DData GA2D;
    public GreekCavern_2DData GC2D;
    public GreekCavern_2D_LeversData GC2DL;
    public GreekCavern_2D_StatuesData GC2DS;
    public GreekLabyrinth_3DData GLa3D;
    public GreekLabyrinth_2D_LeversData GLa2DL;
    public GreekLabyrinth_2D_PedastalsData GLa2DP;
    public GreekMedusa_3DData GM3D;
    public VikingVillage_2DData VV2D;
    public VikingVillage_3DData VV3D;

    public void UpdateData(GameData data) 
    {
        highestChapter = (int)data.highestChapterCompleted;
        highestLevel = (int)data.highestLevelCompleted;

        PlayerData newPlayerData = new PlayerData();
        newPlayerData.selectedCharacter = (int)data.selectedCharacter;
        newPlayerData.health = (int)data.health;
        newPlayerData.collectedHearts = data.collectedHearts;
        newPlayerData.collectedMirror = data.collectedMirror;

        this.playerData = newPlayerData;

        SettingsData newSettings = new SettingsData();
        newSettings.masterVolume = (int)data.masterVolume;
        newSettings.musicVolume = (int)data.musicVolume;
        newSettings.sfxVolume = (int)data.sfxVolume;
        newSettings.enemyVolume = (int)data.enemyVolume;

        this.settingsData = newSettings;

        GreekLibrary_2DData GL2D_Temp = new GreekLibrary_2DData();
        GL2D_Temp.enemyData = data.GL2D_Enemies;
        GL2D_Temp.leverComplete = data.GL2D_Lever.boolData;
        GL2D_Temp.pillarLocation = data.GL2D_Pillar.position;

        this.GL2D = GL2D_Temp;

        GreekLibrary_3DData GL3D_Temp = new GreekLibrary_3DData();
        GL3D_Temp.enemyData = data.GL3D_Enemies;

        this.GL3D = GL3D_Temp;

        GreekAthens_2DData GA2D_Temp = new GreekAthens_2DData();
        GA2D_Temp.enemyData = data.GA2D_Enemies;
        GA2D_Temp.fountain = data.GA2D_Fountain.boolData;

        this.GA2D = GA2D_Temp;

        GreekCavern_2DData GC2D_Temp = new GreekCavern_2DData();
        GC2D_Temp.enemyData = data.GC2D_Enemies;

        this.GC2D = GC2D_Temp;

        GreekCavern_2D_LeversData GC2DL_Temp = new GreekCavern_2D_LeversData();
        GC2DL_Temp.enemyData = data.GC2DL_Enemies;

        this.GC2DL = GC2DL_Temp;

        GreekCavern_2D_StatuesData GC2DS_Temp = new GreekCavern_2D_StatuesData();
        GC2DS_Temp.enemyData = data.GC2DS_Enemies;

        this.GC2DS = GC2DS_Temp;

        GreekLabyrinth_3DData GLa3D_Temp = new GreekLabyrinth_3DData();
        GLa3D_Temp.enemyData = data.GLa3D_Enemies;

        this.GLa3D = GLa3D_Temp;

        GreekLabyrinth_2D_LeversData GLa2DL_Temp = new GreekLabyrinth_2D_LeversData();
        GLa2DL_Temp.enemyData = data.GLa2DL_Enemies;

        this.GLa2DL = GLa2DL_Temp;

        GreekLabyrinth_2D_PedastalsData GLa2DP_Temp = new GreekLabyrinth_2D_PedastalsData();
        GLa2DP_Temp.enemyData = data.GLa2DP_Enemies;

        this.GLa2DP = GLa2DP_Temp;

        GreekMedusa_3DData GM3D_Temp = new GreekMedusa_3DData();
        GM3D_Temp.enemyData = data.GM3D_Enemies;

        this.GM3D = GM3D_Temp;

        VikingVillage_2DData VV2D_Temp = new VikingVillage_2DData();
        VV2D_Temp.enemyData = data.VV2D_Enemies;

        this.VV2D = VV2D_Temp;

        VikingVillage_3DData VV3D_Temp = new VikingVillage_3DData();
        VV3D_Temp.enemyData = data.VV3D_Enemies;

        this.VV3D = VV3D_Temp;


        Save();
    }

    public void SyncData(GameData data)
    {
        data.highestChapterCompleted = (MythologyMayhem.Chapter)highestChapter;
        data.highestLevelCompleted = (MythologyMayhem.Level)highestLevel;

        data.selectedCharacter = (MythologyMayhem.Character)playerData.selectedCharacter;
        data.health = (int)playerData.health;
        data.collectedHearts = playerData.collectedHearts;
        data.collectedMirror = playerData.collectedMirror;

        data.masterVolume = (int)settingsData.masterVolume;
        data.musicVolume = (int)settingsData.musicVolume;
        data.sfxVolume = (int)settingsData.sfxVolume;
        data.enemyVolume = (int)settingsData.enemyVolume;

        data.GL2D_Enemies = GL2D.enemyData;
        data.GL2D_Lever.boolData = GL2D.leverComplete;
        data.GL2D_Pillar.position = GL2D.pillarLocation;

        data.GL3D_Enemies = GL3D.enemyData;

        data.GA2D_Enemies = GA2D.enemyData;
        data.GA2D_Fountain.boolData = GA2D.fountain;

        data.GC2D_Enemies = GC2D.enemyData;

        data.GC2DL_Enemies = GC2DL.enemyData;

        data.GC2DS_Enemies = GC2DS.enemyData;

        data.GLa3D_Enemies = GLa3D.enemyData;

        data.GLa2DL_Enemies = GLa2DL.enemyData;

        data.GLa2DP_Enemies = GLa2DP.enemyData;

        data.GM3D_Enemies = GM3D.enemyData;

        data.VV2D_Enemies = VV2D.enemyData;

        data.VV3D_Enemies = VV3D.enemyData;
    }

    void LoadData(SaveData data) 
    {
        this.highestChapter = data.highestChapter;
        this.highestLevel = data.highestLevel;

        SettingsData newSettings = new SettingsData();
        newSettings = data.settingsData;

        this.settingsData = newSettings;

        PlayerData newPlayerData = new PlayerData();
        newPlayerData = data.playerData;

        this.playerData = newPlayerData;

        GreekLibrary_2DData GL2D_Temp = new GreekLibrary_2DData();
        GL2D_Temp = data.GL2D;

        this.GL2D = GL2D_Temp;

        GreekLibrary_3DData GL3D_Temp = new GreekLibrary_3DData();
        GL3D_Temp = data.GL3D;

        this.GL3D = GL3D_Temp;

        GreekAthens_2DData GA2D_Temp = new GreekAthens_2DData();
        GA2D_Temp = data.GA2D;

        this.GA2D = GA2D_Temp;

        GreekCavern_2DData GC2D_Temp = new GreekCavern_2DData();
        GC2D_Temp = data.GC2D;

        this.GC2D = GC2D_Temp;

        GreekCavern_2D_LeversData GC2DL_Temp = new GreekCavern_2D_LeversData();
        GC2DL_Temp = data.GC2DL;

        this.GC2DL = GC2DL_Temp;

        GreekCavern_2D_StatuesData GC2DS_Temp = new GreekCavern_2D_StatuesData();
        GC2DS_Temp = data.GC2DS;

        this.GC2DS = GC2DS_Temp;

        GreekLabyrinth_3DData GLa3D_Temp = new GreekLabyrinth_3DData();
        GLa3D_Temp = data.GLa3D;

        this.GLa3D = GLa3D_Temp;

        GreekLabyrinth_2D_LeversData GLa2DL_Temp = new GreekLabyrinth_2D_LeversData();
        GLa2DL_Temp = data.GLa2DL;

        this.GLa2DL = GLa2DL_Temp;

        GreekLabyrinth_2D_PedastalsData GLa2DP_Temp = new GreekLabyrinth_2D_PedastalsData();
        GLa2DP_Temp = data.GLa2DP;

        this.GLa2DP = GLa2DP_Temp;

        GreekMedusa_3DData GM3D_Temp = new GreekMedusa_3DData();
        GM3D_Temp = data.GM3D;

        this.GM3D = GM3D_Temp;

        VikingVillage_2DData VV2D_Temp = new VikingVillage_2DData();
        VV2D_Temp = data.VV2D;

        this.VV2D = VV2D_Temp;

        VikingVillage_3DData VV3D_Temp = new VikingVillage_3DData();
        VV3D_Temp = data.VV3D;

        this.VV3D = VV3D_Temp;

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
        PlayerData newPlayerData = new PlayerData();
        this.playerData = newPlayerData;

        GreekLibrary_2DData GL2D_Temp = new GreekLibrary_2DData();
        this.GL2D = GL2D_Temp;

        GreekLibrary_3DData GL3D_Temp = new GreekLibrary_3DData();
        this.GL3D = GL3D_Temp;

        GreekAthens_2DData GA2D_Temp = new GreekAthens_2DData();
        this.GA2D = GA2D_Temp;

        GreekCavern_2DData GC2D_Temp = new GreekCavern_2DData();
        this.GC2D = GC2D_Temp;

        GreekCavern_2D_LeversData GC2DL_Temp = new GreekCavern_2D_LeversData();
        this.GC2DL = GC2DL_Temp;

        GreekCavern_2D_StatuesData GC2DS_Temp = new GreekCavern_2D_StatuesData();
        this.GC2DS = GC2DS_Temp;

        GreekLabyrinth_3DData GLa3D_Temp = new GreekLabyrinth_3DData();
        this.GLa3D = GLa3D_Temp;

        GreekLabyrinth_2D_LeversData GLa2DL_Temp = new GreekLabyrinth_2D_LeversData();
        this.GLa2DL = GLa2DL_Temp;

        GreekLabyrinth_2D_PedastalsData GLa2DP_Temp = new GreekLabyrinth_2D_PedastalsData();
        this.GLa2DP = GLa2DP_Temp;

        GreekMedusa_3DData GM3D_Temp = new GreekMedusa_3DData();
        this.GM3D = GM3D_Temp;

        VikingVillage_2DData VV2D_Temp = new VikingVillage_2DData();
        this.VV2D = VV2D_Temp;

        VikingVillage_3DData VV3D_Temp = new VikingVillage_3DData();
        this.VV3D = VV3D_Temp;
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
[System.Serializable]
public class PlayerData
{
    public int selectedCharacter;
    public int health;
    public int collectedHearts;
    public bool collectedMirror;
}
[System.Serializable]
public class GreekLibrary_2DData 
{
    public bool[] enemyData;
    public bool leverComplete;
    public Vector3 pillarLocation;
}
[System.Serializable]
public class GreekLibrary_3DData
{
    public bool[] enemyData;
}
[System.Serializable]
public class GreekAthens_2DData
{
    public bool[] enemyData;
    public bool fountain;
}
[System.Serializable]
public class GreekCavern_2DData
{
    public bool[] enemyData;
}
[System.Serializable]
public class GreekCavern_2D_LeversData
{
    public bool[] enemyData;
}
[System.Serializable]
public class GreekCavern_2D_StatuesData
{
    public bool[] enemyData;
}
[System.Serializable]
public class GreekLabyrinth_3DData
{
    public bool[] enemyData;
}
[System.Serializable]
public class GreekLabyrinth_2D_LeversData
{
    public bool[] enemyData;
}
[System.Serializable]
public class GreekLabyrinth_2D_PedastalsData
{
    public bool[] enemyData;
}
[System.Serializable]
public class GreekMedusa_3DData
{
    public bool[] enemyData;
}
[System.Serializable]
public class VikingVillage_2DData
{
    public bool[] enemyData;
}
[System.Serializable]
public class VikingVillage_3DData
{
    public bool[] enemyData;
}
