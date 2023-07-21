using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MedusaControlScript : MonoBehaviour
{
    [SerializeField] AttackStates CurrentState;

    //Public for testing, revert later
    public float health;
    public float startingHealth;
    //Delay for Death animation to play
    public float deathDelay;
    //public bool freeMove;
    //Forward and Side Speeds for BlendTree
    [Range(0, 1)]
    public float forwardSpeed;
    [Range(-1, 1)]
    public float sideSpeed;

    public float targetingSpeed;
    public Animator anim;
    [SerializeField] NavMeshAgent medusaAgent;
    [SerializeField] Health playerHealth;

    public float baseSpeed;
    public float fastSpeed;
    public float enragedMultiplier;
    public bool invunerable;

    public bool navStopped;

    [Header("Timers")]
    [SerializeField] float introTime = 5f;
    float cooldown;
    [SerializeField] float lastMeleeTime;
    [SerializeField] float meleeCooldown;
    [SerializeField] float navMeleeStopCooldown;
    [SerializeField] float lastRangedTime;
    [SerializeField] float rangedCooldown1;
    [SerializeField] float rangedCooldown2;
    [SerializeField] float rangedCooldown3;
    [SerializeField] int currentRangedAttackCount;
    [SerializeField] int totalRangedAttacks1;
    [SerializeField] int totalRangedAttacks2;
    [SerializeField] int totalRangedAttacks3;
    [SerializeField] float freezeAttemptStart;
    [SerializeField] float freezeAttemptTotal;
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

    [Header("UI")]
    [SerializeField] Transform healthBarCanvasPivot;
    [SerializeField] Text totalHealthDisplay;
    [SerializeField] Slider healthBar;

    [SerializeField] GameObject spellPrefab;
    [SerializeField] Transform spellSpawnPoint;

    [SerializeField] LineRenderer leftEyeLine;
    [SerializeField] LineRenderer rightEyeLine;
    [SerializeField] Transform[] eyeTargetPoints;

    [SerializeField] Material mainMaterial;
    [SerializeField] Texture normalTex;
    [SerializeField] Texture enragedTex;
    [SerializeField] Texture stoneTex;

    public enum AttackType
    {
        CastSpell,
        HairShake,
        Projectile,
        Stab
    }

    public enum AggroStates
    {
        Enraged,
        Stone,
        Normal,
        NearDeath
    }

    public enum AttackStates
    {
        Start,
        PlayerChase1,
        MoveToRandomPlatform,
        RangedAttack1,
        AttemptToFreeze1,
        FrozenSelf1,
        EndStage1,
        PlayerChase2,
        MoveToRemainingPlatform,
        RangedAttack2,
        AttemptToFreeze2,
        FrozenSelf2,
        EndStage2,
        PlayerChase3,
        MoveToHomeAltar,
        RangedAttack3,
        AttemptToFreeze3,
        FrozenSelf3,
        EndStage3,
        Final
    }
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        CurrentState = AttackStates.Start;

        totalHealthDisplay.text = startingHealth.ToString() + "/" + startingHealth.ToString();
        health = startingHealth;
        healthBar.maxValue = health;
        invunerable = true;
        healthBarCanvasPivot.gameObject.SetActive(false);

        leftEyeLine.enabled = false;
        rightEyeLine.enabled = false;

        medusaAgent.speed = baseSpeed;
        mainMaterial.SetTexture("_MainTex", normalTex);
    }
    // Update is called once per frame
    void Update()
    {
        //Calls Every Frame if in Free Move Mode
        RunFreeMoveBlend();

        // Run Attack State Logic
        RunStateMachine();

        totalHealthDisplay.text = health.ToString() + "/" + startingHealth.ToString();
        healthBar.value = health;

        PointCanvasTowardPlayer(playerHealth.transform);
    }
    void RunStateMachine()
    {
        // Check Attack Stage
        switch (CurrentState)
        {
            case AttackStates.Start:
                {
                    //If at the Start, Medusa should persue the player from the Main Platform. Change state to PlayerChase.
                    //Could add cutsceene here
                    //Timer
                    if (introTime <= Time.time)
                    {
                        SetAggroState(AggroStates.Normal);
                        healthBarCanvasPivot.gameObject.SetActive(true);
                        lastMeleeTime = Time.time;
                        CurrentState = AttackStates.PlayerChase1;
                    }
                    break;
                }
            case AttackStates.PlayerChase1:
                {
                    RunPlayerChase();
                    break;
                }
            case AttackStates.MoveToRandomPlatform:
                {
                    RunMoveToPlatform(AttackStates.RangedAttack1);
                    break;
                }
            case AttackStates.RangedAttack1:
                {
                    RunRangedAttack(rangedCooldown1, totalRangedAttacks1, AttackStates.AttemptToFreeze1);
                    break;
                }
            case AttackStates.AttemptToFreeze1:
                {
                    RunAttemptToFreeze(AttackStates.FrozenSelf1);
                    break;
                }
            case AttackStates.FrozenSelf1:
                {
                    break;
                }
            case AttackStates.EndStage1:
                {
                    break;
                }
            case AttackStates.PlayerChase2:
                {
                    RunPlayerChase();
                    break;
                }
            case AttackStates.MoveToRemainingPlatform:
                {
                    RunMoveToPlatform(AttackStates.RangedAttack2);
                    break;
                }
            case AttackStates.RangedAttack2:
                {
                    RunRangedAttack(rangedCooldown2, totalRangedAttacks2, AttackStates.AttemptToFreeze2);
                    break;
                }
            case AttackStates.AttemptToFreeze2:
                {
                    RunAttemptToFreeze(AttackStates.FrozenSelf2);
                    break;
                }
            case AttackStates.FrozenSelf2:
                {
                    break;
                }
            case AttackStates.EndStage2:
                {
                    break;
                }
            case AttackStates.PlayerChase3:
                {
                    RunPlayerChase();
                    break;
                }
            case AttackStates.MoveToHomeAltar:
                {
                    RunMoveToPlatform(AttackStates.RangedAttack3);
                    break;
                }
            case AttackStates.RangedAttack3:
                {
                    RunRangedAttack(rangedCooldown3, totalRangedAttacks3, AttackStates.AttemptToFreeze3);
                    break;
                }
            case AttackStates.AttemptToFreeze3:
                {
                    RunAttemptToFreeze(AttackStates.FrozenSelf3);
                    break;
                }
            case AttackStates.FrozenSelf3:
                {
                    break;
                }
            case AttackStates.EndStage3:
                {
                    break;
                }
        }
    }
    public void RunPlayerChase()
    {
        if (navStopped && (Time.time - lastMeleeTime) >= navMeleeStopCooldown)
        {
            medusaAgent.isStopped = false;
            navStopped = false;
        }
        medusaAgent.stoppingDistance = 8;
        //Look to Line up with Nav Agent (incase not aligned from targeting phase)
        float step = targetingSpeed * Time.deltaTime;
        Vector3 direction = (medusaAgent.transform.forward).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * targetingSpeed);

        //Set Current Player location as Destination
        medusaAgent.SetDestination(playerHealth.gameObject.transform.position);

        //Check player is within Distance and if Cooldown is complete
        if (medusaAgent.remainingDistance < (medusaAgent.stoppingDistance + .5f) && (Time.time - lastMeleeTime) >= meleeCooldown)
        {
            medusaAgent.isStopped = true;
            navStopped = true;
            //Perform Melee attack, cycle melee from list, and start cooldown timer                       
            MedusaMeleeAttack(meleeAttacks[currentMelee]);
            SetNextMelee();
            lastMeleeTime = Time.time;
        }

    }
    public void RunMoveToPlatform(AttackStates nextState) 
    {
        if (medusaAgent.remainingDistance < (medusaAgent.stoppingDistance + .5f))
        {
            //Stop NavAgent and set State
            medusaAgent.isStopped = true;
            navStopped = true;
            CurrentState = nextState;
        }
    }
    public void RunRangedAttack(float rangeCooldown, int totalAttacks, AttackStates nextState) 
    {
        //Look Towards Player
        float step = targetingSpeed * Time.deltaTime;
        Vector3 direction = (playerHealth.transform.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * targetingSpeed);

        //Calculated Angle of player to current facing direction
        Vector3 playerDirection = playerHealth.transform.position - transform.position;
        Vector3 medusaForward = transform.forward;
        float angle = Vector3.SignedAngle(medusaForward, playerDirection, Vector3.up);
        int angleOfAccuracy = 10;

        //If Medusa's cone of vision is within [-angleOfAccuracy 0 angleOfAccuracy], the ranged cooldown has been reached, and she still has Spell count to reach
        if (angle <= angleOfAccuracy && angle >= -angleOfAccuracy && (Time.time - lastRangedTime) >= rangeCooldown && currentRangedAttackCount < totalAttacks)
        {
            Vector3 target = playerHealth.transform.position;
            target.y = 0;
            //Activate Ranged Attacks and start cooldown timer;
            MedusaRangedAttack(AttackType.CastSpell, target);
            currentRangedAttackCount++;
            lastRangedTime = Time.time;

        }
        //If Medusa's cone of vision is within [-angleOfAccuracy 0 angleOfAccuracy], the ranged cooldown has been reached, and she has reached spell, being able to perform projectile
        else if (angle <= angleOfAccuracy && angle >= -angleOfAccuracy && (Time.time - lastRangedTime) >= rangeCooldown && currentRangedAttackCount >= totalAttacks)
        {
            Vector3 target = playerHealth.transform.position;
            //Activate Ranged Attacks, using , and start cooldown timer;
            MedusaRangedAttack(AttackType.Projectile, target);
            currentRangedAttackCount = 0;
            lastRangedTime = Time.time;
            leftEyeLine.enabled = true;
            rightEyeLine.enabled = true;
            freezeAttemptStart = Time.time;
            CurrentState = nextState;
        }
    }
    public void RunAttemptToFreeze(AttackStates nextState) 
    {
        float timeSinceFreezeStart = Time.time - freezeAttemptStart;
        if (timeSinceFreezeStart < freezeAttemptTotal /*&& !playerSuccessFreeze1*/)
        {
            //Automatically Look directly toward player
            Vector3 direction = (playerHealth.transform.position - transform.position).normalized;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
            print(leftEyeLine.transform.position + " " + playerHealth.transform.position);
            leftEyeLine.SetPosition(0, leftEyeLine.transform.position);
            rightEyeLine.SetPosition(0, rightEyeLine.transform.position);
            eyeTargetPoints[0].position = playerHealth.transform.position;
            leftEyeLine.SetPosition(1, eyeTargetPoints[1].position);
            rightEyeLine.SetPosition(1, eyeTargetPoints[2].position);
        }
        else
        {
            /*
            if (!playerSuccessFreeze1)
            {
                leftEyeLine.enabled = false;
                rightEyeLine.enabled = false;

                CurrentState = AttackStates.PlayerChase1;
            }
            else
            {
                leftEyeLine.enabled = false;
                rightEyeLine.enabled = false;
                SetAggroState(AggroStates.Stone);
                CurrentState = AttackStates.FrozenSelf1;
            }
            */
            //testing direct change
            leftEyeLine.enabled = false;
            rightEyeLine.enabled = false;
            SetAggroState(AggroStates.Stone);
            CurrentState = nextState;
        }
    }
    public int ChooseDestination()
    {
        return (int)Random.Range(0, waypoints.Count);
    }//end ChooseDestination
    //Call for Attack
    public void MedusaMeleeAttack(AttackType _type)
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
                case AttackType.HairShake:
                    anim.SetTrigger("HairShake");
                    HairShake();
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
    public void MedusaRangedAttack(AttackType _type, Vector3 target)
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
                    CastSpell(target);
                    break;

                case AttackType.Projectile:
                    anim.SetTrigger("Projectile");
                    Projectile();
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
        if (!invunerable)
        {
            health -= _amount;
            if (CurrentState == AttackStates.PlayerChase1)
            {
                if (health <= 800)
                {
                    health = 800;
                    SetAggroState(AggroStates.Enraged);
                    medusaAgent.stoppingDistance = 3;
                    medusaAgent.SetDestination(waypoints[0].position);
                    CurrentState = AttackStates.MoveToRandomPlatform;
                }
                return;
            }
            if (CurrentState == AttackStates.FrozenSelf1)
            {
                if (health <= 700)
                {
                    health = 700;
                    Vector3 direction = (playerHealth.transform.position - medusaAgent.transform.position).normalized;
                    direction.y = 0;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    medusaAgent.transform.rotation = lookRotation;
                    SetAggroState(AggroStates.Normal);
                    medusaAgent.stoppingDistance = 8;
                    medusaAgent.isStopped = false;
                    navStopped = false;
                    CurrentState = AttackStates.PlayerChase2;
                }
            }
            if (CurrentState == AttackStates.PlayerChase2)
            {
                if (health <= 500)
                {
                    health = 500;
                    SetAggroState(AggroStates.Enraged);
                    medusaAgent.stoppingDistance = 3;
                    medusaAgent.SetDestination(waypoints[1].position);
                    CurrentState = AttackStates.MoveToRemainingPlatform;
                }
                return;
            }
            if (CurrentState == AttackStates.FrozenSelf2)
            {
                if (health <= 400)
                {
                    health = 400;
                    Vector3 direction = (playerHealth.transform.position - medusaAgent.transform.position).normalized;
                    direction.y = 0;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    medusaAgent.transform.rotation = lookRotation;
                    SetAggroState(AggroStates.NearDeath);
                    medusaAgent.stoppingDistance = 8;
                    medusaAgent.isStopped = false;
                    navStopped = false;
                    CurrentState = AttackStates.PlayerChase3;
                }
            }
            if (CurrentState == AttackStates.PlayerChase3)
            {
                if (health <= 200)
                {
                    health = 200;
                    SetAggroState(AggroStates.Enraged);
                    medusaAgent.stoppingDistance = 3;
                    medusaAgent.SetDestination(MainPlatform.position);
                    CurrentState = AttackStates.MoveToHomeAltar;
                }
                return;
            }
            if (CurrentState == AttackStates.FrozenSelf3)
            {
                if (health <= 0)
                {
                    health = 0;
                    anim.enabled = true;
                    medusaAgent.isStopped = true;
                    navStopped = true;

                    if (anim != null)
                    {
                        anim.SetTrigger("Death");
                        //Code for Death (other than Destroy, as that has been called)
                    }
                    medusaAgent.isStopped = true;
                    Destroy(this.gameObject.transform.parent.gameObject, deathDelay);
                    return;
                    //}
                }
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
    void CastSpell(Vector3 direction)
    {
        //Code for Spell Mechanics
        GameObject obj = Instantiate(spellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
        obj.transform.LookAt(direction);

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
    void PointCanvasTowardPlayer(Transform target)
    {
        if (healthBarCanvasPivot != null)
        {
            if (playerHealth != null)
            {
                healthBarCanvasPivot.LookAt(playerHealth.transform.position);
            }
        }
    }
    void SetAggroState(AggroStates state)
    {
        if (state == AggroStates.Normal)
        {
            anim.enabled = true;
            medusaAgent.speed = baseSpeed;
            invunerable = false;
            mainMaterial.SetTexture("_MainTex", normalTex);
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
            totalHealthDisplay.color = Color.green;
        }
        if (state == AggroStates.Enraged)
        {
            medusaAgent.isStopped = false;
            navStopped = false;
            anim.enabled = true;
            medusaAgent.speed *= enragedMultiplier;
            invunerable = true;
            mainMaterial.SetTexture("_MainTex", enragedTex);
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color32(143, 0, 254, 255);
            totalHealthDisplay.color = new Color32(143, 0, 254, 255);
        }
        if (state == AggroStates.Stone)
        {
            anim.enabled = false;
            invunerable = false;
            mainMaterial.SetTexture("_MainTex", stoneTex);
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.grey;
            totalHealthDisplay.color = Color.grey;
        }
        if (state == AggroStates.NearDeath)
        {
            anim.enabled = true;
            medusaAgent.speed = fastSpeed;
            invunerable = false;
            mainMaterial.SetTexture("_MainTex", normalTex);
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
            totalHealthDisplay.color = Color.red;
        }

    }
    void SetNextMelee()
    {
        currentMelee++;
        if (currentMelee == meleeAttacks.Count)
        {
            currentMelee = 0;
        }
    }
    void SetNextRanged()
    {
        currentRanged++;
        if (currentRanged == rangedAttacks.Count)
        {
            currentRanged = 0;
        }
    }
}
