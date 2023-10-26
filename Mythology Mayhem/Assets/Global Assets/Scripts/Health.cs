using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // --------------------------
    // ***PROPERTIES***
    // --------------------------
    [SerializeField] private float MaxHealth;
    [SerializeField]
    private float _health;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private GameObject mainObject; // Parent Self
    public GameObject rewardObject; // Reward Object

    [Header("Animation")]
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource healSound;
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
        //Death Check
        //if(GetHealth() <= 0) {
        //    SetHealth(0);
        //}
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
            if (hurtSound != null)
            {
                hurtSound.Play();
                Debug.Log("Damaged");
            }
            if(anim != null)
                anim.SetTrigger(hurtTrigger);
        }
        Life -= d;
    }//end take damage

    public void Heal(float h) {
        if (healSound != null)
            healSound.Play();
        if (anim != null)
            anim.SetTrigger(healTrigger);
        Life += h;
    }//end heal

    public void Death() {
        if(GetHealth() <= 0 && Life != -1000) {
            Life = -1000;

            if(gameObject.tag == "Enemy")
            {
                if (deathSound != null)
                    deathSound.Play();
                if (anim != null)
                    anim.SetTrigger(deathTrigger);
                if (gameObject.GetComponent<Enemy>().enemyDimension == MythologyMayhem.Dimension.TwoD)
                {
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                }
                else
                {
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }     
            foreach (Behaviour component in components) {
                component.enabled = false;
            }
            StartCoroutine(DeathTimer(3f));
        }//check that health is really less than 0 when called        
    }//end death

    public void Death(float time) {
        if(GetHealth() <= 0) {
            if(gameObject.tag == "Enemy")
            {
                if (deathSound != null)
                    deathSound.Play();
                if (anim != null)
                    anim.SetTrigger(deathTrigger);
                if(gameObject.GetComponent<Enemy>().enemyDimension == MythologyMayhem.Dimension.TwoD)
                {
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                } else
                {
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            foreach (Behaviour component in components)
            {
                component.enabled = false;
            }
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
