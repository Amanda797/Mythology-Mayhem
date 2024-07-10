using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class WeaponSwitcher : MythologyMayhem
{
    GameManager gameManager;
    public MainHand currentMain;
    public OffHand currentOffHand;

    [Header("Right Hand")]
    public GameObject[] RightHandWeapons;
    int currentRightHandWeapon;

    [Header("Left Hand")]
    public GameObject[] LeftHandWeapons;
    int currentLeftHandWeapon;

    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
    }
    // Update is called once per frame
    void Update()
    {
        //Main Hand
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            //Iterate Next Weapon
            if(currentRightHandWeapon + 1 >= RightHandWeapons.Length) currentRightHandWeapon = 0;
            else currentRightHandWeapon++;

            if (RightHandWeapons[currentRightHandWeapon].name == "TobiasBow")
            {
                if (!gameManager.gameData.saveData.playerData.collectedBow) currentRightHandWeapon++;
            }
            //Activate Next Weapon, Deactivate Other Weapons
            for (int i = 0; i < RightHandWeapons.Length; i++)
            {
                if(i == currentRightHandWeapon) RightHandWeapons[i].SetActive(true);
                else RightHandWeapons[i].SetActive(false);
            }

            //Set Current Main
            currentMain = RightHandWeapons[currentRightHandWeapon].GetComponent<PlayerWeapon>().mainHand;

            //switch (currentMain) 
            //{
            //    case MainHand.MainWeapon:
            //        currentMain = MainHand.Bow;
            //        Main.SetActive(false);
            //        Bow.SetActive(true);
            //        break;

            //    case MainHand.Bow:
            //        currentMain = MainHand.MainWeapon;
            //        Main.SetActive(true);
            //        Bow.SetActive(false);
            //        break;

            //}
        }
        //Currently Cycles, need key assignment for each switch due to number of items later on

        //Off Hand
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            //Iterate Next Weapon
            if (currentLeftHandWeapon + 1 >= LeftHandWeapons.Length) currentLeftHandWeapon = 0;
            else currentLeftHandWeapon++;

            if (LeftHandWeapons[currentLeftHandWeapon].name == "GreekMirror3D")
            {
                if (!gameManager.gameData.saveData.playerData.collectedMirror) currentLeftHandWeapon++;
            }

            //Activate Next Weapon, Deactivate Other Weapons
            for (int i = 0; i < LeftHandWeapons.Length; i++)
            {
                if (i == currentLeftHandWeapon) LeftHandWeapons[i].SetActive(true);
                else LeftHandWeapons[i].SetActive(false);
            }

            //Set Current Main
            currentOffHand = LeftHandWeapons[currentLeftHandWeapon].GetComponent<PlayerWeapon>().offHand;

            //switch (currentOffHand) 
            //{
            //    case OffHand.Crystal:
            //        currentOffHand = OffHand.Mirror;
            //        Crystal.SetActive(false);
            //        Mirror.SetActive(true);
            //        Compass.SetActive(false);
            //        break;

            //    case OffHand.Mirror:
            //        currentOffHand = OffHand.Compass;
            //        Crystal.SetActive(false);
            //        Mirror.SetActive(false);
            //        Compass.SetActive(true);
            //        break;

            //    case OffHand.Compass:
            //        currentOffHand = OffHand.Crystal;
            //        Crystal.SetActive(true);
            //        Mirror.SetActive(false);
            //        Compass.SetActive(false);
            //        break;
            //}
        }
    }
}
