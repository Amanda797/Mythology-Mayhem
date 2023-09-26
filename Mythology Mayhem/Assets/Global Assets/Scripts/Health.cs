using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // --------------------------
    // ***PROPERTIES***
    // --------------------------
    [SerializeField] private float MaxHealth;
    [SerializeField] private float _health;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private GameObject mainObject; // Parent Self
    public GameObject rewardObject; // Reward Object

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
        Life = MaxHealth;
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

    public float Life
    {
        get { return _health; }
        set { _health = value; }
    }

    public void SetHealth(float h) {
        Life = h;
    }// end set health

    public float GetHealth() {
        return Life;
    }//end get health

    public void TakeDamage(float d) {
        if(gameObject.tag == "Enemy") 
        {
            if(anim != null)
                anim.SetTrigger(hurtTrigger);
        }
        Life -= d;
        if(GetHealth() <= 0) {
            Death();
        }
    }//end take damage

    public void Heal(float h) {
        if(anim != null)
            anim.SetTrigger(healTrigger);
        Life += h;
    }//end heal

    public void Death() {
        if(GetHealth() <= 0) {
            if(gameObject.tag == "Enemy") 
            {
                if(anim != null)
                    anim.SetTrigger(deathTrigger);
            }     
            //this? 
            //foreach (Behaviour component in components) {
            //    component.enabled = false;
            //}
            //or this??

            StartCoroutine(DeathTimer(3f));
        }//check that health is really less than 0 when called        
    }//end death

    public void Death(float time) {
        if(GetHealth() <= 0) {
            if(gameObject.tag == "Enemy") 
            {
                if(anim != null)
                    anim.SetTrigger(deathTrigger);
            }
                  
            //this? 
            //foreach (Behaviour component in components) {
            //    component.enabled = false;
            //}
            //or this??
            StartCoroutine(DeathTimer(time));
        }//check that health is really less than 0 when called        
    }//end death


    public IEnumerator DeathTimer(float time) {
        yield return new WaitForSeconds(time);
        if(rewardObject != null)
        {

            GameObject reward = Instantiate(rewardObject, transform.position + Vector3.up*3, transform.rotation);
            reward.name = rewardObject.name;
            SaveScene.AddObject(reward, rewardObject);
        }

        mainObject.SetActive(false);
    }//end death timer
    

}//end health class
