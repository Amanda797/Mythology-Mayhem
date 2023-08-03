using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement3D playerMovement;
    public Animator anim;
    public AnimationClip attackAnim;
    public Attack3D attack3D;

    public SwordEffectScript effectsScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && attack3D.GetIsAttacking() == false)
        {
            anim.Play(attackAnim.name);
            effectsScript.vfxAnimator.SetTrigger("StartEffect");
        }

        
    }
    public void Attack()
    {
        attack3D.Attack();
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
}
