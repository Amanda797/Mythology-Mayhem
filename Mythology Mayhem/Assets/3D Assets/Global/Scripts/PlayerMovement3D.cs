using System.Collections;
using UnityEngine;

public class PlayerMovement3D : MythologyMayhem
{
    //selected character
    public Character character;
    public LocalGameManager localGameManager;
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

    [Header("Tobias")]
    public float extraDamage;
    public float tobiasCurrentCooldown;
    public float tobiasCooldown;

    [Header("Amunet")]
    [SerializeField] private bool gliding;
    [SerializeField] private float maxGlideSpeed;
    [SerializeField] private float currentGlideStamina;
    [SerializeField] private float glideStamina;
    [SerializeField] private float glideStaminaUsage;
    [SerializeField] private GameObject Amunet3dCloud;

    [Header("Micos")]
    [SerializeField] private bool doubleJumped;

    [Header("Medusa")]
    public bool frozen;
    public float frozenTimer;
    public bool canPlayFootstepClip = true;

    void Start()
    {
        //Disable Cloud at Start
        if (character == Character.Amunet)
        {
            Amunet3dCloud.SetActive(false);
        }
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
        if (!frozen)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector2 input = new Vector2(x, z);
            if (playerAttack != null)
                playerAttack.SetSpeed(input.magnitude / 1.414f);

            Vector3 move = transform.right * x + transform.forward * z;
            if(move.sqrMagnitude > 1f){
                move = move.normalized;
            }

            controller.Move(move * speed * Time.deltaTime);

            // if the player is moving left or right
            if (input != Vector2.zero && canPlayFootstepClip && isGrounded)
            {
                canPlayFootstepClip = false;
                // randomly select a audio clip from the footstep clips array on the local game manager
                AudioClip clip = localGameManager.footstepClips[Random.Range(0, localGameManager.footstepClips.Length)];
                localGameManager.footstepAudioSource.clip = clip;
                localGameManager.footstepAudioSource.Play();
                StartCoroutine(ToggleFootStep());
                // play footstep audio 
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                if (playerAttack != null)
                {
                    playerAttack.Jump();
                    playerAttack.LandedEnd();
                }

            }

            switch (character)
            {
                case Character.Tobias:
                    velocity.y += gravity * Time.deltaTime;
                    break;

                case Character.Gorm:
                    velocity.y += gravity * Time.deltaTime;
                    break;

                case Character.Amunet:
                    AmunetGlideAbility();
                    break;

                case Character.Micos:
                    MicosDoubleJumpAbility();
                    //Continue to add gravity, ability will hard set velocity on double jump
                    velocity.y += gravity * Time.deltaTime;
                    break;
            }
        }
        else 
        {
            //Apply gravity, even if frozen
            velocity.y += gravity * Time.deltaTime;
            frozenTimer -= Time.deltaTime;
            if (frozenTimer <= 0) 
            {
                frozen = false;
            }
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

    void AmunetGlideAbility() 
    {

        //preset cloud to inactive if not gliding, before input check so a frame is not missed
        if (Amunet3dCloud.activeSelf && !gliding)
        {
            Amunet3dCloud.SetActive(false);
        }

        //check for reaching glide speed
        bool glideSpeedCheck = false;


        //Check for holding "Jump", while in the air, with enough enough Stamina
        if (Input.GetButton("Jump") && !isGrounded && currentGlideStamina > 0)
        {
            //display bool change and slower fall speed
            gliding = true;
            //check if falling faster than glide falling speed and clamp
            if (velocity.y < (maxGlideSpeed * -1))
            {
                //Drain glide stamina over time (Only when actually gliding, not while holding Jump
                currentGlideStamina -= Time.deltaTime;

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
            //Standard fall acceleration and show not gliding
            gliding = false;
            velocity.y += gravity * Time.deltaTime;
        }

        //Check if gliding
        if (gliding)
        {
            //If stamina reaches zero
            if (currentGlideStamina <= 0)
            {
                //Clamp so refill is exact
                currentGlideStamina = 0;
                //Standard fall acceleration and show not gliding
                gliding = false;
                velocity.y += gravity * Time.deltaTime;
            }
        }
        else 
        {
            //Only recharges while grounded
            if (isGrounded)
            {
                //Check if stamina not fully filled
                if (currentGlideStamina < glideStamina)
                {
                    //and fill
                    currentGlideStamina += Time.deltaTime;
                }
                //Check for overfill
                if (currentGlideStamina > glideStamina)
                {
                    //and clamp
                    currentGlideStamina = glideStamina;
                }
            }
        }

        //activate cloud first frame of gliding
        if (!Amunet3dCloud.activeSelf && gliding && glideSpeedCheck)
        {
            Amunet3dCloud.SetActive(true);
            Amunet3dCloud.GetComponent<AudioSource>().Play();
        }
    }

    void MicosDoubleJumpAbility() 
    {
        //For jump press, while in air, without another Double Jump this cycle
        if (Input.GetButtonDown("Jump") && !isGrounded && !doubleJumped)
        {
            //reset velocity to match original jump speed, hard setting to overwrite current fall speed
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (playerAttack != null)
            {
                playerAttack.Jump();
                playerAttack.LandedEnd();
            }
            //Set bool so Double Jump can not be performed again this cycle
            doubleJumped = true;
        }
    
        //Check for found contact after double jump
        if (isGrounded && doubleJumped)
        {
            //and reset for next jump cycle
            doubleJumped = false;
        }
    }
    IEnumerator ToggleFootStep()
    {
        float waitTime = .5f;

        yield return new WaitForSeconds(waitTime);
        canPlayFootstepClip = !canPlayFootstepClip;
    }
}
