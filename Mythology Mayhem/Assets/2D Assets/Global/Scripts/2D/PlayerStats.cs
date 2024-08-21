using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    GameManager gameManager;
    public AudioSource swordAttackAS;
    public AudioSource bowAttackAS;
    public AudioSource hurtAS;
    public AudioSource dieAS;
    public AudioSource respawnAS;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private Transform owlPoint;
    [SerializeField] private GameObject owlPrefab;


    [Header("Player Stats")]
    [SerializeField] public HealthUIController huic;
    [SerializeField] public PlayerStats_SO ps;

    [Header("Player Animation")]
    [SerializeField] private Animator anim;

    private SpriteRenderer sr;
    public bool flipped = false;
    private bool respawning;

    private GameObject owl;
    [HideInInspector]public int hitCount;

    public PlayerAttach attachScript;
    // Start is called before the first frame update

    Vector2 spawnPoint = Vector2.zero;
    void Awake()
    {        
        sr = GetComponent<SpriteRenderer>();
        huic = GameObject.FindGameObjectWithTag("huic").GetComponent<HealthUIController>();

        huic.UpdateHealth();
        if (huic != null) { 
            ps = huic.ps;
            ps.CanAttack = true;
            ps.NextAttackTime = 0;
            //ps.CurrHealth = ps.MaxHealth;
        }
        else Debug.LogWarning("Can't find huic's player stats so");
    }//end on awake

    private void Start()
    {
        spawnPoint = transform.position;
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= ps.NextAttackTime && ps.CanAttack) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(Time.timeScale == 1)
                {
                    Attack();
                    ps.NextAttackTime = Time.time + 1f / ps.AttackRate;
                }
            }
        }

        //The Code below is to flip the attackPoint of the player so that if the player is flipped they can still attack behind the enemy
        if (sr.flipX)
        {
            if (!flipped)
            {
                attackPoint.localPosition = attackPoint.localPosition * new Vector2(-1,1);
                if(owlPoint != null)
                    owlPoint.localPosition = owlPoint.localPosition * new Vector2(-1, 1);
                flipped = !flipped;
            }
        } 
        else 
        {
            if (flipped)
            {
                attackPoint.localPosition = attackPoint.localPosition * new Vector2(-1,1);
                if(owlPoint != null)
                    owlPoint.localPosition = owlPoint.localPosition * new Vector2(-1, 1);
                flipped = !flipped;
            }
        }

    }   

    public void SpawnOwl() 
    {
        if(owl == null)
        {
            Instantiate(owlPrefab, owlPoint.position, Quaternion.identity);
        }
    } 

    public void ToggleAttack() {
        ps.CanAttack = !ps.CanAttack;
    }//end toggle can attack

    private void Attack()
    {
        if (anim.GetBool("IsDead")) return;
        anim.SetBool("UseSword", true);
        anim.SetTrigger("Attack");
        swordAttackAS.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCapsuleAll(attackPoint.position, new Vector2(ps.AttackRange, ps.AttackRange+ps.AttackHeight), CapsuleDirection2D.Vertical, 0f, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<Enemy>() && enemy.GetComponent<KnockBackFeedback>())
            {
                hitCount++;
                if (!enemy.GetComponent<Health>().isDead)
                {
                    enemy.GetComponent<Health>().TakeDamage(ps.AttackDamage);
                    enemy.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
                }
            }
        }
    }
    public void TakeDamage(float damage) 
    {
        if (anim.GetBool("IsDead")) return;
        if(gameManager.gameData.curHealth > 0)
        {
            gameManager.gameData.curHealth = Mathf.Clamp(gameManager.gameData.curHealth -= damage, 0, gameManager.gameData.maxHealth);
            anim.SetTrigger("Hurt");

            huic.UpdateHealth();

            if (gameManager.gameData.curHealth <= 0) Die();
            else hurtAS.Play();
        }
    }

    public void Heal(int heal, bool potion) 
    {
        gameManager.gameData.curHealth = Mathf.Clamp(gameManager.gameData.curHealth += heal, 0, gameManager.gameData.maxHealth);
        huic.UpdateHealth();
    }

    private void Die()
    {
        if (!anim.GetBool("IsDead"))
        {
            Debug.Log("Die");
            anim.SetBool("IsDead", true);
            dieAS.Play();
            GetComponent<PlayerController>().enabled = false;
            StartCoroutine(Respawn());
        }
    }
    public IEnumerator Respawn() 
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("IsDead", false);
        transform.position = spawnPoint;
        Heal((int)gameManager.gameData.maxHealth, false);
        GetComponent<PlayerController>().enabled = true;
        anim.SetBool("IsDead", false);
        anim.Play("Greek-Boy-Idle", 0);
        respawnAS.Play();
    }   
    
    public void CollectHeart(int amount)
    {
        gameManager.gameData.maxHealth += amount;
        gameManager.gameData.curHealth += amount;
        huic.UpdateHealth();
    }
}
