using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Stats")]
    [SerializeField] private float atkDamage = 1f;
    [SerializeField] private int maxHealth = 10;

    private int currHealth;

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
        GetComponent<MouseAI>().enabled = false;
        this.enabled = false;
    }
}
