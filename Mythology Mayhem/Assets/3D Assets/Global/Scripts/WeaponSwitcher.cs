using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class WeaponSwitcher : MythologyMayhem
{
    GameManager gameManager;
    public MainHand currentMain;
    public GameObject currentMainObject;
    public OffHand currentOffHand;
    public GameObject currentOffHandObject;

    [Header("Right Hand")]
    public GameObject[] RightHandWeapons;
    public int currentRightHandWeapon;

    [Header("Left Hand")]
    public GameObject[] LeftHandWeapons;
    public int currentLeftHandWeapon;

    public float weaponSwitchCooldown = .5f;
    public bool canSwitchMainHand = true;
    public bool canSwitchOffHand = true;

    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
        currentMainObject = RightHandWeapons[currentRightHandWeapon];
        currentOffHandObject = LeftHandWeapons[currentLeftHandWeapon];
    }
    // Update is called once per frame
    void Update()
    {
        if (canSwitchMainHand)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                canSwitchMainHand = false;

                SwitchMainWeapon();
            }
        }

        if (canSwitchOffHand)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                canSwitchOffHand = false;

                SwitchOffhandWeapon();
            }
        }
    }
    IEnumerator MainHandCooldown()
    {
        yield return new WaitForSeconds(weaponSwitchCooldown);
        canSwitchMainHand = true;
    }
    IEnumerator OffHandCooldown()
    {
        yield return new WaitForSeconds(weaponSwitchCooldown);
        canSwitchOffHand = true;
    }

    void SwitchMainWeapon()
    {
        currentRightHandWeapon++;
        if (currentRightHandWeapon >= RightHandWeapons.Length) currentRightHandWeapon = 0;

        var nextWeapon = RightHandWeapons[currentRightHandWeapon];

        if (currentMainObject == nextWeapon) return;

        if (nextWeapon.name.Contains("Sword"))
        {
            // switch to sword
            currentMainObject.gameObject.SetActive(false);
            nextWeapon.gameObject.SetActive(true);
            currentMainObject = nextWeapon;
            currentMain = nextWeapon.GetComponent<PlayerWeapon>().mainHand;
            StartCoroutine(MainHandCooldown());
        }
        else if (nextWeapon.name.Contains("Bow"))
        {
            if (gameManager.gameData.collectedBow)
            {
                // switch to bow
                currentMainObject.gameObject.SetActive(false);
                nextWeapon.gameObject.SetActive(true);
                currentMainObject = nextWeapon;
                currentMain = nextWeapon.GetComponent<PlayerWeapon>().mainHand;
                StartCoroutine(MainHandCooldown());
            }
            else SwitchMainWeapon();
        }
        else if (RightHandWeapons[currentRightHandWeapon].name.Contains("hammer"))
        {
            if (gameManager.gameData.collectedHammer)
            {
                // switch to hammer
                currentMainObject.gameObject.SetActive(false);
                nextWeapon.gameObject.SetActive(true);
                currentMainObject = nextWeapon;
                currentMain = nextWeapon.GetComponent<PlayerWeapon>().mainHand;
                StartCoroutine(MainHandCooldown());
            }
            else SwitchMainWeapon();
        }
    }

    void SwitchOffhandWeapon()
    {
        currentLeftHandWeapon++;
        if (currentLeftHandWeapon >= LeftHandWeapons.Length) currentLeftHandWeapon = 0;

        var nextWeapon = LeftHandWeapons[currentLeftHandWeapon];

        if (currentOffHandObject == nextWeapon) return;

        if (nextWeapon.name.Contains("Crystal"))
        {
            if (gameManager.gameData.collectedCrystal)
            {
                // switch to crystal
                currentOffHandObject.gameObject.SetActive(false);
                nextWeapon.gameObject.SetActive(true);
                currentOffHandObject = nextWeapon;
                currentOffHand = nextWeapon.GetComponent<PlayerWeapon>().offHand;
                StartCoroutine(OffHandCooldown());
            }
        }
        else if (nextWeapon.name.Contains("Mirror"))
        {
            if (gameManager.gameData.collectedMirror)
            {
                // switch to mirror
                currentOffHandObject.gameObject.SetActive(false);
                nextWeapon.gameObject.SetActive(true);
                currentOffHandObject = nextWeapon;
                currentOffHand = nextWeapon.GetComponent<PlayerWeapon>().offHand;
                StartCoroutine(OffHandCooldown());
            }
            else SwitchMainWeapon();
        }
        else if (nextWeapon.name.Contains("Compass"))
        {
            if (gameManager.gameData.collectedCompass)
            {
                // switch to compass
                currentOffHandObject.gameObject.SetActive(false);
                nextWeapon.gameObject.SetActive(true);
                currentOffHandObject = nextWeapon;
                currentOffHand = nextWeapon.GetComponent<PlayerWeapon>().offHand;
                StartCoroutine(OffHandCooldown());
            }
            else SwitchMainWeapon();
        }
    }
}
