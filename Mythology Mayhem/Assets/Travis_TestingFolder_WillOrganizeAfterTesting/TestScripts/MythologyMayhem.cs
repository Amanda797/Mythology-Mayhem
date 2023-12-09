using UnityEngine;

public class MythologyMayhem : MonoBehaviour
{
    [HideInInspector]
    public string[] PlayerPrefKeys = new string[] { "spawningScene", "spwanPointIndex", "playerIndex" };
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
        VikingMine_2D,
        VikingMine_3D,
        VikingIceCave_2D,
        VikingIceCave_3D,
        VikingShip_3D,
        VikingShip_2D_CrossOver,
        VikingBoss_3D,
        EgyptianVillage_2D,
        EpytianDesertDay_2D,
        EpytianDesertNight_2D,
        EgyptianOutsidePyramid_2D,
        EgyptianInsidePyramid_2D,
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
        None,
        MainWeapon,
        Bow,
        ThorsHammer
    }

    public enum OffHand
    {
        None,
        Crystal,
        Mirror,
        Compass
    }
    public enum Enemies 
    { 
        Default,
        Viking1,
        Viking2,
        Viking3,
        Viking4,
        None
    }

}
