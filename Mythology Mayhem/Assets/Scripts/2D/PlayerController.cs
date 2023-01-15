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
        FlipPlayerSpriteWithMoveDirection();
        PlayerAttack();
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
    #endregion Unity Methods


    #region Self-defined Methods
    private bool GetInput()
    {
        xMovement = Input.GetAxis("Horizontal");
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
        rb2d.velocity = new Vector2((xMovement * speed) * Time.deltaTime, rb2d.velocity.y);
    }

    private bool PlayerAttack()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
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

        anim.SetBool("IsAttacking", PlayerAttack());
    }
    #endregion Self-defined Methods
}
