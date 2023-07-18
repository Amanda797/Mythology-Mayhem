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
    private float timing = 0.1f;
    private float freshDelay;
    public bool playerInRange = false;
    private bool awake = false;
    public bool idle = false;
    public bool attacking = false;
    [SerializeField] private int atkDamage;
    [SerializeField] private Transform Peck;
    [SerializeField] private float atkRange;

    private float xMovement;

    [SerializeField] private float playerRange = 1f;

    void Start()
    {
        awake = true;
    }

    void OnAwake()
    {
        //thePlayer = FindObjectOfType<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
        //playa = FindObjectOfType<PlayerStats>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Attack();
        }
    }
    void Update()
    {
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
        if (playerInRange && awake == true && idle == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, _Speed * Time.deltaTime);
        }


        if (transform.position == thePlayer.transform.position && playa.GetComponent<PlayerStats>().flipped == true && awake == true && idle == true)
        {
            sr.flipX = true;
        }
        if (transform.position == thePlayer.transform.position && playa.GetComponent<PlayerStats>().flipped == false && awake == true && idle == true)
        {
            sr.flipX = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Peck.position, atkRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>())
            {
                enemy.GetComponent<Enemy>().TakeDamage(atkDamage);
            }
        }
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