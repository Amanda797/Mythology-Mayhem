using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSHealth : MonoBehaviour
{ // --------------------------
    // ***PROPERTIES***
    // --------------------------
    [SerializeField] private float MaxHealth;
    [SerializeField] private float health;
    [SerializeField] private GameObject mainObject; // Parent Self

    // --------------------------
    // ***METHODS***
    // --------------------------
    void Start()
    {
        health = MaxHealth;
    }// end start

    void FixedUpdate() {
        // Test
        float f = 0f;
        if(f >= 1f) {
            TakeDamage(1);
            print(GetHealth());
        } else {
            f += Time.deltaTime;
        }        

        //Death Check
        if(GetHealth() <= 0) {
            SetHealth(0);
        }
    }//end fixed update

    public void SetHealth(float h) {
        health = h;
    }// end set health

    public float GetHealth() {
        return health;
    }//end get health

    public void TakeDamage(float d) {
        health -= d;
        if(GetHealth() <= 0) {
            Death();
        }
    }//end take damage

    public void Heal(float h) {
        health += h;
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
