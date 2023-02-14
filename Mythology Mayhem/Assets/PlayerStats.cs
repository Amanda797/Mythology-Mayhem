using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayers;

    [Header("Player Stats")]
    [SerializeField] private int atkDamage = 2;
    [SerializeField] private int maxHealth = 10;
    public int CurrHealth { get; set; }
    [SerializeField] private float attackRate = 2f;
    public float NextAttackTime { get; set; }

    [Header("Player Animation")]
    [SerializeField] private Animator anim;

    private SpriteRenderer sr;
    private bool flipped = false;    

    // Start is called before the first frame update
    void Start()
    {
        CurrHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        NextAttackTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= NextAttackTime) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                NextAttackTime = Time.time + 1f/attackRate;
            }
        }
        //The Code below is to flip the attackPoint of the player so that if the player is flipped they can still attack behind the enemy
        if (sr.flipX)
        {
            if (!flipped) 
            {
                attackPoint.localPosition = attackPoint.localPosition * new Vector2(-1,1);
                Debug.Log("Flipped backwards");
                flipped = !flipped;
            }
        } 
        else 
        {
            if (flipped)
            {
                attackPoint.localPosition = attackPoint.localPosition * new Vector2(-1,1);
                Debug.Log("Flipped forwards");
                flipped = !flipped;
            }
        }
    }    

    private void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(atkDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
