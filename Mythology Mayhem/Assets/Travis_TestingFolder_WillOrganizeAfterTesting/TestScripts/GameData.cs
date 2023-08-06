using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MythologyMayhem
{
    public Chapter highestChapterCompleted;

    public Level highestLevelCompleted;

    [Header("VikingVillage_3D")]
    public VikingVillageSteps lastCompletedVV3DStep;

    [Header("Load Data")]
    public bool overrideLoad;
    public Level startScene;
    public Level spawnerToUse;
    public Character selectedCharacter;
    public int Health;
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
                    startScene = Level.Lib3D;
                    return;

                case Chapter.Inca:
                    print("Start at Final Chapter");
                    startScene = Level.Lib3D;
                    return;

                case Chapter.Egyptian:
                    print("Start at Inca Chapter");
                    startScene = Level.Lib3D;
                    return;

                case Chapter.Norse:
                    print("Start at Egyptian Chapter");
                    startScene = Level.Lib3D;
                    return;

                case Chapter.Greek:
                    print("Start at Norse Chapter");

                    switch (highestLevelCompleted)
                    {

                        case Level.VikingBoss_3D:
                            print("Norse already beaten");
                            highestChapterCompleted = Chapter.Norse;
                            print("Start at Egyptian Chapter");
                            startScene = Level.Lib3D;
                            return;

                        case Level.VikingCave_2D:
                            print("Start outside Ice Cave ready to head to boss.");
                            startScene = Level.VikingCave_3D;
                            spawnerToUse = Level.VikingCave_2D;
                            return;

                        case Level.VikingCave_3D:
                            print("Start off Boat, after Ice Giant, ready to head into Ice Cave.");
                            startScene = Level.VikingCave_3D;
                            spawnerToUse = Level.VikingShip_3D;
                            return;

                        case Level.VikingShip_2D_CrossOver:
                            print("Start as Cavern Loads in, with shipping heading in its direction.");
                            startScene = Level.VikingShip_3D;
                            spawnerToUse = Level.VikingVillage_3D;
                            return;

                        case Level.VikingShip_3D:
                            print("Start at beginning of ship battle sequence");
                            startScene = Level.VikingShip_3D;
                            spawnerToUse = Level.VikingVillage_3D;
                            return;

                        case Level.VikingVillage_3D:
                            print("Start at entrance to ship, ready to board");
                            startScene = Level.VikingVillage_3D;
                            spawnerToUse = Level.VikingShip_3D;
                            return;


                        case Level.VikingVillage_2D:
                            print("Start at entrance to 3D Village");
                            startScene = Level.VikingVillage_3D;
                            spawnerToUse = Level.VikingVillage_2D;
                            return;

                        case Level.None:
                            print("Start at entrance to 2D Village");
                            startScene = Level.VikingVillage_2D;
                            spawnerToUse = Level.None;
                            return;
                    }
                    startScene = Level.Lib3D;
                    return;

                case Chapter.None:
                    print("Start at Begininng");
                    startScene = Level.Lib3D;
                    return;
            }
        }
        else 
        {
            print("Load Override");
        }
    }
}
