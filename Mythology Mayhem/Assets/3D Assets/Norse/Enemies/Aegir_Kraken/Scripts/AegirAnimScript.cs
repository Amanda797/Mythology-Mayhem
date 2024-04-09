using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegirAnimScript : MonoBehaviour
{
    public AegirControlScript aegirControl;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateJet() 
    {
        aegirControl.ActivateJet();
    }

    public void SmallWaveSpawn() 
    {
        aegirControl.SmallWaveSpawn();
    }

    public void MediumWaveSpawn() 
    {
        aegirControl.MediumWaveSpawn();
    }

    public void LargeWaveSpawn() 
    {
        aegirControl.LargeWaveSpawn();
    }
}
