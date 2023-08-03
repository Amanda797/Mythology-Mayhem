using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    //selected character
    [SerializeField] private CharacterType character;

    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -19.62f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDist = .4f;
    [SerializeField] private LayerMask groundMask;

    public PlayerAttack playerAttack;

    private Vector3 velocity;
    
    bool isGrounded;

    [Header("Amunet")]
    [SerializeField] private bool gliding;
    [SerializeField] private float maxGlideSpeed;
    [SerializeField] private GameObject Amunet3dCloud;

    //Character types 
    enum CharacterType 
    { 
        Tobias,
        Gorm,
        Amunet,
        Micos
    }

    void Start()
    {
        //Disable Cloud at Start
        Amunet3dCloud.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        bool lastGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (isGrounded && !lastGrounded)
        {
            if(playerAttack != null)
            playerAttack.Landed();
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(x, z);
        if(playerAttack != null)
        playerAttack.SetSpeed(input.magnitude/1.414f);

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (playerAttack != null)
            {
                playerAttack.Jump();
                playerAttack.LandedEnd();
            }

        }

        //preset cloud to inactive if not gliding, before input check so a frame is not missed
        if(Amunet3dCloud.activeSelf && !gliding) 
        {
            Amunet3dCloud.SetActive(false);
        }

        //check for reaching glide speed
        bool glideSpeedCheck = false;

        //Check for holding "Jump", while in the air, as amunet
        if (Input.GetButton("Jump") && !isGrounded && character == CharacterType.Amunet)
        {
            //display bool change and slower fall speed
            gliding = true;
            //check if falling faster than glide falling speed and clamp
            if (velocity.y < (maxGlideSpeed * -1))
            {
                velocity.y = -1 * maxGlideSpeed;
                glideSpeedCheck = true;
            }
            else 
            {
                velocity.y += gravity * Time.deltaTime;
            }
        }
        else
        {
            //standard fall speed and show not gliding
            gliding = false;
            velocity.y += gravity * Time.deltaTime;
        }

        //activate cloud first frame of gliding
        if (!Amunet3dCloud.activeSelf && gliding && glideSpeedCheck)
        {
            Amunet3dCloud.SetActive(true);
            Amunet3dCloud.GetComponent<AudioSource>().Play();
        }

        controller.Move(velocity * Time.deltaTime);
    }
    void LateUpdate()
    {
        if(playerAttack != null)
        {
            playerAttack.LandedEnd();
            playerAttack.JumpEnd();
        }
    }
}
