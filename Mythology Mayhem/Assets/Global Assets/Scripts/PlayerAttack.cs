using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MythologyMayhem
{
    public PlayerMovement3D playerMovement;
    public Animator anim;
    public AnimationClip attackAnim;
    public Attack3D attack3D;
    public float mainHandDamage = 10f;
    public bool canAttack = true;
    [SerializeField] float attackSpeed = .5f;
    public SwordEffectScript effectsScript;
    // Start is called before the first frame update
    void Start()
    {
        attack3D.damage = mainHandDamage;
    }

    // Update is called once per frame
    void Update()
    {
        //Run Tobias Cooldown
        TobiasDamageCooldown();

        if (!playerMovement.frozen && canAttack)
        {
            if (Input.GetMouseButtonDown(0) && !attack3D.GetIsAttacking())
            {
                if (Time.timeScale == 1)
                {
                    canAttack = false;

                    //Store Original Damage
                    float baseDamage = attack3D.damage;

                    //Tobias and cooldown complete
                    if (playerMovement.character == Character.Tobias && playerMovement.tobiasCurrentCooldown <= 0)
                    {
                        //Add extra damage, start vfxs, and set cooldown to max
                        attack3D.damage += playerMovement.extraDamage;
                        effectsScript.vfxAnimator.SetTrigger("StartEffect");
                        playerMovement.tobiasCurrentCooldown = playerMovement.tobiasCooldown;
                    }
                    //Run Attack()
                    Attack();

                    //Restore Original in case Changed (Outside if in case other damage changes are added later
                    attack3D.damage = baseDamage;
                }

            }
        }
        
    }
    IEnumerator ToggleCanAttack()
    {
        anim.Play(attackAnim.name);
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
        AttackEnd();
    }
    public void Attack()
    {
        attack3D.Attack();
        
        StartCoroutine(ToggleCanAttack());
    }
    public void AttackEnd()
    {
        attack3D.StopAttack();
    }
    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
    public void JumpEnd()
    {
        anim.ResetTrigger("Jump");
    }
    public void Landed()
    {
        anim.SetTrigger("Landed");
    }
    public void LandedEnd()
    {
        anim.ResetTrigger("Landed");
    }
    public void SetSpeed(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    void TobiasDamageCooldown() 
    {
        //If cooldown still remaining
        if (playerMovement.tobiasCooldown > 0)
        {
            //Sub time
            playerMovement.tobiasCurrentCooldown -= Time.deltaTime;
            //If understeps zero
            if (playerMovement.tobiasCurrentCooldown <= 0)
            {
                //Clamp
                playerMovement.tobiasCurrentCooldown = 0;              
            }
        }
    }
}
