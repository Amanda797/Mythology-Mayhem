using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MythologyMayhem
{
    public bool loaded;

    public Chapter highestChapterCompleted;

    public Level highestLevelCompleted;

    public bool overrideLoad;
    public Level overrideStartScene;
    [Header("Load Data")]
    public Level startScene;
    public Level spawnerToUse;
    [Header("Player Data")]
    public Character selectedCharacter;
    public int health;
    public int collectedHearts;
    public bool collectedMirror;
    [Header("Main Menu")]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float enemyVolume;
    [Header("Level Data")]
    public bool[] GL2D_Enemies;
    public SaveDataBool GL2D_Lever;
    public SaveDataPosition GL2D_Pillar;
    public bool[] GL3D_Enemies;
    public bool[] GA2D_Enemies;
    public SaveDataBool GA2D_Fountain;
    public bool[] GC2D_Enemies;
    public bool[] GC2DL_Enemies;
    public bool[] GC2DS_Enemies;
    public bool[] GLa3D_Enemies;
    public bool[] GLa2DL_Enemies;
    public bool[] GLa2DP_Enemies;
    public bool[] GM3D_Enemies;
    public bool[] VV2D_Enemies;
    public bool[] VV3D_Enemies;

    [Header("Save Data")]
    public SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStartScene() 
    {
        if (!overrideLoad)
        {
            switch (highestChapterCompleted)
            {
                case Chapter.Final:
                    print("Game has been beaten!");
                    startScene = Level.GreekLibrary_2D;
                    return;

                case Chapter.Inca:
                    print("Start at Final Chapter");
                    startScene = Level.GreekLibrary_2D;
                    return;

                case Chapter.Egyptian:
                    print("Start at Inca Chapter");
                    startScene = Level.GreekLibrary_2D;
                    return;

                case Chapter.Norse:
                    print("Start at Egyptian Chapter");
                    switch (highestLevelCompleted)
                    {
                        case Level.EgyptianVillage_2D:
                            print("Start at entrance to 3D Village");
                            startScene = Level.EgyptianVillage_3D;
                            spawnerToUse = Level.EgyptianVillage_3D;
                            return;

                        case Level.None:
                            print("Start at entrance to 2D Village");
                            startScene = Level.EgyptianVillage_2D;
                            spawnerToUse = Level.EgyptianVillage_2D;
                            return;
                    }
                    return;

                case Chapter.Greek:
                    print("Start at Norse Chapter");

                    switch (highestLevelCompleted)
                    {

                        case Level.VikingBoss_3D:
                            print("Norse already beaten");
                            highestChapterCompleted = Chapter.Norse;
                            print("Start at Egyptian Chapter");
                            startScene = Level.GreekLibrary_2D;
                            return;

                        case Level.VikingShip_3D:
                            print("Start on Ship, start of Boss fight");
                            startScene = Level.VikingBoss_3D;
                            spawnerToUse = Level.VikingShip_3D;
                            return;
                        case Level.VikingIceCave_3D:
                            print("Start outside Ice Cave ready to head to boss.");
                            startScene = Level.VikingIceCave_3D;
                            spawnerToUse = Level.VikingShip_3D;
                            return;

                        case Level.VikingIceCave_2D:
                            print("Start in 3D Ice Cave, before fighting to ship");
                            startScene = Level.VikingIceCave_3D;
                            spawnerToUse = Level.VikingIceCave_2D;
                            return;

                        case Level.VikingMine_3D:
                            print("Start at end of 2D Mine, ready to enter Ice Cave");
                            startScene = Level.VikingMine_3D;
                            spawnerToUse = Level.VikingIceCave_2D;
                            return;

                        case Level.VikingMine_2D:
                            print("Start at beginning of 3D Mine Sequence");
                            startScene = Level.VikingMine_2D;
                            spawnerToUse = Level.VikingMine_3D;
                            return;

                        case Level.VikingVillage_3D:
                            print("Start at return to 2D Village, ready to move to Mine");
                            startScene = Level.VikingVillage_3D;
                            spawnerToUse = Level.VikingVillage_2D;
                            return;


                        case Level.VikingVillage_2D:
                            print("Start at entrance to 3D Village");
                            startScene = Level.VikingVillage_3D;
                            spawnerToUse = Level.VikingVillage_2D;
                            return;

                        case Level.None:
                            print("Start at entrance to 2D Village");
                            startScene = Level.VikingVillage_2D;
                            spawnerToUse = Level.VikingVillage_2D;
                            return;
                    }
                    startScene = Level.GreekLibrary_2D;
            
                    return;
                case Chapter.None:
                    switch (highestLevelCompleted)
                    {
                        case Level.GreekLabyrinth_2D_Pedastals:
                            print("Start on Ship, start of Boss fight");
                            startScene = Level.GreekMedusa_3D;
                            spawnerToUse = Level.GreekLabyrinth_2D_Pedastals;
                            return;
                        case Level.GreekLabyrinth_2D_Levers:
                            print("Start outside Ice Cave ready to head to boss.");
                            startScene = Level.GreekLabyrinth_2D_Pedastals;
                            spawnerToUse = Level.GreekLabyrinth_2D_Levers;
                            return;

                        case Level.GreekLabyrinth_3D:
                            print("Start in 3D Ice Cave, before fighting to ship");
                            startScene = Level.GreekLabyrinth_2D_Levers;
                            spawnerToUse = Level.GreekLabyrinth_3D;
                            return;

                        case Level.GreekCavern_2D:
                            print("Start at end of 2D Mine, ready to enter Ice Cave");
                            startScene = Level.GreekLabyrinth_3D;
                            spawnerToUse = Level.GreekCavern_2D;
                            return;

                        case Level.GreekAthens_2D:
                            print("Start at beginning of 3D Mine Sequence");
                            startScene = Level.GreekCavern_2D;
                            spawnerToUse = Level.GreekAthens_2D;
                            return;

                        case Level.GreekLibrary_3D:
                            print("Start at return to 2D Village, ready to move to Mine");
                            startScene = Level.GreekAthens_2D;
                            spawnerToUse = Level.GreekLibrary_3D;
                            return;


                        case Level.GreekLibrary_2D:
                            print("Start at entrance to 3D Village");
                            startScene = Level.GreekLibrary_3D;
                            spawnerToUse = Level.GreekLibrary_2D;
                            return;

                        case Level.None:
                            print("Start at entrance to 2D Village");
                            startScene = Level.GreekLibrary_2D;
                            spawnerToUse = Level.GreekLibrary_2D;
                            return;
                    }
                    return;
            }
        }
        else 
        {
            print("Load Override");
        }
    }

    public void SaveEnemyData(Level whichLevel, bool[] enemyData) 
    {
        switch (whichLevel) 
        {
            case Level.GreekLibrary_2D:
                GL2D_Enemies = enemyData;
                break;
            case Level.GreekLibrary_3D:
                GL3D_Enemies = enemyData;
                break;
            case Level.GreekAthens_2D:
                GA2D_Enemies = enemyData;
                break;
            case Level.GreekCavern_2D:
                GC2D_Enemies = enemyData;
                break;
            case Level.GreekCavern_2D_Levers:
                GC2DL_Enemies = enemyData;
                break;
            case Level.GreekCavern_2D_Statues:
                GC2DS_Enemies = enemyData;
                break;
            case Level.GreekLabyrinth_3D:
                GLa3D_Enemies = enemyData;
                break;
            case Level.GreekLabyrinth_2D_Levers:
                GLa2DL_Enemies = enemyData;
                break;
            case Level.GreekLabyrinth_2D_Pedastals:
                GLa2DP_Enemies = enemyData;
                break;
            case Level.GreekMedusa_3D:
                GM3D_Enemies = enemyData;
                break;
            case Level.VikingVillage_2D:
                VV2D_Enemies = enemyData;
                break;
            case Level.VikingVillage_3D:
                VV3D_Enemies = enemyData;
                break;
        }
    }
    public bool[] FetchEnemyData(Level whichLevel)
    {
        switch (whichLevel)
        {
            case Level.GreekLibrary_2D:
                return GL2D_Enemies;
            case Level.GreekLibrary_3D:
                return GL3D_Enemies;
            case Level.GreekAthens_2D:
                return GA2D_Enemies;
            case Level.GreekCavern_2D:
                return GC2D_Enemies;
            case Level.GreekCavern_2D_Levers:
                return GC2DL_Enemies;
            case Level.GreekCavern_2D_Statues:
                return GC2DS_Enemies;
            case Level.GreekLabyrinth_3D:
                return GLa3D_Enemies;
            case Level.GreekLabyrinth_2D_Levers:
                return GLa2DL_Enemies;
            case Level.GreekLabyrinth_2D_Pedastals:
                return GLa2DP_Enemies;
            case Level.GreekMedusa_3D:
                return GM3D_Enemies;
            case Level.VikingVillage_2D:
                return VV2D_Enemies;
            case Level.VikingVillage_3D:
                return VV3D_Enemies;
        }
        return null;
    }

    public void NewGame()
    {
        highestChapterCompleted = Chapter.None;
        highestLevelCompleted = Level.None;

        overrideLoad = false;
        startScene = Level.GreekLibrary_2D;
        spawnerToUse = Level.GreekLibrary_2D;

        health = 100;
        collectedHearts = 0;
        collectedMirror = false;

        SaveData newData = new SaveData();
        newData.GenerateNewData();
        saveData = newData;
        saveData.SyncData(this);

        GameManager.instance.SaveGame();
    }
}
