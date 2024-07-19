using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static StatueWeapon;

public class StatuePuzzle : MonoBehaviour
{
    [System.Serializable]
    public class Statue 
    {
        public int currentHead = 0;
        public int correctHead;
        public int currentWeapon;
        public int correctWeapon;
        
    }

    public UnityEvent puzzleCompleteEvent;

    public List<StatueBody2D> statues = new List<StatueBody2D>();

    public List<Sprite> heads = new List<Sprite>();

    public GameObject healthPickUp;
    public Transform healthSpawnPos;

    public bool deBugResetPuzzle;

    public Weapon currentWeapon = Weapon.Null;
    public GameObject currentWeaponObject = null;


    public void CheckWeaponPuzzleStatus()
    {
        Debug.Log("CheckWeaponPuzzleStatus: " + WeaponPuzzleComplete());
        if (WeaponPuzzleComplete())
        {
            //Instantiate(healthPickUp, healthSpawnPos.position, Quaternion.identity);
            healthPickUp.SetActive(true);
            DisableAllStatueParts();
            puzzleCompleteEvent.Invoke();
        }
    }

    void DisableAllStatueParts()
    {
        StatueBody2D[] allStatueBodies;
        StatueWeapon[] allStatueWeapons;
        allStatueBodies = FindObjectsOfType<StatueBody2D>();
        allStatueWeapons = FindObjectsOfType<StatueWeapon>();
        foreach (StatueBody2D statueBody in allStatueBodies)
        {
            statueBody.enabled = false;
        }
        foreach (StatueWeapon statueWeapon in allStatueWeapons)
        {
            statueWeapon.enabled = false;
        }
    }

    //public bool HeadPuzzleComplete()
    //{

    //    bool complete = true;

    //    foreach (Statue statue in statues)
    //    {
    //        if(statue.currentHead == statue.correctHead)
    //        {
    //            Debug.Log("Correct Head");
    //            continue;

    //        }
    //        Debug.Log("Incorrect Head");
    //        complete = false;
    //        break;
    //    }
    //    return complete;

    //}

    public bool WeaponPuzzleComplete()
    {
        bool complete = false;

        foreach (StatueBody2D statue in statues)
        {
            if (statue.hasCorrectWeapon) complete = true;
            else
            {
                complete = false;
                break;
            }
        }
        return complete;
    }
}
