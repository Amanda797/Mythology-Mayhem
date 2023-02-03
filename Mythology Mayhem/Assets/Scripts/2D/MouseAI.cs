using UnityEngine;

public class MouseAI : MonoBehaviour
{
    [Header("Mouse Movement")]
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float runSpeed = 15f;

    [Header("Attack Activation")]
    [SerializeField] private BoxCollider2D soundTrigger;
    [SerializeField] private GameObject attackTarget;

    [Header("Mouse Animations")]
    [SerializeField] private Animator mouseAnim;

    [Header("Mouse Stats")]
    [SerializeField] private float atkDamage = 1f;
    [SerializeField] public float health = 10f;

    private Rigidbody2D rb2d;
    private bool hitCheck = false;
    public BoxCollider2D enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (attackTarget == null) 
        {
            attackTarget = GameObject.FindWithTag("Player");
        }
        //enemyAttack = attackTarget.transform.Find("Damage Collider").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveMouse();
        FlipMouseSpriteToWalkDirection();
        AttackPlayer();
    }

    void MoveMouse()
    {
        // TODO: Random mouse patrolling
        return;
    }

    void FlipMouseSpriteToWalkDirection()
    {
        //TODO: Flip the mouse sprite in the direction it is walking
        return;
    }
    void AttackPlayer()
    {
        bool attackTrigger = soundTrigger.IsTouching(attackTarget.GetComponent<Collider2D>());
        if (attackTrigger)
        {
            //Debug.Log("ATTACK GREEK BOY!");
        }
        mouseAnim.SetBool("IsAttacking", attackTrigger);
    }
    public void TakeDamage(float damage)
    {
        health = health - damage;
    }

    void OnTriggerEnter2D(Collider2D playerAttack) {
        Debug.Log("hi");
        if (playerAttack.tag == "CanHit" && !hitCheck)
        {
            health -= 1;
            hitCheck = true;
            Debug.Log(health);
        }
    }
    private void OnTriggerStay2D(Collider2D playerAttack) {
        if (playerAttack.tag == "CanHit" && !hitCheck)
        {
            health -= 1;
            hitCheck = true;
            Debug.Log(health);
        }
    }
}
