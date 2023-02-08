using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    int speed;
    bool alive;

    private Health health; //this game object's health
    private Health playerHealth;
    private Animator anim;

    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private float damage;

    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float range; //adjust to extend the collider's area
    [SerializeField] float colliderDistance; //adjust to control the collider's distance from the game object


    void Awake() {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        health = GetComponent<Health>();
        cooldownTimer = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(alive) {
            if(health.GetHealth() <= 0) {
                alive = false;
                anim.SetTrigger("dying");
                health.Death();
            }
        }

        //Timers
        cooldownTimer += Time.deltaTime;

        if(PlayerDetected()) {
            if(cooldownTimer >= attackCooldown) {
                cooldownTimer = 0;
                anim.SetBool("pursuing", true);
                DamagePlayer();
            }
        }//end if player detected
    }

    bool PlayerDetected() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if(hit.collider != null) {
            playerHealth = hit.transform.GetComponent<Health>();
            print("Player Hit Triggered");
        }
        return hit.collider != null;
    }

    void DamageSelf() {
        //if hit by player, damaging obstacle
        health.TakeDamage(5); // health.TakeDamage(player.Attack.GetDamage())
    }

    void DamagePlayer() {
        print("Damage Player Triggered");
        if(PlayerDetected()) {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
