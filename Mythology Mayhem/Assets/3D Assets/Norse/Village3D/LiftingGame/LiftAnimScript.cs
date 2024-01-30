using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftAnimScript : MonoBehaviour
{
    public int curRock;
    public GameObject[] groundRock;
    public GameObject[] handRock;

    public bool resetRock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (resetRock) 
        {
            ResetRock();
            resetRock = false;
        }   
    }

    public void ActivateRock() 
    {
        groundRock[curRock].SetActive(false);
        handRock[curRock].SetActive(true);
    }

    public void ResetRock() 
    {
        groundRock[curRock].SetActive(true);
        handRock[curRock].SetActive(false);
    }

    public void SetRock(int which) 
    {
        curRock = which;
        foreach (GameObject obj in groundRock) 
        {
            obj.SetActive(false);
        }

        groundRock[which].SetActive(true);

        foreach (GameObject obj in handRock) 
        {
            obj.SetActive(false);
        }
    }
}
