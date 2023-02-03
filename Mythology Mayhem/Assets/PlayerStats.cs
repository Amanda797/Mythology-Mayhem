using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayers;

    [Header("Player Stats")]
    [SerializeField] private float atkDamage = 1f;
    [SerializeField] private float health = 10f;

    [SerializeField] private bool canHit = false;
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        /*if (damageCollider.IsTouching(test.GetComponent<Collider2D>()))
        {
            Debug.Log("hit");
        }
        foreach (GameObject enemy in enemies)
        {
            if (damageCollider.IsTouching(enemy.GetComponent<Collider2D>())) {
                if (canHit) {
                    enemy.GetComponent<MouseAI>().TakeDamage(atkDamage);
                    Debug.Log(enemy.GetComponent<MouseAI>().health);
                    canHit = false;
                }
                
            }
        }*/
    }    

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    /*private void CanAttack() 
    {
        damageCollider.gameObject.tag = "CanDamage";
    }
    private void CannotAttack() 
    {
        damageCollider.gameObject.tag = "Untagged";
    }*/
}
