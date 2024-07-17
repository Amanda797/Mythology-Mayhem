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
        newPlayerData.curHealth = data.curHealth;
        newPlayerData.maxHealth = data.maxHealth;
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
        GL2D_Temp.poitionData = data.GL2D_Potions;
        //GL2D_Temp.leverComplete = data.GL2D_Lever.boolData;
        //GL2D_Temp.pillarLocation = data.GL2D_Pillar.position;

        this.GL2D = GL2D_Temp;

        GreekLibrary_3DData GL3D_Temp = new GreekLibrary_3DData();
        GL3D_Temp.enemyData = data.GL3D_Enemies;
        GL3D_Temp.poitionData = data.GL3D_Potions;

        this.GL3D = GL3D_Temp;

        GreekAthens_2DData GA2D_Temp = new GreekAthens_2DData();
        GA2D_Temp.enemyData = data.GA2D_Enemies;
        GA2D_Temp.poitionData = data.GA2D_Potions;
        //GA2D_Temp.fountain = data.GA2D_Fountain.boolData;

        this.GA2D = GA2D_Temp;

        GreekCavern_2DData GC2D_Temp = new GreekCavern_2DData();
        GC2D_Temp.enemyData = data.GC2D_Enemies;
        GC2D_Temp.poitionData = data.GC2D_Potions;

        this.GC2D = GC2D_Temp;

        GreekCavern_2D_LeversData GC2DL_Temp = new GreekCavern_2D_LeversData();
        GC2DL_Temp.enemyData = data.GC2DL_Enemies;

        this.GC2DL = GC2DL_Temp;

        GreekCavern_2D_StatuesData GC2DS_Temp = new GreekCavern_2D_StatuesData();
        GC2DS_Temp.enemyData = data.GC2DS_Enemies;
        GC2DS_Temp.poitionData = data.GC2DS_Potions;

        this.GC2DS = GC2DS_Temp;

        GreekLabyrinth_3DData GLa3D_Temp = new GreekLabyrinth_3DData();
        GLa3D_Temp.enemyData = data.GLa3D_Enemies;
        GLa3D_Temp.poitionData = data.GLa3D_Potions;

        this.GLa3D = GLa3D_Temp;

        GreekLabyrinth_2D_LeversData GLa2DL_Temp = new GreekLabyrinth_2D_LeversData();
        GLa2DL_Temp.enemyData = data.GLa2DL_Enemies;
        GLa2DL_Temp.poitionData = data.GLa2DL_Potions;

        this.GLa2DL = GLa2DL_Temp;

        GreekLabyrinth_2D_PedastalsData GLa2DP_Temp = new GreekLabyrinth_2D_PedastalsData();
        GLa2DP_Temp.enemyData = data.GLa2DP_Enemies;
        GLa2DP_Temp.poitionData = data.GLa2DP_Potions;

        this.GLa2DP = GLa2DP_Temp;

        GreekMedusa_3DData GM3D_Temp = new GreekMedusa_3DData();
        GM3D_Temp.enemyData = data.GM3D_Enemies;
        GM3D_Temp.poitionData = data.GM3D_Potions;

        this.GM3D = GM3D_Temp;

        VikingVillage_2DData VV2D_Temp = new VikingVillage_2DData();
        VV2D_Temp.enemyData = data.VV2D_Enemies;
        VV2D_Temp.poitionData = data.VV2D_Potions;

        this.VV2D = VV2D_Temp;

        VikingVillage_3DData VV3D_Temp = new VikingVillage_3DData();
        VV3D_Temp.enemyData = data.VV3D_Enemies;
        VV3D_Temp.poitionData = data.VV3D_Potions;

        this.VV3D = VV3D_Temp;


        Save();
    }

    public void SyncData(GameData data)
    {
        data.highestChapterCompleted = (MythologyMayhem.Chapter)highestChapter;
        data.highestLevelCompleted = (MythologyMayhem.Level)highestLevel;

        data.selectedCharacter = (MythologyMayhem.Character)playerData.selectedCharacter;
        data.curHealth = playerData.curHealth;
        data.maxHealth = playerData.maxHealth;

        data.collectedHearts = playerData.collectedHearts;
        int totalHearts = 0;
        for (int i = 0; i < data.collectedHearts.Length; i++) 
        {
            if (data.collectedHearts[i]) 
            {
                totalHearts++;
            }
        }
        //GameManager.instance.UpdateCollectedHearts(totalHearts, data.health);

        data.collectedMirror = playerData.collectedMirror;

        data.masterVolume = (int)settingsData.masterVolume;
        data.musicVolume = (int)settingsData.musicVolume;
        data.sfxVolume = (int)settingsData.sfxVolume;
        data.enemyVolume = (int)settingsData.enemyVolume;

        data.GL2D_Enemies = GL2D.enemyData;
        data.GL2D_Potions = GL2D.poitionData;
        //data.GL2D_Lever.boolData = GL2D.leverComplete;
        //data.GL2D_Pillar.position = GL2D.pillarLocation;

        data.GL3D_Enemies = GL3D.enemyData;
        data.GL3D_Potions = GL3D.poitionData;

        data.GA2D_Enemies = GA2D.enemyData;
        data.GA2D_Potions = GA2D.poitionData;
        data.GA2D_Fountain.boolData = GA2D.fountain;

        data.GC2D_Enemies = GC2D.enemyData;
        data.GC2DL_Potions = GC2D.poitionData;

        data.GC2DL_Enemies = GC2DL.enemyData;
        data.GC2DL_Potions = GC2DL.poitionData;

        data.GC2DS_Enemies = GC2DS.enemyData;
        data.GC2DS_Potions = GC2DS.poitionData;

        data.GLa3D_Enemies = GLa3D.enemyData;
        data.GLa3D_Potions = GLa3D.poitionData;

        data.GLa2DL_Enemies = GLa2DL.enemyData;
        data.GLa2DL_Potions = GLa2DL.poitionData;

        data.GLa2DP_Enemies = GLa2DP.enemyData;
        data.GLa2DP_Potions = GLa2DP.poitionData;

        data.GM3D_Enemies = GM3D.enemyData;
        data.GM3D_Potions = GM3D.poitionData;

        data.VV2D_Enemies = VV2D.enemyData;
        data.VV2D_Potions = VV2D.poitionData;

        data.VV3D_Enemies = VV3D.enemyData;
        data.VV3D_Potions = VV3D.poitionData;
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

    public void Save()
    {
        Debug.Log("Save File");
        string sceneObjectsString = JsonUtility.ToJson(this, true);

        System.IO.File.WriteAllText(Application.persistentDataPath + "SaveData.json", sceneObjectsString);

        //#if UNITY_EDITOR
        //    UnityEditor.AssetDatabase.Refresh();
        //#endif
    }

    public void Delete()
    {
        Debug.Log("Delete Saved File");
        if (!System.IO.File.Exists(Application.persistentDataPath + "SaveData.json")) return;
        System.IO.File.Delete(Application.persistentDataPath + "SaveData.json");
    }
    public void Load()
    {
        if (!System.IO.File.Exists(Application.persistentDataPath + "SaveData.json")) return;

        string sceneObjectsString = System.IO.File.ReadAllText(Application.persistentDataPath + "SaveData.json");

        SaveData loadedData = JsonUtility.FromJson<SaveData>(sceneObjectsString);
        LoadData(loadedData);
        GameManager.instance.gameData.loaded = true;
    }

    public void GenerateNewData() 
    {
        SettingsData newSettings = new SettingsData();
        this.settingsData = newSettings;

        PlayerData newPlayerData = new PlayerData();
        newPlayerData.collectedHearts = new bool[GameManager.instance.gameData.heartCollectionTotal];
        this.playerData = newPlayerData;

        this.GL2D = new GreekLibrary_2DData();

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
    public float curHealth;
    public float maxHealth;
    public bool[] collectedHearts;
    public bool collectedMirror;
    public bool collectedBow;
    public bool collectedOwl;
    public bool collectedWolf;
}
[System.Serializable]
public class GreekLibrary_2DData 
{
    public bool[] enemyData;
    public bool[] poitionData;
    public bool leverComplete;
    public Vector3 pillarLocation;
}
[System.Serializable]
public class GreekLibrary_3DData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class GreekAthens_2DData
{
    public bool[] enemyData;
    public bool[] poitionData;
    public bool fountain;
}
[System.Serializable]
public class GreekCavern_2DData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class GreekCavern_2D_LeversData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class GreekCavern_2D_StatuesData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class GreekLabyrinth_3DData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class GreekLabyrinth_2D_LeversData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class GreekLabyrinth_2D_PedastalsData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class GreekMedusa_3DData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class VikingVillage_2DData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
[System.Serializable]
public class VikingVillage_3DData
{
    public bool[] enemyData;
    public bool[] poitionData;
}
