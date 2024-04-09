using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenTentacleAnimScript : MonoBehaviour
{

    public KrakenTentacleScript tentacleScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDeath() 
    {
        tentacleScript.AnimDeath();
    }
}
