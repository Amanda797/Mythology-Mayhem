using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private Transform owlPoint;

    [Header("Player Stats")]
    [SerializeField] HealthUIController huic;
    [SerializeField] public PlayerStats_SO ps;

    [Header("Player Animation")]
    [SerializeField] private Animator anim;

    private SpriteRenderer sr;
    public bool flipped = false;
    private AudioSource aud;    

    // Start is called before the first frame update
    void Awake()
    {        
        sr = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();
        huic = GameObject.FindGameObjectWithTag("huic").GetComponent<HealthUIController>();

        if(huic != null)
            ps = huic.ps;
        else{
            print("Can't find huic's player stats so");
        }
    }//end on awake

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= ps.NextAttackTime && ps.CanAttack) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                ps.NextAttackTime = Time.time + 1f/ps.AttackRate;
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

    public void ToggleAttack() {
        ps.CanAttack = !ps.CanAttack;
    }//end toggle can attack

    private void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCapsuleAll(attackPoint.position, new Vector2(ps.AttackRange, ps.AttackRange+ps.AttackHeight), CapsuleDirection2D.Vertical, 0f, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<Enemy>() && enemy.GetComponent<KnockBackFeedback>()) {
                enemy.GetComponent<Enemy>().TakeDamage(ps.AttackDamage);
                enemy.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            }
        }
    }

    public void TakeDamage(int damage) 
    {
        if(ps.CurrHealth >= 0)
        {
            ps.CurrHealth -= Mathf.Abs(damage);
            anim.SetTrigger("Hurt");
            if(huic != null)
                huic.PlayerCurrHealth = ps.CurrHealth;
            if(ps.CurrHealth <= 0)
            {
                Die();
            }
        }
    }//end take damage

    public void Heal(int heal) 
    {
        if(ps.CurrHealth < ps.MaxHealth)
        {
            ps.CurrHealth += Mathf.Abs(heal);
            if(huic != null)
                huic.PlayerCurrHealth = ps.CurrHealth;
        }
        else if(ps.CurrHealth >= ps.MaxHealth)
        {
            ps.CurrHealth = ps.MaxHealth;
            if(huic != null)
                huic.PlayerCurrHealth = ps.CurrHealth;
        }
    }//end heal

    private void Die()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<KnockBackFeedback>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        this.enabled = false;

        ps.CurrHealth = ps.MaxHealth;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, ps.AttackRange/2);
        Vector3 heightPoint = new Vector3 (attackPoint.position.x, attackPoint.position.y + ps.AttackHeight/2, attackPoint.position.z);
        Gizmos.DrawWireSphere(heightPoint, ps.AttackRange/2);
        heightPoint = new Vector3 (attackPoint.position.x, attackPoint.position.y - ps.AttackHeight/2, attackPoint.position.z);
        Gizmos.DrawWireSphere(heightPoint, ps.AttackRange/2);
    }

    public void PlaySwordSwing()
    {
        aud.Play();
    }
    
}
