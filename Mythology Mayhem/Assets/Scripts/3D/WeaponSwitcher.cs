using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject Sword;
    #region bow
    public GameObject Bow1;
    public GameObject Bow2;
    public GameObject Bow3;
    public GameObject Bow4;
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
    private float TO = 1f;
    private float m_timeStamp = 0f;

    // Update is called once per frame
    void Update()
    {
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
            Sword.SetActive(false);
            Bow1.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && b1 == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Sword.SetActive(true);
            Bow1.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == true && bow == false && switched == false && b2 == true && (Time.time >= m_timeStamp))
        {
            bow = true;
            sword = false;
            Sword.SetActive(false);
            Bow2.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && b2 == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Sword.SetActive(true);
            Bow2.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == true && bow == false && switched == false && b3 == true && (Time.time >= m_timeStamp))
        {
            bow = true;
            sword = false;
            Sword.SetActive(false);
            Bow3.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && b3 == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Sword.SetActive(true);
            Bow3.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == true && bow == false && switched == false && b4 == true && (Time.time >= m_timeStamp))
        {
            bow = true;
            sword = false;
            Sword.SetActive(false);
            Bow4.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && b4 == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Sword.SetActive(true);
            Bow4.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
    }
}
