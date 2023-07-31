using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public List<Statue> statues = new List<Statue>();

    public List<Sprite> heads = new List<Sprite>();

    public GameObject healthPickUp;
    public Transform healthSpawnPos;

    public bool deBugResetPuzzle;


    // Start is called before the first frame update
    void Awake()
    {
       if(deBugResetPuzzle)
            PuzzleReset();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("This puzzle completion is: " + HeadPuzzleComplete());
        }
    }
    
    void PuzzleReset()
    {
        for (int i = 0; i < statues.Count; i++)
        {
            string statueWeaponCheck = "statueWeapon" + i;
            PlayerPrefs.SetInt(statueWeaponCheck, 0);
        }
    }

    public void CheckWeaponPuzzleStatus()
    {
        if(WeaponPuzzleComplete())
        {
            Instantiate(healthPickUp, healthSpawnPos.position, Quaternion.identity);
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

    public bool HeadPuzzleComplete()
    {

        bool complete = true;

        foreach (Statue statue in statues)
        {
            if(statue.currentHead == statue.correctHead)
            {
                Debug.Log("Correct Head");
                continue;

            }
            Debug.Log("Incorrect Head");
            complete = false;
            break;
        }
        return complete;

    }

    public bool WeaponPuzzleComplete()
    {
        bool complete = true;

        foreach (Statue statue in statues)
        {
            if(statue.currentWeapon == statue.correctWeapon)
            {
                Debug.Log("Correct Weapon");
                continue;

            }
            Debug.Log("Incorrect Weapon");
            complete = false;
            break;
        }
        return complete;
    }



}
