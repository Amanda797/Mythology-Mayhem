using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSHealth : MonoBehaviour
{ // --------------------------
    // ***PROPERTIES***
    // --------------------------

    [Header("Player Stats")]
    [SerializeField] HealthUIController huic;
    [SerializeField] public PlayerStats_SO ps;

    [SerializeField] Vector3 spawnPoint = Vector3.zero;

    // --------------------------
    // ***METHODS***
    // --------------------------
    
    void Awake()
    {        
        huic = GameObject.FindGameObjectWithTag("huic").GetComponent<HealthUIController>();

        if(huic != null) { 
            ps = huic.ps;
            ps.CanAttack = true;
            ps.NextAttackTime = 0;
            //ps.CurrHealth = ps.MaxHealth;
        }
        else{
            print("Can't find huic's player stats so");
        }
    }//end on awake


    private void Start()
    {
        spawnPoint = gameObject.transform.position;
    }

    public void SetHealth(float h) {
        huic.PlayerCurrHealth = h;
    }// end set health

    public float GetHealth() {
        return huic.PlayerCurrHealth;
    }//end get health

    public void TakeDamage(float d) {
        huic.PlayerCurrHealth -= d;
        if(GetHealth() <= 0) {
            Death();
        }
    }//end take damage

    public void Heal(float h) {
        huic.PlayerCurrHealth += h;
    }//end heal

    public void Death()
    {
        if (GetHealth() <= 0) {
            GetComponent<PlayerMovement3D>().enabled = false;
            gameObject.transform.position = spawnPoint;
            Heal(huic.PlayerMaxHealth);
            GetComponent<PlayerMovement3D>().enabled = true;
        }
    }
}
