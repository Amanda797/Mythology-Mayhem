using UnityEngine;

public class Health : MonoBehaviour
{
    // --------------------------
    // ***PROPERTIES***
    // --------------------------
    [SerializeField] private float MaxHealth;
    [SerializeField] private float health;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private GameObject mainObject; // Parent Self

    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private string hurtTrigger;
    [SerializeField] private string deathTrigger;
    [SerializeField] private string healTrigger;

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
        anim.SetTrigger(hurtTrigger);
        health -= d;
        if(GetHealth() <= 0) {
            Death();
        }
    }//end take damage

    public void Heal(float h) {
        anim.SetTrigger(healTrigger);
        health += h;
    }//end heal

    public void Death() {
        if(GetHealth() <= 0) {
            anim.SetTrigger(deathTrigger);       
            //this? 
            //foreach (Behaviour component in components) {
            //    component.enabled = false;
            //}
            //or this??
            Destroy(mainObject, 3f);
        }//check that health is really less than 0 when called        
    }//end death

    public void Death(float time) {
        if(GetHealth() <= 0) {
            anim.SetTrigger(deathTrigger);       
            //this? 
            //foreach (Behaviour component in components) {
            //    component.enabled = false;
            //}
            //or this??
            Destroy(mainObject, time);
        }//check that health is really less than 0 when called        
    }//end death

}//end health class
