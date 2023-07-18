using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MedusaControlScript : MonoBehaviour
{
    [SerializeField] AttackStates CurrentState;

    //Public for testing, revert later
    public float Health;
    //Delay for Death animation to play
    public float deathDelay;
    //public bool freeMove;
    //Forward and Side Speeds for BlendTree
    [Range(0, 1)]
    public float forwardSpeed;
    [Range(-1, 1)]
    public float sideSpeed;
    public Animator anim;
    [SerializeField] NavMeshAgent medusaAgent;
    [SerializeField] Health playerHealth;

    [Header("Attack Triggers")]
    public bool attackCastSpell;
    public bool attackHairShake;
    public bool attackProjectile;
    public bool attackStab;
    public bool tryDefend;
    public bool takeDamage;
    public float testDamageAmount;

    [Header("Timers")]
    [SerializeField] float timer = 5f;
    float cooldown;
    [SerializeField] float lastTime;
    [SerializeField] private float player1Timer;
    private int timesAttacked;
    private int maxAttacks;

    [Header("Lists")]
    [SerializeField] Transform MainPlatform;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] List<AttackType> meleeAttacks;
    int currentMelee = 0;
    [SerializeField] List<AttackType> rangedAttacks;
    int currentRanged = 0;
    private bool activatedRange = false;
    [SerializeField] float rangedAttackDist = 20f;
    [SerializeField] float meleeAttackDist = 5f;

    public enum AttackType
    {
        CastSpell,
        HairShake, 
        Projectile,
        Stab
    }

    public enum AttackStates {
        Start,
        PlayerChase,
        RandomPlatform,
        PlayerChase2,
        RemainingPlatform,
        PlayerChase3,
        MainPlatform,
        Final
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        CurrentState = AttackStates.Start;
    }

    // Update is called once per frame
    void Update()
    {
        //Calls Every Frame if in Free Move Mode
        RunFreeMoveBlend();

        // Check Attack Stage
        switch(CurrentState) {
            case AttackStates.Start: {
                //If at the Start, Medusa should persue the player from the Main Platform. Change state to PlayerChase.
                //Could add cutsceene here
                //Timer
                if(timer <= Time.time) {
                    CurrentState = AttackStates.PlayerChase;
                    lastTime = Time.time;
                }
                break;
            }
            case AttackStates.PlayerChase: {
                //Timer
                medusaAgent.SetDestination(playerHealth.gameObject.transform.position); //Testing

                if(player1Timer <= Time.time - lastTime || timesAttacked > maxAttacks) {
                    CurrentState = AttackStates.RandomPlatform;
                    medusaAgent.SetDestination(waypoints[ChooseDestination()].position);
                    lastTime = Time.time;
                }
                break;
            }
            case AttackStates.RandomPlatform: {
                //Check where Medusa is
                if(medusaAgent.pathStatus == NavMeshPathStatus.PathComplete && medusaAgent.remainingDistance < (medusaAgent.stoppingDistance + .5f)) {
                    print(medusaAgent.remainingDistance);
                    print(medusaAgent.pathStatus);

                    //Turn medusa to face player, check to see if rotation is complete
                    Vector3 playerPos = playerHealth.gameObject.transform.position;
                    Vector3 medusaForward = gameObject.transform.forward;
                    float angle = Vector3.SignedAngle(playerPos, medusaForward, Vector3.up);
                    int angleOfAccuracy = 10;

                    //If Medusa's cone of vision is within [-angleOfAccuracy 0 angleOfAccuracy]... 
                    if(angle <= angleOfAccuracy && angle >= -angleOfAccuracy) {
                        Vector3 distance = medusaAgent.gameObject.transform.position - playerPos;

                        //...Check if she's within range to attack the player, and if within range, attack once
                        if(distance.x <= rangedAttackDist && distance.z <= rangedAttackDist)
                        {
                            //Activate Ranged Attacks
                            MedusaAttack(rangedAttacks[currentRanged]);
                            //Increment Next Attack in Sequence
                            currentRanged++;
                            if(currentRanged > rangedAttacks.Count) {
                                currentRanged = 0;
                            }
                        }//end distance check

                        //Q: Should she back up some or stay within range, leaving setting distance up to the player?
                                               
                    }//end angle check
                }//if path complete
                break;
            }
            case AttackStates.PlayerChase2: {    
                //Change Medusa Properties
                //Function()

                //Timer
                if(player1Timer <= Time.time - lastTime || timesAttacked > maxAttacks) {
                    CurrentState = AttackStates.RemainingPlatform;
                    medusaAgent.SetDestination(waypoints[0].position);
                    lastTime = Time.time;
                }
                break;
            }
            case AttackStates.RemainingPlatform: {
                //Reset the attack list to Zero

                //Check where Medusa is
                if(medusaAgent.pathStatus == NavMeshPathStatus.PathComplete && medusaAgent.remainingDistance < (medusaAgent.stoppingDistance + .5f)) {
                    //Turn medusa to face player, check to see if rotation is complete
                    Vector3 playerPos = playerHealth.gameObject.transform.position;
                    Vector3 medusaForward = gameObject.transform.forward;
                    float angle = Vector3.SignedAngle(playerPos, medusaForward, Vector3.up);
                    int angleOfAccuracy = 10;

                    if(angle <= angleOfAccuracy && angle >= -angleOfAccuracy) {

                        if(!activatedRange)
                        {
                            //Activate Ranged Attacks
                            MedusaAttack(rangedAttacks[currentRanged]);
                            currentRanged++;
                            if(currentRanged > rangedAttacks.Count) {
                                currentRanged = 0;
                            }
                        }
                        activatedRange = true;                        
                    }
                }//if path complete
                break;
            }
            case AttackStates.PlayerChase3: {
                //Change Medusa Properties
                //Function()

                //Timer
                if(player1Timer <= Time.time - lastTime || timesAttacked > maxAttacks) {
                    CurrentState = AttackStates.RandomPlatform;
                    medusaAgent.SetDestination(MainPlatform.position);
                    lastTime = Time.time;
                }
                break;
            }
            case AttackStates.MainPlatform: {
                //Reset the attack list to Zero

                //Check where Medusa is
                if(medusaAgent.pathStatus == NavMeshPathStatus.PathComplete && medusaAgent.remainingDistance < (medusaAgent.stoppingDistance + .5f)) {
                    //Turn medusa to face player, check to see if rotation is complete
                    Vector3 playerPos = playerHealth.gameObject.transform.position;
                    Vector3 medusaForward = gameObject.transform.forward;
                    float angle = Vector3.SignedAngle(playerPos, medusaForward, Vector3.up);
                    int angleOfAccuracy = 10;

                    if(angle <= angleOfAccuracy && angle >= -angleOfAccuracy) {

                        if(!activatedRange)
                        {
                            //Activate Ranged Attacks
                            MedusaAttack(rangedAttacks[currentRanged]);
                            currentRanged++;
                            if(currentRanged > rangedAttacks.Count) {
                                currentRanged = 0;
                            }
                        }
                        activatedRange = true;                        
                    }
                }//if path complete
                break;
            }
            case AttackStates.Final: {
                //Final Freeze
                //Death Animation
                //Cutscene
                break;
            }
            default: {
                break;
            }
        }//end attack stage switch

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

    //Change attack states externally
    public void ChangeState(AttackStates desiredState) {
        
    }

    public int ChooseDestination() {
        return (int) Random.Range(0, waypoints.Count);
    }//end ChooseDestination

    //Call for Attack
    public void MedusaAttack(AttackType _type) 
    {
        /* //Check that we are not in Free Move before performing Attack
        if (!freeMove)
        { */
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
        //}
    }

    //Call for Defend
    public void MedusaDefend() 
    {
        /* if (!freeMove) 
        { */
        if (anim != null) 
        {
            anim.SetTrigger("Defend");
            //Code for Defend
        }
        //}
    }

    //Call for Damage (unsure if we are using integers or float damage
    public void MedusaDamage(float _amount)
    {
        Health -= _amount;
        if (Health <= 0) 
        {
            Destroy(this.gameObject, deathDelay);
            /* if (!freeMove)
            { */
            if (anim != null)
            {
                anim.SetTrigger("Death");
                //Code for Death (other than Destroy, as that has been called)
                return;
            }
            //}
        }

        //if (!freeMove)
        //{
        if (anim != null)
        {
            anim.SetTrigger("Damage");
            //Code for Damage (other than Health subtraction, as that as run)
        }
        //}
    }

    //Called in Update to Animate FreeMovement Based on Speed and Direction
    void RunFreeMoveBlend() 
    {
        //Set Forward Speed for Blend Tree to variable forwardSpeed, unless it is out of Range
        if (forwardSpeed <= 1 && forwardSpeed >= 0)
        {
            anim.SetFloat("ForwardSpeed", forwardSpeed);
        }
        else
        {
            if (forwardSpeed < 0)
                anim.SetFloat("ForwardSpeed", 0);
            if (forwardSpeed > 1)
                anim.SetFloat("ForwardSpeed", 1);
        }
        //Set Side Speed for Blend Tree to variable sideSpeed, unless it is out of Range
        if (sideSpeed <= 1 && sideSpeed >= -10)
        {
            anim.SetFloat("SideSpeed", sideSpeed);
        }
        else
        {
            if (sideSpeed < -1)
                anim.SetFloat("SideSpeed", -1);
            if (sideSpeed > 1)
                anim.SetFloat("SideSpeed", 1);
        }
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
