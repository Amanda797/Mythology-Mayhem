using System.Collections;
using UnityEngine;

public class MouseAI : MonoBehaviour
{
    [Header("Enemy Type")]
    [SerializeField] bool isBoar;

    [Header("Special Animations")]
    [SerializeField] GameObject boarCloudAnimation;

    [Header("Mouse Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    private float ogWalkSpeed;
    private float ogRunSpeed;

    [Header("Attack Activation")]
    [SerializeField] private BoxCollider2D soundTrigger;
    [SerializeField] private GameObject attackTarget;

    [Header("Mouse Animations")]
    [SerializeField] private Animator mouseAnim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] float idleDuration = 5f;
    private float idleTimer = 0f;
    private bool movingLeft;

    [Header("Mouse Waypoints")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex = 0;

    [Header("MirrorActivatePlayerIsInRange")]
    public BoxCollider2D mirrorInRange;

    private Transform[] savedWaypoints;
    public Rigidbody2D rb2d;
    public bool dead = false;
    private bool attacking = false;
    private bool idle = false;
    private Vector2 currentPosition;
    private Vector2 previousPosition;
    private TwoDMirror twoDMirror;

    [SerializeField] string patrolBool = "IsPatrolling";

    // [SerializeField] private bool isFlying = false;

    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        mirrorInRange = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        savedWaypoints = waypoints;
        if (attackTarget == null) 
        {
            attackTarget = GameObject.FindWithTag("Player");
        }
        if(waypoints.Length != 0)
            transform.position = waypoints[waypointIndex].transform.position;
        sr = GetComponent<SpriteRenderer>();
        movingLeft = sr.flipX;
        currentPosition = transform.position;
        aud = GetComponent<AudioSource>();

        ogWalkSpeed = walkSpeed;
        ogRunSpeed = runSpeed;

        if(isBoar && boarCloudAnimation != null)
        {
            boarCloudAnimation.gameObject.SetActive(false);
            chargingTimer = gameObject.GetComponent<Enemy>().attackRate;
        }            

    }

    // Update is called once per frame
    void Update()
    {
        if (attackTarget == null) 
        {
            attackTarget = GameObject.FindWithTag("Player");
        }
        if (!dead)
        {
            previousPosition = currentPosition;
            currentPosition = transform.position;
            if (!attacking && !idle)
            {
                if(waypoints.Length != 0)
                    MoveMouse();
            }
            if (!attacking && idle)
            {
                Idle();
            }
            if (attackTarget != null)
            {
                AttackPlayer();
            }
            else 
            {
                
            }
            Flip();
        } else {
            if (boarCloudAnimation != null)
            {
                boarCloudAnimation.gameObject.SetActive(false);
            }
        }

    }

    void MoveMouse()
    {
        if (mouseAnim != null)
        {
            mouseAnim.SetBool(patrolBool, true);
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, walkSpeed * Time.deltaTime);
        Vector3 distance = new Vector3(transform.position.x - waypoints[waypointIndex].transform.position.x, transform.position.y - waypoints[waypointIndex].transform.position.y, transform.position.z - waypoints[waypointIndex].transform.position.z);
        if (Mathf.Abs(distance.x) < .3)
        {
            waypointIndex += 1;
            idle = true;
            if (mouseAnim != null)
            {
                mouseAnim.SetBool(patrolBool, false);
            }
            idleTimer = Time.time + idleDuration;
        }
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
                
    }

    void Flip()
    {
        if(!attacking) {
            Vector2 currentDirection = (currentPosition-previousPosition).normalized;
            if (currentDirection.x < 0)
            {
                sr.flipX = true;
            }
            else if (currentDirection.x > 0)
            {
                sr.flipX = false;
            }
        } else {
            Vector2 playerDirection = (attackTarget.transform.position-gameObject.transform.position).normalized;
            if (playerDirection.x < 0)
            {
                sr.flipX = true;
            }
            else if (playerDirection.x > 0)
            {
                sr.flipX = false;
            }
        }
    }

    float chargingTimer;

    void AttackPlayer()
    {
        attacking = soundTrigger.IsTouching(attackTarget.GetComponent<Collider2D>());
        if (attacking && gameObject.GetComponent<Enemy>().CanAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackTarget.transform.position, runSpeed * Time.deltaTime);
        } else 
        if (attacking && !gameObject.GetComponent<Enemy>().CanAttack) {
            if(isBoar && chargingTimer > gameObject.GetComponent<Enemy>().attackRate) {
                StartCoroutine(ChargingCloud());
                chargingTimer = 0f;
            } else if(isBoar) {
                chargingTimer += 1f * Time.deltaTime;
            }
        }
        mouseAnim.SetBool("IsAttacking", attacking);
    }

    IEnumerator ChargingCloud() {
        boarCloudAnimation.gameObject.SetActive(true);
        yield return new WaitForSeconds(gameObject.GetComponent<Enemy>().attackRate);
        boarCloudAnimation.gameObject.SetActive(false);
    }

    void Idle()
    {
        if (Time.time >= idleTimer)
        {
            idle = false;
        }
    }
    public void PlayHurt()
    {
        aud.Play();
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void SetMovementSpeed(float dividerValue)
    {
        walkSpeed /= dividerValue;
        runSpeed /= dividerValue;
    }

    public void ResetMovementSpeed()
    {
        walkSpeed = ogWalkSpeed;
        runSpeed = ogRunSpeed;
    }
}
