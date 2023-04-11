using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
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

    [Header("Player Animation")]
    [SerializeField] private Animator anim;

    private SpriteRenderer sr;
    private bool flipped = false;
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

        phs = FindObjectOfType<PlayerHeartState>();

        phs.PlayerCurrHealth = CurrHealth;
        phs.PlayerMaxHealth = MaxHealth;
        aud = GetComponent<AudioSource>();
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

    private void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(atkDamage);
            enemy.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
        }
    }

    public void TakeDamage(int damage) 
    {
        if(CurrHealth > 0)
        {
            CurrHealth -= Mathf.Abs(damage);
            phs.PlayerCurrHealth = CurrHealth;
        }

        anim.SetTrigger("Hurt");

        if(CurrHealth <= 0)
        {
            phs.PlayerCurrHealth = CurrHealth;
            Die();
        }
    }

    public void Heal(int heal) 
    {
        if(CurrHealth < MaxHealth)
        {
            CurrHealth += Mathf.Abs(heal);
            phs.PlayerCurrHealth = CurrHealth;
        }

        anim.SetTrigger("Hurt");

        if(CurrHealth >= MaxHealth)
        {
            CurrHealth = MaxHealth;
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

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void PlaySwordSwing()
    {
        aud.Play();
    }
}
