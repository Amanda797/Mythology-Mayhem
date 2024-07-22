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
    public int currentRightHandWeapon;

    [Header("Left Hand")]
    public GameObject[] LeftHandWeapons;
    public int currentLeftHandWeapon;

    public float weaponSwitchCooldown = .5f;
    public bool canSwitch = false;
    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
        StartCoroutine(ToggleCooldown());
    }
    // Update is called once per frame
    void Update()
    {
        if (!canSwitch) return;

        //Main Hand
        if (Input.GetKeyUp(KeyCode.Alpha1)) 
        {
            canSwitch = false;

            SwitchMainWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            canSwitch = false;

            SwitchOffhandWeapon();
        }
    }
    IEnumerator ToggleCooldown()
    {
        yield return new WaitForSeconds(weaponSwitchCooldown);
        canSwitch = true;
    }

    void SwitchMainWeapon()
    {
        RightHandWeapons[currentRightHandWeapon].SetActive(false);

        if (currentRightHandWeapon + 1 >= RightHandWeapons.Length) currentRightHandWeapon = 0;
        else currentRightHandWeapon++;

        if (RightHandWeapons[currentRightHandWeapon].name.Contains("Sword"))
        {
            RightHandWeapons[currentRightHandWeapon].gameObject.SetActive(true);
            StartCoroutine(ToggleCooldown());
        }
        else if (RightHandWeapons[currentRightHandWeapon].name == "TobiasBow")
        {
            if (gameManager.gameData.collectedBow)
            {
                RightHandWeapons[currentRightHandWeapon].gameObject.SetActive(true);

                StartCoroutine(ToggleCooldown());
            }
            else SwitchMainWeapon();
        }
        else if (RightHandWeapons[currentRightHandWeapon].name == "Thorshammer")
        {
            if (gameManager.gameData.collectedHammer)
            {
                RightHandWeapons[currentRightHandWeapon].gameObject.SetActive(true);

                StartCoroutine(ToggleCooldown());
            }
            else SwitchMainWeapon();
        }

        currentMain = RightHandWeapons[currentRightHandWeapon].GetComponent<PlayerWeapon>().mainHand;
        StartCoroutine(ToggleCooldown());
    }

    void SwitchOffhandWeapon()
    {
        LeftHandWeapons[currentLeftHandWeapon].SetActive(false);

        if (currentLeftHandWeapon + 1 >= LeftHandWeapons.Length) currentLeftHandWeapon = 0;
        else currentLeftHandWeapon++;

        if (LeftHandWeapons[currentLeftHandWeapon].name == "Crystal")
        {
            LeftHandWeapons[currentLeftHandWeapon].gameObject.SetActive(true);
            StartCoroutine(ToggleCooldown());
        }
        else if (LeftHandWeapons[currentLeftHandWeapon].name == "GreekMirror3D")
        {
            if (gameManager.gameData.collectedMirror)
            {
                LeftHandWeapons[currentLeftHandWeapon].gameObject.SetActive(true);

                StartCoroutine(ToggleCooldown());
            }
            else SwitchMainWeapon();
        }
        else if (LeftHandWeapons[currentLeftHandWeapon].name.Contains("Compass"))
        {
            if (gameManager.gameData.collectedCompass)
            {
                LeftHandWeapons[currentLeftHandWeapon].gameObject.SetActive(true);

                StartCoroutine(ToggleCooldown());
            }
            else SwitchMainWeapon();
        }

        currentOffHand = LeftHandWeapons[currentLeftHandWeapon].GetComponent<PlayerWeapon>().offHand;
        StartCoroutine(ToggleCooldown());
    }
}
