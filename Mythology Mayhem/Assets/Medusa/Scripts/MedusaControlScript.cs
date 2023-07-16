using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaControlScript : MonoBehaviour
{
    //Public for testing, revert later
    public float Health;
    //Delay for Death animation to play
    public float deathDelay;
    public bool freeMove;
    //Forward and Side Speeds for BlendTree
    [Range(0, 1)]
    public float forwardSpeed;
    [Range(-1, 1)]
    public float sideSpeed;
    public Animator anim;

    //Boolean Triggers for Testing, Remove Later
    [Header ("Free Move Swithces")]
    public bool startFreeMove;
    public bool endFreeMove;
    [Header("Root Move Triggers")]
    public bool forwardRoot;
    public bool leftRoot;
    public bool rightRoot;
    [Header("Attack Triggers")]
    public bool attackCastSpell;
    public bool attackHairShake;
    public bool attackProjectile;
    public bool attackStab;
    public bool tryDefend;
    public bool takeDamage;
    public float testDamageAmount;

    public enum AttackType
    {
        CastSpell,
        HairShake,
        Projectile,
        Stab
    }
    public enum MoveType 
    { 
        Forward,
        Left,
        Right
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Calls Every Frame if in Free Move Mode
        //if (freeMove)
        RunFreeMoveBlend();

        //TestCode with Boolean Triggers, Remove Later
        if (startFreeMove) 
        {
            MedusaActivateFreeMove(true);
            startFreeMove = false;
        }
        if (endFreeMove)
        {
            MedusaActivateFreeMove(false);
            endFreeMove = false;
        }
        if (forwardRoot) 
        {
            MedusaMoveRoot(MoveType.Forward);
            forwardRoot = false;
        }
        if (leftRoot)
        {
            MedusaMoveRoot(MoveType.Left);
            leftRoot = false;
        }
        if (rightRoot)
        {
            MedusaMoveRoot(MoveType.Right);
            rightRoot = false;
        }
        if (attackCastSpell)
        {
            MedusaAttack(AttackType.CastSpell);
            attackCastSpell = false;
        }
        if (attackHairShake)
        {
            MedusaAttack(AttackType.HairShake);
            attackHairShake = false;
        }
        if (attackProjectile)
        {
            MedusaAttack(AttackType.Projectile);
            attackProjectile = false;
        }
        if (attackStab)
        {
            MedusaAttack(AttackType.Stab);
            attackStab = false;
        }
        if (tryDefend)
        {
            MedusaDefend();
            tryDefend = false;
        }
        if (takeDamage)
        {
            MedusaDamage(testDamageAmount);
            takeDamage = false;
        }
    }

    //Call to Switch Movement Style
    public void MedusaActivateFreeMove(bool isOn) 
    {
        freeMove = isOn;
        anim.SetBool("FreeMove", isOn);
    }

    //Call for Single Movement
    public void MedusaMoveRoot(MoveType _type)
    {
        //Check that we are not in Free Move before performing Attack
        if (!freeMove)
        {
            //Check Animator is assigned
            if (anim != null)
            {
                //Sets Trigger based on input Attack Type
                switch (_type)
                {
                    case MoveType.Forward:
                        anim.SetTrigger("Forward_Root");
                        break;

                    case MoveType.Left:
                        anim.SetTrigger("Left_Root");
                        break;

                    case MoveType.Right:
                        anim.SetTrigger("Right_Root");
                        break;
                }
            }
            else
            {
                print("Missing Animator on Medusa GameObject");
            }
        }
    }

    //Call for Attack
    public void MedusaAttack(AttackType _type) 
    {
        //Check that we are not in Free Move before performing Attack
        if (!freeMove)
        {
            //Check Animator is assigned
            if (anim != null)
            {
                //Sets Trigger based on input Attack Type
                switch (_type)
                {
                    case AttackType.CastSpell:
                        anim.SetTrigger("CastSpell");
                        CastSpell();
                        break;

                    case AttackType.HairShake:
                        anim.SetTrigger("HairShake");
                        HairShake();
                        break;

                    case AttackType.Projectile:
                        anim.SetTrigger("Projectile");
                        Projectile();
                        break;

                    case AttackType.Stab:
                        anim.SetTrigger("Stab");
                        Stab();
                        break;
                }
            }
            else 
            {
                print("Missing Animator on Medusa GameObject"); 
            }
        }
    }

    //Call for Defend
    public void MedusaDefend() 
    {
        if (!freeMove) 
        {
            if (anim != null) 
            {
                anim.SetTrigger("Defend");
                //Code for Defend
            }
        }
    }

    //Call for Damage (unsure if we are using integers or float damage
    public void MedusaDamage(float _amount)
    {
        Health -= _amount;
        if (Health <= 0) 
        {
            Destroy(this.gameObject, deathDelay);
            if (!freeMove)
            {
                if (anim != null)
                {
                    anim.SetTrigger("Death");
                    //Code for Death (other than Destroy, as that has been called)
                    return;
                }
            }
        }

        if (!freeMove)
        {
            if (anim != null)
            {
                anim.SetTrigger("Damage");
                //Code for Damage (other than Health subtraction, as that as run)
            }
        }
    }

    //Called in Update to Animate FreeMovement Based on Speed and Direction
    void RunFreeMoveBlend() 
    {
        //Set Forward Speed for Blend Tree to variable forwardSpeed, unless it is out of Range
        if (forwardSpeed <= 1 && forwardSpeed >= 0)
            anim.SetFloat("ForwardSpeed", forwardSpeed);
        else
            anim.SetFloat("ForwardSpeed", 0);

        //Set Side Speed for Blend Tree to variable sideSpeed, unless it is out of Range
        if (sideSpeed <= 1 && sideSpeed >= -1)
            anim.SetFloat("SideSpeed", sideSpeed);
        else
            anim.SetFloat("SideSpeed", 0);
    }

    void CastSpell() 
    { 
        //Code for Spell Mechanics
    }

    void HairShake()
    {
        //Code for Spell Mechanics
    }

    void Projectile()
    {
        //Code for Spell Mechanics
    }

    void Stab()
    {
        //Code for Spell Mechanics
    }
}
