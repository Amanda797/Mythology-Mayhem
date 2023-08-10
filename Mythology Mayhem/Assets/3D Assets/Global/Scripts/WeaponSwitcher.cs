using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MythologyMayhem
{

    public MainHand currentMain;
    public OffHand currentOffHand;

    [Header("Right Hand")]
    public GameObject Main;
    public GameObject Bow;

    [Header("Left Hand")]
    public GameObject Crystal;
    public GameObject Mirror;
    public GameObject Compass;
    /*
    private bool bow = false;
    private bool funny = false;
    private bool funny2 = false;
    private bool b1 = true;
    private bool b2 = false;
    private bool b3 = false;
    private bool b4 = false;

    #endregion
    private bool sword = true;
    private bool switched = false;
    [SerializeField] private float TO = 1f;
    private float m_timeStamp = 0f;
    */
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            print("1 Pressed");
            switch (currentMain) 
            {
                case MainHand.MainWeapon:
                    currentMain = MainHand.Bow;
                    Main.SetActive(false);
                    Bow.SetActive(true);
                    break;

                case MainHand.Bow:
                    currentMain = MainHand.MainWeapon;
                    Main.SetActive(true);
                    Bow.SetActive(false);
                    break;

            }
        }
        //Currently Cycles, need key assignment for each switch due to number of items later on
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            switch (currentOffHand) 
            {
                case OffHand.Crystal:
                    currentOffHand = OffHand.Mirror;
                    Crystal.SetActive(false);
                    Mirror.SetActive(true);
                    Compass.SetActive(false);
                    break;

                case OffHand.Mirror:
                    currentOffHand = OffHand.Compass;
                    Crystal.SetActive(false);
                    Mirror.SetActive(false);
                    Compass.SetActive(true);
                    break;

                case OffHand.Compass:
                    currentOffHand = OffHand.Crystal;
                    Crystal.SetActive(true);
                    Mirror.SetActive(false);
                    Compass.SetActive(false);
                    break;
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Alpha6) && funny == false)
        {
            funny = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && funny2 == false && funny == true)
        {
            funny2 = true;
            funny = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && funny2 == true && funny == true)
        {
            funny2 = false;
            funny = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && funny2 != false && b1 != true)
        {
            b1 = true;
            b2 = false;
            b3 = false;
            b4 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && funny2 != false && b2 != true)
        {
            b1 = false;
            b2 = true;
            b3 = false;
            b4 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && funny2 != false && b3 != true)
        {
            b3 = true;
            b2 = false;
            b1 = false;
            b4 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && funny2 != false && b4 != true)
        {
            b4 = true;
            b2 = false;
            b3 = false;
            b1 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && funny2 != false && b1 != true && bow == true)
        {
            b1 = true;
            b2 = false;
            b3 = false;
            b4 = false;
            Bow1.SetActive(true);
            Bow3.SetActive(false);
            Bow2.SetActive(false);
            Bow4.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && funny2 != false && b2 != true && bow == true)
        {
            b1 = false;
            b2 = true;
            b3 = false;
            b4 = false;
            Bow1.SetActive(false);
            Bow3.SetActive(false);
            Bow2.SetActive(true);
            Bow4.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && funny2 != false && b3 != true && bow == true)
        {
            b3 = true;
            b2 = false;
            b1 = false;
            b4 = false;
            Bow1.SetActive(false);
            Bow3.SetActive(true);
            Bow2.SetActive(false);
            Bow4.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && funny2 != false && b4 != true && bow == true)
        {
            b4 = true;
            b2 = false;
            b3 = false;
            b1 = false;
            Bow1.SetActive(false);
            Bow3.SetActive(false);
            Bow2.SetActive(false);
            Bow4.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == true && bow == false && switched == false && b1 == true && (Time.time >= m_timeStamp))
        {
            bow = true;
            sword = false;
            Main.SetActive(false);
            Bow1.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && b1 == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Main.SetActive(true);
            Bow1.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == true && bow == false && switched == false && b2 == true && (Time.time >= m_timeStamp))
        {
            bow = true;
            sword = false;
            Main.SetActive(false);
            Bow2.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && b2 == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Main.SetActive(true);
            Bow2.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == true && bow == false && switched == false && b3 == true && (Time.time >= m_timeStamp))
        {
            bow = true;
            sword = false;
            Main.SetActive(false);
            Bow3.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && b3 == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Main.SetActive(true);
            Bow3.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == true && bow == false && switched == false && b4 == true && (Time.time >= m_timeStamp))
        {
            bow = true;
            sword = false;
            Main.SetActive(false);
            Bow4.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && b4 == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Main.SetActive(true);
            Bow4.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
        */
    }
}
