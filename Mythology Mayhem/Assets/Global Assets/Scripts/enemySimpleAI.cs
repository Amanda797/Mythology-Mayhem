using UnityEngine;

public class enemySimpleAI : MonoBehaviour
{
    public float attackDamage = 2f;
    public float speed = 3f;   // Movement speed of the enemy
    public float attackRange = 2f;   // Distance from which the enemy can attack the player
    public float attackDelay = 1f;   // Delay between enemy attacks
    public Animator animator;   // Reference to the animator component
    private Transform player;   // Reference to the player's transform
    private bool isAttacking;   // Flag to determine if the enemy is currently attacking
    private float lastAttackTime;   // Time when the enemy last attacked
    private bool isHurt = false;
    private float HurtTimeDelay;
    public float hurtDelay = 1f;
    bool isDead = false;
    public Health health;

    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj.scene.name == gameObject.scene.name) player = obj.transform;
        }
    }

    void Update()
    {
        if(health.GetHealth() <= 0 && health.GetHealth() != -1000)
        {
            Die();
        }

        if(isHurt)
        {
            isAttacking = false;
            animator.SetFloat("Speed", 0f);
            HurtTimeDelay -= Time.deltaTime;
            if(HurtTimeDelay <= 0)
            {
                isHurt = false;
            }
        }

        if (!isAttacking && !isHurt&& !isDead)
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x,transform.position.y,player.position.z), speed * Time.deltaTime);

            // Rotate towards the player
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);

            // Check if the enemy is close enough to attack
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Start attacking
                isAttacking = true;
                //lastAttackTime = attackDelay;
                //Attack();
            }
            animator.SetFloat("Speed", speed);
        }
        else if(isAttacking && !isHurt && !isDead)
        {
            animator.SetFloat("Speed", 0f);
            if (Vector3.Distance(transform.position, player.position) >= attackRange)
            {
                isAttacking = false;
                return;
            }
            
            lastAttackTime -= Time.deltaTime;

            
            // Check if enough time has passed since the last attack
            if (lastAttackTime <= 0)
            {
                // Attack again
                Attack();
                lastAttackTime = attackDelay;
                
            }
            
        }
    }

    void Attack()
    {
        // Play attack animation or perform attack action
        player.GetComponent<FPSHealth>().TakeDamage(attackDamage);
        animator.SetTrigger("Attack");
    }
    public void Hurt()
    {
        isHurt = true;
        HurtTimeDelay = hurtDelay;
        isAttacking = false;
        //knockback enemy when hit
        Vector3 direction = transform.position - player.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
    }
    public void Die()
    {
        isDead = true;
        animator.SetFloat("Speed", 0f);
        health.Death();
    }
}

