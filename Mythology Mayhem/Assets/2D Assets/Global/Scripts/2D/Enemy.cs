using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MythologyMayhem
{
    [SerializeField] private Animator animator;
    [Header("Stats")]
    [SerializeField] private Enemies enemyType;
    [SerializeField] private int atkDamage;
    [SerializeField] private int maxHealth;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private float attackRate;
    [Header("Viking1 Stats")]
    [SerializeField] private int viking1AtkDamage;
    [SerializeField] private int viking1MaxHealth;
    [SerializeField] private float timeLastTaunt;
    [SerializeField] private float tauntRate;
    [Header("Viking2 Stats")]
    [SerializeField] private int viking2AtkDamage;

    private int currHealth;

    public bool CanAttack {
        get {return canAttack;} 
        set {canAttack = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
        currHealth = maxHealth;
    }

    void Update()
    {
        switch(enemyType)
        {
            case Enemies.Default:
                break;

            case Enemies.Viking1:
                if(Time.time - timeLastTaunt >= tauntRate) 
                {
                    Debug.Log("Run Taunt" + Time.time + " " + timeLastTaunt + " " + (Time.time - timeLastTaunt));
                    timeLastTaunt = Time.time;
                }
                
                break;

            case Enemies.Viking2:
                break;
        }
    }

    public void SetStats()
    {
        switch(enemyType)
        {
            case Enemies.Default:
                atkDamage = 1;
                maxHealth = 10;
                attackRate = 1.5f;
                break;

            case Enemies.Viking1:
                atkDamage = viking1AtkDamage;
                maxHealth = viking1MaxHealth;
                break;
            case Enemies.Viking2:
                atkDamage = viking2AtkDamage;  
                break;
        }
    }

    public void TakeDamage(int damage) 
    {
        currHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currHealth <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsDead", true);

        Debug.Log("Enemy Died");

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<KnockBackFeedback>().enabled = false;
        GetComponent<MouseAI>().enabled = false;
        GetComponent<MouseAI>().dead = true;
        if(GetComponent<DropScrolls>() != null) {
            GetComponent<DropScrolls>().enabled = false;
        }

        StartCoroutine(Disappear());
    }

    IEnumerator Disappear() {
        yield return new WaitForSeconds(4);

        Destroy(this.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.layer == 3)
        {
            if(canAttack) {
                if(other.gameObject.GetComponent<PlayerStats>())
                    other.gameObject.GetComponent<PlayerStats>().TakeDamage(atkDamage);
                if(other.gameObject.GetComponent<KnockBackFeedback>())
                    other.gameObject.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
                canAttack = false;
                StartCoroutine(AttackRate());
                //TODO: Add dust cloud animation/particles
            }
            
        }
    }//end on collision enter 2d

    IEnumerator AttackRate() {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}
