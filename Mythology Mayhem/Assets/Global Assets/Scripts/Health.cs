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
    [SerializeField] private Animator anim;
    [SerializeField] private string hurtTrigger;
    [SerializeField] private string deathTrigger;
    [SerializeField] private string healTrigger;

    public Enemy enemy;

    public bool _attacked = false;
    public bool _defenseUp = false;
    [HideInInspector]
    public float _defenseTimer = 0f;

    public float respawnTimer = 50f;
    public bool canRespawn = false;

    // --------------------------
    // ***METHODS***
    // --------------------------
    void Start()
    {
        enemy = this.gameObject.GetComponent<Enemy>();
        Life = MaxHealth;
    }// end start

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

    public void Respawn(float _respawnHealth)
    {
        SetHealth(_respawnHealth);
    }//end respawn

    public void TakeDamage(float d) {
        //Defense Bool. If not _attacked, take damage. If _attacked, do not take damage. Use for timed, temporary defenses in specific enemies (See Boar3D)
        if(!_attacked && !_defenseUp)
        {
            if (gameObject.tag == "Enemy")
            {
                if (enemy != null)
                {
                    enemy.PlaySound(Enemy.Soundtype.Hurt);
                }
                if (anim != null)
                {
                    anim.SetTrigger(hurtTrigger);
                }

                StartCoroutine(Attacked());
            }

            Life -= d;
        }        
    }//end take damage

    IEnumerator Attacked()
    {
        _attacked = true;
        yield return new WaitForSeconds(_defenseTimer);
        _attacked = false;
    }

    public void Heal(float h) {
        if (anim != null)
            anim.SetTrigger(healTrigger);
        Life += Mathf.Clamp(h,h,MaxHealth);
    }//end heal

    public void Death() {
        if(GetHealth() <= 0 && Life != -1000) {
            //Lock Death() from being called again
            Life = -1000;

            //Trigger Death sounds and animations
            if (enemy != null)
            {
                enemy.PlaySound(Enemy.Soundtype.Death);
            }
            if (anim != null && deathTrigger != "")
                anim.SetTrigger(deathTrigger);

            if (gameObject.tag == "Enemy")
            {
                //Make rigidbody static
                if (gameObject.GetComponent<Enemy>() && gameObject.GetComponent<Enemy>().enemyDimension == MythologyMayhem.Dimension.TwoD)
                {
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                }
                else if (gameObject.GetComponent<Enemy>() && gameObject.GetComponent<Enemy>().enemyDimension == MythologyMayhem.Dimension.ThreeD)
                {
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
                    Destroy(gameObject.GetComponent<BoxCollider>());
                    gameObject.tag = "Untagged";
                }
            }

            foreach (Behaviour component in components)
            {
                component.enabled = false;
            }
            StartCoroutine(DeathTimer(4f));
        }//check that health is really less than 0 when called        
    }//end death

    public void Death(float time) {
        if(GetHealth() <= 0) {
            if(gameObject.tag == "Enemy")
            {
                if (enemy != null)
                {
                    enemy.PlaySound(Enemy.Soundtype.Death);
                }
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
        }

        mainObject.SetActive(false);

        if(mainObject.CompareTag("Companion"))
        {
            StartCoroutine(CompanionRespawn(respawnTimer));
        }
    }//end death timer
     //
     public IEnumerator CompanionRespawn(float time)
    {
        canRespawn = false;
        yield return new WaitForSeconds(time);
        canRespawn = true;
    }//end companion respawn timer

}//end health class
