using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackHeight = 3f;
    [SerializeField] private LayerMask enemyLayers;

    [Header("Player Stats")]
    [SerializeField] private int atkDamage = 2;
    [SerializeField] public PlayerHeartState phs;
    [SerializeField] public int MaxHealth { get; private set; }
    [SerializeField] public int CurrHealth { get; private set; }
    [SerializeField] public int MaxMana { get; private set; }
    [SerializeField] public int CurrMana { get; private set; }
    [SerializeField] private float attackRate = 2f;
    public float NextAttackTime { get; private set; }
    [SerializeField] private bool canAttack;

    [Header("Player Animation")]
    [SerializeField] private Animator anim;

    private SpriteRenderer sr;
    public bool flipped = false;
    private AudioSource aud;    

    // Start is called before the first frame update
    void Awake()
    {
        MaxHealth = 16;
        MaxMana = 10;

        CurrHealth = MaxHealth;
        CurrMana = MaxMana;

        sr = GetComponent<SpriteRenderer>();
        NextAttackTime = 0f;

        //phs = FindObjectOfType<PlayerHeartState>();

        //phs.PlayerCurrHealth = CurrHealth;
        //phs.PlayerMaxHealth = MaxHealth;
        //aud = GetComponent<AudioSource>();

        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= NextAttackTime && canAttack) 
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
                flipped = !flipped;
            }
        } 
        else 
        {
            if (flipped)
            {
                attackPoint.localPosition = attackPoint.localPosition * new Vector2(-1,1);
                flipped = !flipped;
            }
        }
    

    }    

    public void ToggleAttack() {
        canAttack = !canAttack;
    }//end toggle can attack

    private void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCapsuleAll(attackPoint.position, new Vector2(attackRange, attackRange+attackHeight), CapsuleDirection2D.Vertical, 0f, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<Enemy>() && enemy.GetComponent<KnockBackFeedback>()) {
                enemy.GetComponent<Enemy>().TakeDamage(atkDamage);
                enemy.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            }
        }
    }

    public void TakeDamage(int damage) 
    {
        if(CurrHealth > 0)
        {
            
            CurrHealth -= Mathf.Abs(damage);
            
            if(phs != null)
                phs.PlayerCurrHealth = CurrHealth;
        }

        anim.SetTrigger("Hurt");

        if(CurrHealth <= 0)
        {
            if(phs != null)
                phs.PlayerCurrHealth = CurrHealth;
            Die();
        }
    }

    public void Heal(int heal) 
    {
        if(CurrHealth < MaxHealth)
        {
            CurrHealth += Mathf.Abs(heal);
            if(phs != null)
                phs.PlayerCurrHealth = CurrHealth;
        }

        

        if(CurrHealth >= MaxHealth)
        {
            CurrHealth = MaxHealth;
            if(phs != null)
                phs.PlayerCurrHealth = CurrHealth;
        }
    }

    private void Die()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<KnockBackFeedback>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        this.enabled = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange/2);
        Vector3 heightPoint = new Vector3 (attackPoint.position.x, attackPoint.position.y + attackHeight/2, attackPoint.position.z);
        Gizmos.DrawWireSphere(heightPoint, attackRange/2);
        heightPoint = new Vector3 (attackPoint.position.x, attackPoint.position.y - attackHeight/2, attackPoint.position.z);
        Gizmos.DrawWireSphere(heightPoint, attackRange/2);
    }
    /*public void PlaySwordSwing()
    {
        aud.Play();
    }*/
}
