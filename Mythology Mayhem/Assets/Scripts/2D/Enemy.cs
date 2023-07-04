using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Stats")]
    [SerializeField] private int atkDamage = 1;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private bool canAttack = true;

    private int currHealth;

    public bool CanAttack {
        get {return canAttack;} 
        set {canAttack = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
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
        animator.SetBool("IsDead", true);

        Debug.Log("Enemy Died");

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<KnockBackFeedback>().enabled = false;
        GetComponent<MouseAI>().enabled = false;
        GetComponent<MouseAI>().dead = true;
        this.enabled = false;
        if(GetComponent<DropScrolls>() != null) {
            GetComponent<DropScrolls>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.layer == 3)
        {
            if(canAttack) {
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(atkDamage);
                other.gameObject.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            }
            
        }
    }
}
