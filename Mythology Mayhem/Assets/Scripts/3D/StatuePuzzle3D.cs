using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatuePuzzle3D : MonoBehaviour
{
    [System.Serializable]
    public class Statue
    {
        [HideInInspector]public int currentHead = 0;
        public int correctHead;
    }

    public UnityEvent puzzleCompleteEvent;

    public List<Statue> statues = new List<Statue>();

    public List<Mesh> heads = new List<Mesh>();

    public bool deBugPuzzleReset = false;

    public GameObject healthPickUp;
    public Transform healthSpawnPos;

    
    // Start is called before the first frame update
    void Awake()
    {
        if(deBugPuzzleReset == true)
        {
            PuzzleReset();
        }
        CheckHeadPuzzleStatus();
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
            string statueBodyCheck = "statueBody" + i;
            PlayerPrefs.SetInt(statueBodyCheck, 0);
        }
    }

    public void CheckHeadPuzzleStatus()
    {
        if(HeadPuzzleComplete())
        {
            Instantiate(healthPickUp, healthSpawnPos.position, Quaternion.identity);
            DisableAllStatueParts();
            puzzleCompleteEvent.Invoke();
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

    void DisableAllStatueParts()
    {
        StatueBody3D[] allStatueBodies;
        StatueHead[] allStatueHeads;
        allStatueBodies = FindObjectsOfType<StatueBody3D>();
        allStatueHeads = FindObjectsOfType<StatueHead>();
        foreach (StatueBody3D statueBody in allStatueBodies)
        {
            statueBody.enabled = false;
        }
        foreach (StatueHead statueHead in allStatueHeads)
        {
            statueHead.enabled = false;
        }
    }

}
