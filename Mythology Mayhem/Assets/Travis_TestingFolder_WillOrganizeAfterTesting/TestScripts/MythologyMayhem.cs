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
        MainMenu,
        CutScene1,
        CutScene2,
        GreekLibrary_2D,
        GreekLibrary_3D,
        GreekAthens_2D,
        GreekCavern_2D,
        GreekLabyrinth_3D,
        GreekLabyrinth_2D_Levers,
        GreekLabyrinth_2D_Pedastals,
        GreekMedusa_3D,
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
        EgyptianVillage_3D,
        EgyptianDesertDay_2D,
        EgyptianDesertNight_2D,
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
