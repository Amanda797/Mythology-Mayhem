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
            ps.CurrHealth = ps.MaxHealth;
        }
        else{
            print("Can't find huic's player stats so");
        }
    }//end on awake

    void FixedUpdate() {
        //Death Check
        if(GetHealth() <= 0) {
            SetHealth(0);
        }
    }//end fixed update

    public void SetHealth(float h) {
        huic.PlayerCurrHealth  = (int) h;
    }// end set health

    public float GetHealth() {
        return huic.PlayerCurrHealth;
    }//end get health

    public void TakeDamage(float d) {
        huic.PlayerCurrHealth -= (int) d;
        if(GetHealth() <= 0) {
            Death();
        }
    }//end take damage

    public void Heal(float h) {
        huic.PlayerCurrHealth += (int) h;
    }//end heal

    public void Death() {
        if(GetHealth() <= 0) {   
            //this? 
            //foreach (Behaviour component in components) {
            //    component.enabled = false;
            //}
            //or this??
            //Destroy(mainObject, 3f);
            
            //restart scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }//check that health is really less than 0 when called        
    }//end death
}
