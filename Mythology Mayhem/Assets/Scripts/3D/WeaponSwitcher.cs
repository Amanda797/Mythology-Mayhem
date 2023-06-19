using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Bow;
    private bool sword = true;
    private bool bow = false;
    private bool switched = false;
    private float TO = 1f;
    private float m_timeStamp = 0f;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && sword == true && bow == false && switched == false && (Time.time >= m_timeStamp))
        {
            bow = true;
            sword = false;
            Sword.SetActive(false);
            Bow.SetActive(true);
            switched = true;
            m_timeStamp = Time.time + TO;
        }
        if (Input.GetKeyDown(KeyCode.X) && sword == false && bow == true && switched == true && (Time.time >= m_timeStamp))
        {
            bow = false;
            sword = true;
            Sword.SetActive(true);
            Bow.SetActive(false);
            switched = false;
            m_timeStamp = Time.time + TO;
        }
    }
}
