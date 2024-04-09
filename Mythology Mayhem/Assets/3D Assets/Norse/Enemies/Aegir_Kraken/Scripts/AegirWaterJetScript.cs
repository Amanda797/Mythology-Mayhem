using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegirWaterJetScript : MonoBehaviour
{
    public float damagePerTick;
    public float damageTick;
    public float lastDamageTick;
    // Start is called before the first frame update
    void OnEnable()
    {
        lastDamageTick = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastDamageTick >= damageTick) 
        {
            DamageShip();
            lastDamageTick = Time.time;
        }
    }

    public void DamageShip() 
    { 
        
    }
}
