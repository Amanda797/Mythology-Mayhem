using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythologyMayhem : MonoBehaviour
{
    public enum Dimension
    {
        TwoD,
        ThreeD
    }
    public enum Chapter
    {
        Greek,
        Norse,
        Egyptian,
        Inca,
        Final,
        None
    }

    public enum Level
    {
        Lib3D,
        VikingVillage_2D,
        VikingVillage_3D,
        VikingShip_3D,
        VikingShip_2D_CrossOver,
        VikingCave_3D,
        VikingCave_2D,
        VikingBoss_3D,
        None
    }

    public enum VikingVillageSteps
    {
        ObtainKey1,
        ObtainKey2,
        ObtainKey3,
        ObtainCompass,
        None
    }

    public enum Character
    {
        Tobias,
        Gorm,
        Amunet,
        Micos
    }

    public enum MainHand 
    { 
        MainWeapon,
        Bow,
        None
    }

    public enum OffHand 
    { 
        Crystal,
        Mirror,
        Compass,
        None
    }
    public enum Enemies 
    { 
        
    }

}
