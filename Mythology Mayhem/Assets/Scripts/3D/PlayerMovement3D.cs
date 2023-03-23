using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
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

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if(playerAttack != null){
                playerAttack.Jump();
                playerAttack.LandedEnd();
            }
            
        }

        velocity.y += gravity * Time.deltaTime;
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
