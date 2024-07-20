using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    #region Variables
    [Header("Player Movement")]
    [SerializeField] private float walkSpeed = 200f;
    [SerializeField] private float runSpeed = 300f;
    [SerializeField] private float jumpAmount = 9f;
    [SerializeField] private AudioSource footstepsSFX;

    [Header("Player Animation")]
    [SerializeField] private Animator anim;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    private float xMovement;
    private float yMovement;
    private bool isRunning = false;
    public bool climbing = false;
    public bool ladderEntered = false;
    public bool grounded = false;
    
    [SerializeField] private GameObject pushBlock;
    public bool pushing = false;
    public bool canPush = false;

    public float XMovement { get => xMovement; set => xMovement = value; }
    public AudioSource FootstepsSFX { get => footstepsSFX; set => footstepsSFX = value; }
    #endregion Variables


    #region Unity Methods
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        anim.SetBool("IsGrounded", true);

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsGrounded", grounded);
        GetInput();
        if (ladderEntered)
        {
            if (Input.GetKeyDown(KeyCode.E) && !climbing)
            {
                gameManager.Popup("", false);
                anim.SetBool("IsClimb", true);
                climbing = true;
                rb2d.gravityScale = 0;
                rb2d.velocity = new Vector2(0,0);
            }
            else if (Input.GetKeyDown(KeyCode.E) && climbing)
            {
                anim.SetBool("IsClimb", false);
                climbing = false;
                rb2d.gravityScale = 1;
            }
        }
        if(canPush || pushing)
        {
            if (Input.GetKeyDown(KeyCode.E) && grounded) 
            {
                gameManager.Popup("", false);
                if (!pushing)
                {
                    pushing = true;
                    pushBlock.transform.SetParent(gameObject.transform);
                    pushBlock.transform.localPosition = new Vector3(4,-.2f,0); //offset new position
                    walkSpeed = 100;
                    anim.SetBool("IsPush", true);
                }
                else
                {
                    pushBlock.transform.parent = null;
                    walkSpeed = 200;
                    anim.SetBool("IsPush", false);
                    pushing = false;
                }
            }
        }

        FlipPlayerSpriteWithMoveDirection();
        AnimatePlayer();
        
        if (rb2d.velocity == new Vector2(0,0))
        {
            if (climbing || pushing)
            {
                anim.speed = 0;
            }
        }
        else
        {
            anim.speed = 1;
        }
        
        // Jump code
        if (!ladderEntered && !pushing && grounded) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
                anim.SetTrigger("Jump");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isRunning)
        {
            MovePlayer(walkSpeed);
        }
        else
        {
            if(!pushing && !climbing) 
            {
                MovePlayer(runSpeed);
            }
            else
            {
                MovePlayer(walkSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Ladder")
        {
            gameManager.Popup("Press E to Climb", true);
            ladderEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            gameManager.Popup("", false);
            ladderEntered = false;

            if (climbing)
            {
                climbing = false;
                anim.SetBool("IsClimb", false);
                rb2d.gravityScale = 1;
            }
        }
        if (other.tag == "PushBlock")
        {
            gameManager.Popup("", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.tag == "PushBlock")
        {
            gameManager.Popup("Press E to Move", true);
            canPush = true;
            if (!pushing) pushBlock = other.gameObject;
        }
        if (other.collider.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.collider.tag == "PushBlock")
        {
            gameManager.Popup("", false);
            canPush = false;
            if (!pushing) pushBlock = null;
        }
        if (other.collider.tag == "Ground")
        {
            grounded = false;
        }
    }

    #endregion Unity Methods


    #region Self-defined Methods
    private bool GetInput()
    {
        XMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        return XMovement != 0;
    }

    private void FlipPlayerSpriteWithMoveDirection()
    {
        if(!pushing) 
        {
            if (XMovement < 0)
            {
                sr.flipX = true;
            }
            else if (XMovement > 0)
            {
                sr.flipX = false;
            }
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
            rb2d.velocity = new Vector2((XMovement * speed) * Time.deltaTime, rb2d.velocity.y);
        }
        
    }

    private void AnimatePlayer()
    {
        if (XMovement != 0)
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
