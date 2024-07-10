using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    public List<LeverPuzzle> levers;
    public List<bool> combination;
    public bool match;
    public DoorCode Door;
    //public GameObject SpawnLocation;
    public GameObject item;

    public bool mirrorCollected;
    public bool loading;
    public bool completed;
    // Start is called before the first frame update
    void Start()
    {

        loading = true;

        Door.doorOpen = true;
        Door.blocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!loading)
        {
            if (!completed)
            {
                match = true;
                for (int i = 0; i < levers.Count; i++)
                {
                    if (levers[i].switchOn != combination[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    completed = true;
                    item.SetActive(true);
                    UpdateAndSave();
                }
            }
        }
        else 
        {
            if (SaveScene.instance != null) 
            {
                if (SaveScene.instance.Loaded) 
                {
                    for (int i = 0; i < SaveScene.instance.saveData.TwoDLabyrinthLevers.Count; i++) 
                    {
                        //levers[i].LoadState(SaveScene.instance.saveData.TwoDLabyrinthLevers[i]);
                    }
                    mirrorCollected = SaveScene.instance.saveData.collectedMirror;
                    completed = SaveScene.instance.saveData.TwoDLabyrinthCompleted;
                    if (!completed) 
                    {
                        item.SetActive(false);
                    }
                    if (completed && !mirrorCollected) 
                    {
                        item.SetActive(true);
                    }
                    if (completed && mirrorCollected) 
                    {
                        item.SetActive(false);
                    }
                    loading = false;
                }
            }
        }
    }

    public void CollectMirror() 
    {
        mirrorCollected = true;
        UpdateAndSave();
    }

    public void UpdateAndSave() 
    {
        List<bool> tempLeverList = new List<bool>();
        for (int i = 0; i < levers.Count; i++)
        {
            tempLeverList.Add(levers[i].switchOn);
        }
        SaveScene.instance.UpdateLeverPuzzle(tempLeverList, mirrorCollected, completed);
    }
}
