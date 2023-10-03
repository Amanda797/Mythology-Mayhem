using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl_Animator : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private float _Speed = 9f;
    [SerializeField] private GameObject playa;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemyLayer;
    //private float timing = 0.1f;
    private float freshDelay;
    public bool playerInRange = false;
    private bool awake = false;
    public bool idle = false;
    public bool attacking = false;
    [SerializeField] private int atkDamage;
    [SerializeField] private Transform Peck;
    [SerializeField] private float atkRange;
    private PlayerStats ps;

    private float xMovement;

    [SerializeField] private float playerRange = 1f;

    public float moveAttackSpeed = 5f;
    public float hitDelay = 0.5f;
    public int attackInterval = 7;

    void Start()
    {
        awake = true;
        thePlayer = FindObjectOfType<PlayerController>().gameObject;
        playa = FindObjectOfType<PlayerStats>().gameObject;
        ps = FindObjectOfType<PlayerStats>();
    }

    void OnAwake()
    {
        //thePlayer = FindObjectOfType<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
        //playa = FindObjectOfType<PlayerStats>();
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy" && ps.hitCount >= attackInterval && attacking == false )
        {
            ps.hitCount = 0;
            StartCoroutine(Attack(col.transform));
        }
    }
    void FixedUpdate()
    {
        if (attacking)
        {
            return;
        }

        if (awake == true)
        {
            idle = true;
        }
        /*if (Input.GetKeyDown(KeyCode.X) && awake == true && (Time.time >= freshDelay))
        {
            awake = false;
            freshDelay = Time.time + timing;
        }
        if(Input.GetKeyDown(KeyCode.X) && awake == false && (Time.time >= freshDelay))
        {
            awake = true;
            freshDelay = Time.time + timing;
        }*/
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange && Vector3.Distance(transform.position, thePlayer.transform.position) > 5 && awake == true && idle == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, _Speed * Time.deltaTime);
            
        }


       /* if (transform.position == thePlayer.transform.position && playa.GetComponent<PlayerStats>().flipped == true && awake == true && idle == true)
        {
            sr.flipX = true;
        }
        if (transform.position == thePlayer.transform.position && playa.GetComponent<PlayerStats>().flipped == false && awake == true && idle == true)
        {
            sr.flipX = false;
        }*/

        if (awake && idle)
        {
            sr.flipX = transform.position.x > thePlayer.transform.position.x;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }

    IEnumerator Attack(Transform target)
    {
        Debug.Log("Owl IS Attacking");
        attacking = true;
        bool attackComplete = false;

        while (!attackComplete)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _Speed * Time.deltaTime * moveAttackSpeed);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Peck.position, atkRange, enemyLayer);

            if (target == null)
            {
                attackComplete = true;
                Debug.Log("Owl did not do Damage");
            }

            foreach(Collider2D enemy in hitEnemies)
            {
                if (enemy.GetComponent<Enemy>() && enemy.GetComponent<KnockBackFeedback>())
                {
                    enemy.GetComponent<Health>().TakeDamage(atkDamage);
                    enemy.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
                    attackComplete = true;
                    Debug.Log("Owl did Damage");
                }
            }

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(hitDelay);

        Debug.Log("Owl IS Done Attacking");
        attacking = false;
        yield return null;


    }
    #region wastedtears
    //#region variables
    /*[Header("Owl Movement")]
    [SerializeField] private float forwardmovement = 200f;
    [SerializeField] private float climb = 9f;

    [Header("Owl Animation")]
    [SerializeField] private Animator anim;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    private float xMovement;
    private float yMovement;
    private bool climbing = false;

    private bool Attacking = false;
    private bool idle = false;

    private bool flipped = false;

    private Vector2 currentPosition;
    private Vector2 previousPosition;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform playa;*/

    //#endregion variables
    /* // Start is called before the first frame update
     void OnAwake()
     {
         sr = GetComponent<SpriteRenderer>();
     }

     // Update is called once per frame
     void Update()
     {
         Vector3 displacement = playa.position - transform.position;
         //displacement = displacement.normalized;
         if (Vector3.Distance(playa.position, transform.position) > 1.0f)
         {
             transform.position += (displacement * forwardmovement * Time.deltaTime);

             if (Input.GetKeyDown(KeyCode.X) && flipped == falseplayer.GetComponent<PlayerStats>().flipped == true)
             {
                 sr.flipX = true;
             }
             else if (Input.GetKeyDown(KeyCode.X) && flipped == true player.GetComponent<PlayerStats>().flipped == false)
             {
                 sr.flipX = false;
             }
         }
    private GameObject _target;
    private void Awake()
    {
        _target = FindGameObjectsWithTag("Player")().transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _Speed * Time.deltaTime);
    }
     }*/
    #endregion

}