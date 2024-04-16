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
    [Header("Main Menu")]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float enemyVolume;

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
                            startScene = Level.GreekLibrary_2D;
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

    public void NewGame()
    {
        highestChapterCompleted = Chapter.None;
        highestLevelCompleted = Level.None;

        overrideLoad = false;
        startScene = Level.GreekLibrary_2D;
        spawnerToUse = Level.GreekLibrary_2D;

        health = 100;

        SaveData newData = new SaveData();
        newData.GenerateNewData();
        saveData = newData;
        saveData.UpdateData(this);
    }
}
