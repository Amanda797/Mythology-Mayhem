using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Player Movement")]
    [SerializeField] private float walkSpeed = 200f;
    [SerializeField] private float runSpeed = 300f;

    [Header("Player Animation")]
    [SerializeField] private Animator anim;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    private float xMovement;
    private float yMovement;
    private bool isRunning = false;
    public bool climbing = false;
    public bool ladderEntered = false;
    public bool pushing = false;
    #endregion Variables


    #region Unity Methods
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if (ladderEntered)
        {
            if (Input.GetKeyDown(KeyCode.E) && !climbing)
            {
                anim.SetBool("IsClimb", true);
                climbing = true;
                rb2d.gravityScale = 0;
                rb2d.velocity = new Vector2(0,0);
                Debug.Log("Start Climbing");
            }
            else if (Input.GetKeyDown(KeyCode.E) && climbing)
            {
                anim.SetBool("IsClimb", false);
                climbing = false;
                rb2d.gravityScale = 1;
                Debug.Log("Stop Climbing");
            }
        }
        FlipPlayerSpriteWithMoveDirection();
        AnimatePlayer();
    }

    private void FixedUpdate()
    {
        if (!isRunning)
        {
            MovePlayer(walkSpeed);
        }
        else
        {
            MovePlayer(runSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Ladder")
        {
            ladderEntered = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            anim.SetBool("IsClimb", false);
            ladderEntered = false;
            rb2d.gravityScale = 1;
            if (climbing)
            climbing = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.collider.tag == "PushBlock" )
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                Debug.Log("Test");
                if (!pushing)
                {
                    other.gameObject.transform.SetParent(gameObject.transform);
                }
                else
                {

                }
            }
            
        }
    }

    #endregion Unity Methods


    #region Self-defined Methods
    private bool GetInput()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        return xMovement != 0;
    }

    private void FlipPlayerSpriteWithMoveDirection()
    {
        if (xMovement < 0)
        {
            sr.flipX = true;
        }
        else if (xMovement > 0)
        {
            sr.flipX = false;
        }
    }

    private void MovePlayer(float speed)
    {
        if (climbing) 
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x , (yMovement * speed) * Time.deltaTime);
        } 
        else
        {
            rb2d.velocity = new Vector2((xMovement * speed) * Time.deltaTime, rb2d.velocity.y);
        }
        
    }

    private void AnimatePlayer()
    {
        if (xMovement != 0)
        {
            anim.SetBool("IsWalking", GetInput());
            anim.SetBool("IsRunning", isRunning);
        }
        else
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
        }
    }
    #endregion Self-defined Methods
}
