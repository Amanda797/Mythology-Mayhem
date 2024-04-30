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
    public float health;
    public int heartCollectionTotal;
    public bool[] collectedHearts;
    public bool collectedMirror;
    [Header("Main Menu")]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float enemyVolume;
    [Header("Level Data")]
    public bool[] GL2D_Enemies;
    public bool[] GL2D_Potions;
    public SaveDataBool GL2D_Lever;
    public SaveDataPosition GL2D_Pillar;
    public bool[] GL3D_Enemies;
    public bool[] GL3D_Potions;
    public bool[] GA2D_Enemies;
    public bool[] GA2D_Potions;
    public SaveDataBool GA2D_Fountain;
    public bool[] GC2D_Enemies;
    public bool[] GC2D_Potions;
    public bool[] GC2DL_Enemies;
    public bool[] GC2DL_Potions;
    public bool[] GC2DS_Enemies;
    public bool[] GC2DS_Potions;
    public bool[] GLa3D_Enemies;
    public bool[] GLa3D_Potions;
    public bool[] GLa2DL_Enemies;
    public bool[] GLa2DL_Potions;
    public bool[] GLa2DP_Enemies;
    public bool[] GLa2DP_Potions;
    public bool[] GM3D_Enemies;
    public bool[] GM3D_Potions;
    public bool[] VV2D_Enemies;
    public bool[] VV2D_Potions;
    public bool[] VV3D_Enemies;
    public bool[] VV3D_Potions;

    public enum BoolArrayType 
    { 
        Enemy,
        Potion
    }

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
    public void SetStartScene(bool overrideLoad) 
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
    public void SaveBoolArrayData(Level whichLevel, bool[] boolArrayData, BoolArrayType type) 
    {
        switch (whichLevel)
        {
            case Level.GreekLibrary_2D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GL2D_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GL2D_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekLibrary_3D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GL3D_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GL3D_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekAthens_2D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GA2D_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GA2D_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekCavern_2D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GC2D_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GC2D_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekCavern_2D_Levers:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GC2DL_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GC2DL_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekCavern_2D_Statues:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GC2DS_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GC2DS_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekLabyrinth_3D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GLa3D_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GLa3D_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekLabyrinth_2D_Levers:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GLa2DL_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GLa2DL_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekLabyrinth_2D_Pedastals:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GLa2DP_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GLa2DP_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.GreekMedusa_3D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        GM3D_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        GM3D_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.VikingVillage_2D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        VV2D_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        VV2D_Potions = boolArrayData;
                        break;
                }
                break;
            case Level.VikingVillage_3D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        VV3D_Enemies = boolArrayData;
                        break;
                    case BoolArrayType.Potion:
                        VV3D_Potions = boolArrayData;
                        break;
                }
                break;
        }
    }
    public bool[] FetchBoolArrayData(Level whichLevel, BoolArrayType type)
    {
        switch (whichLevel)
        {
            case Level.GreekLibrary_2D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GL2D_Enemies;
                    case BoolArrayType.Potion:
                        return GL2D_Potions;
                }
                break;
            case Level.GreekLibrary_3D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GL3D_Enemies;
                    case BoolArrayType.Potion:
                        return GL3D_Potions;
                }
                break;
            case Level.GreekAthens_2D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GA2D_Enemies;
                    case BoolArrayType.Potion:
                        return GA2D_Potions;
                }
                break;
            case Level.GreekCavern_2D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GC2D_Enemies;
                    case BoolArrayType.Potion:
                        return GC2D_Potions;
                }
                break;
            case Level.GreekCavern_2D_Levers:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GC2DL_Enemies;
                    case BoolArrayType.Potion:
                        return GC2DL_Potions;
                }
                break;
            case Level.GreekCavern_2D_Statues:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GC2DS_Enemies;
                    case BoolArrayType.Potion:
                        return GC2DS_Potions;
                }
                break;
            case Level.GreekLabyrinth_3D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GLa3D_Enemies;
                    case BoolArrayType.Potion:
                        return GLa3D_Potions;
                }
                break;
            case Level.GreekLabyrinth_2D_Levers:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GLa2DL_Enemies;
                    case BoolArrayType.Potion:
                        return GLa2DL_Potions;
                }
                break;
            case Level.GreekLabyrinth_2D_Pedastals:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GLa2DP_Enemies;
                    case BoolArrayType.Potion:
                        return GLa2DP_Potions;
                }
                break;
            case Level.GreekMedusa_3D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return GM3D_Enemies;
                    case BoolArrayType.Potion:
                        return GM3D_Potions;
                }
                break;
            case Level.VikingVillage_2D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return VV2D_Enemies;
                    case BoolArrayType.Potion:
                        return VV2D_Potions;
                }
                break;
            case Level.VikingVillage_3D:
                switch (type)
                {
                    case BoolArrayType.Enemy:
                        return VV3D_Enemies;
                    case BoolArrayType.Potion:
                        return VV3D_Potions;
                }
                break;
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

        health = 16;
        collectedHearts = new bool[heartCollectionTotal];
        //GameManager.instance.UpdateCollectedHearts(0, health);
        collectedMirror = false;

        SaveData newData = new SaveData();
        newData.GenerateNewData();
        saveData = newData;
        saveData.UpdateData(this);
    }
    public void UpdateLevelComplete(Chapter chapter, Level level) 
    {
        highestChapterCompleted = chapter;
        highestLevelCompleted = level;

        GameManager.instance.SaveGame();
    }

}
