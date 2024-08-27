using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]

public class Enemy : MythologyMayhem
{
    public Level inScene;

    [Header("2D Components")]
    public Rigidbody2D rigidBody2D;
    public SpriteRenderer spriteRenderer;

    [Header("3D Components")]
    public Rigidbody rigidBody3D;
    public NavMeshAgent agent;

    [Header("Audio Components")]
    AudioSource audioSource;
    public AudioClip[] attackSounds;
    public AudioClip[] hurtSounds;
    public AudioClip[] deathSounds;
    public enum Soundtype
    {
        Idle,
        Attack,
        Hurt,
        Death
    }

    [Header("Stats")]
    public Dimension enemyDimension;
    public Health health;
    public Animator animator;
    public GameObject player;
    [SerializeField] public float attackRate;
    public float attackDamage;
    [SerializeField] private bool canAttack = true;
    [SerializeField] public GameObject attackCollider;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    [SerializeField] float patrolDistance = 2f;
    float flipSensitivity = 1f;
    [SerializeField] float speed = 2f;
    [SerializeField] string patrolBool = "IsPatrolling";
    [SerializeField] bool isFlying = false;

    [Header("Attack")]
    [SerializeField] private string[] attackTriggers;
    [SerializeField] string attackTrigger = "Attack";
    [SerializeField] float meleeDistance = 4f;

    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex = 0;
    public Vector3 target;
    public float idleTimerMax = 3f;
    public float idleTimer;

    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public bool hasIdleDelegate, hasPatrolDelegate, hasAttackDelegate, hasDeadDelegate;
    public UnityEvent IdleDelegate;
    public UnityEvent PatrolDelegate;
    public UnityEvent AttackDelegate;
    public UnityEvent DeadDelegate;
    public LocalGameManager _localGameManager;

    public enum EnemyStates { Idle, Patrol, Attack, Dead };
    public EnemyStates currentState;
    public enum StatePosition { Exit, Current, Entry }; //0 = exit, 1 = current, 2 = entry
    public StatePosition currentStatePosition;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        health = gameObject.GetComponent<Health>();

        currentState = EnemyStates.Idle;
        currentStatePosition = StatePosition.Entry;
        StartCoroutine(SwitchStates(currentState, 0));

        _localGameManager = transform.root.GetComponent<LocalGameManager>();

        foreach (LocalGameManager lgm in GameObject.FindObjectsOfType<LocalGameManager>())
        {
            if (lgm.inScene.ToString() == gameObject.scene.name) _localGameManager = lgm;
        }
    }

    public void Reset()
    {
        currentState = EnemyStates.Idle;
        currentStatePosition = StatePosition.Entry;
        StartCoroutine(SwitchStates(currentState, 0));
    }

    void Update()
    {
        ////set reference to player if in a scene using the StartScene feature

        if (_localGameManager != null) if (player == null) if (_localGameManager.player != null) player = _localGameManager.player.gameObject;

        //Check for Death

        if (health.isDead && currentState != EnemyStates.Dead) StartCoroutine(SwitchStates(EnemyStates.Dead, 0));
        else if (health.isDead) return;
        // If not transitioning states, invoke state action
        if (currentStatePosition == StatePosition.Current)
        {
            switch (currentState)
            {
                case EnemyStates.Idle:
                    {
                        if (DetectPlayer()) StartCoroutine(SwitchStates(EnemyStates.Attack, 0));
                        else if (enemyDimension == Dimension.TwoD) // if 2D
                        {
                            if (idleTimer <= 0)
                            {
                                animator.SetBool(patrolBool, true);
                                StartCoroutine(SwitchStates(EnemyStates.Patrol, 0));
                            }
                            else
                            {
                                animator.SetBool(patrolBool, false);
                                idleTimer -= Time.deltaTime;
                            }
                        }
                        else // if 3D
                        {
                            if (idleTimer <= 0) StartCoroutine(SwitchStates(EnemyStates.Patrol, 0));
                            else idleTimer -= Time.deltaTime;
                        }

                        if (hasIdleDelegate) IdleDelegate.Invoke();

                        break;
                    }
                case EnemyStates.Patrol:
                    {
                        if (DetectPlayer()) StartCoroutine(SwitchStates(EnemyStates.Attack, 0));
                        else if (enemyDimension == Dimension.TwoD) // if 2D
                        {
                            if (Vector3.Distance(transform.position, target) < patrolDistance) StartCoroutine(SwitchStates(EnemyStates.Idle, 0));
                            else MoveTo2DTarget(false);
                        }
                        else // if 3D
                        {
                            if (Vector3.Distance(transform.position, target) < agent.stoppingDistance) StartCoroutine(SwitchStates(EnemyStates.Idle, 0));
                            else transform.forward = agent.velocity;
                        }

                        if (hasPatrolDelegate) PatrolDelegate.Invoke();

                        break;
                    }
                case EnemyStates.Attack:
                    {
                        if (!DetectPlayer()) StartCoroutine(SwitchStates(EnemyStates.Idle, 0));
                        if (!GameManager.instance.isPlayerAlive) StartCoroutine(SwitchStates(EnemyStates.Idle, 0));
                        else if (enemyDimension == Dimension.TwoD) // if 2D
                        {
                            if (Vector3.Distance(transform.position, target) < meleeDistance && CanAttack)
                            {
                                CanAttack = false;

                                player.GetComponent<PlayerStats>().TakeDamage(attackDamage);

                                animator.SetTrigger(attackTrigger);

                                PlaySound(Soundtype.Attack);

                                if (player.GetComponent<KnockBackFeedback>()) player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);                                

                                StartCoroutine(AttackRate());
                            }
                            else if (Vector3.Distance(transform.position, target) > meleeDistance)
                            {
                                MoveTo2DTarget(true);
                            }
                        }
                        else // if 3D
                        {
                            if (Vector3.Distance(transform.position, target) < agent.stoppingDistance && CanAttack)
                            {
                                CanAttack = false;

                                player.GetComponent<FPSHealth>().TakeDamage(attackDamage);

                                animator.SetTrigger(attackTriggers[UnityEngine.Random.Range(0, attackTriggers.Length)]);

                                PlaySound(Soundtype.Attack);

                                if (player.GetComponent<KnockBackFeedback>()) player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);                                

                                StartCoroutine(AttackRate());
                            }
                            else if (Vector3.Distance(transform.position, player.transform.position) > agent.stoppingDistance)
                            {
                                transform.forward = agent.velocity;

                                agent.speed = runSpeed;
                                animator.SetFloat("Speed", runSpeed);
                                agent.SetDestination(target);
                            }
                        }                                                    
                        if (hasAttackDelegate) AttackDelegate.Invoke();
                        break;
                    }
                case EnemyStates.Dead:
                    {
                        health.Death();

                        if (enemyDimension == Dimension.TwoD) GetComponent<BoxCollider2D>().enabled = false;
                        else GetComponent<BoxCollider>().enabled = false;

                        currentStatePosition = StatePosition.Exit;

                        if (enemyDimension == Dimension.ThreeD) animator.SetFloat("Speed", 0);

                      

                        break;
                    }
                default: { break; }
            }
        }
    }//end update

    public IEnumerator SwitchStates(EnemyStates newState, float delay)
    {
        currentStatePosition = StatePosition.Exit;
        ExitState(currentState);
        yield return new WaitForSeconds(delay);
        currentStatePosition = StatePosition.Entry;
        currentState = newState;
        EnterState(currentState);
        currentStatePosition = StatePosition.Current;
    }

    public void EnterState(EnemyStates newState)
    {
        switch (newState)
        {
            case EnemyStates.Idle:
                {
                    if (enemyDimension == Dimension.TwoD) animator.SetBool(patrolBool, false);
                    else animator.SetFloat("Speed", 0);

                    idleTimer = idleTimerMax;
                    break;
                }
            case EnemyStates.Patrol:
                {
                    if (enemyDimension == Dimension.TwoD) // if is 2D
                    {
                        target = waypoints[waypointIndex].position;

                        if (waypoints.Length > 0)
                        {
                            if (waypointIndex >= waypoints.Length - 1) waypointIndex = 0;
                            else waypointIndex++;
                        }
                        animator.SetBool(patrolBool, true);
                    }
                    else // if is 3D
                    {
                        target = waypoints[waypointIndex].position;

                        if (waypoints.Length > 0)
                        {
                            if (waypointIndex >= waypoints.Length - 1) waypointIndex = 0;
                            else waypointIndex++;                            
                        }
                        else
                        {
                            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolDistance;
                            randomDirection += transform.position;
                            NavMeshHit hit;
                            NavMesh.SamplePosition(randomDirection, out hit, patrolDistance, 1);
                            target = hit.position;
                        }
                        agent.speed = walkSpeed;
                        animator.SetFloat("Speed", walkSpeed);
                        agent.SetDestination(target);
                    }
                    break;
                }
            case EnemyStates.Attack:
                {
                    if (enemyDimension == Dimension.ThreeD)
                    {
                        animator.SetFloat("Speed", 0);
                        agent.speed = 0;
                    }

                    canAttack = true;
                    break;
                }
            case EnemyStates.Dead:
                {
                    if (enemyDimension == Dimension.ThreeD)
                    {
                        animator.SetFloat("Speed", 0);
                        agent.speed = 0;
                    }
                    break;
                }
            default: { break; }
        }
    }

    public void ExitState(EnemyStates oldState)
    {
        currentStatePosition = StatePosition.Exit;

        switch (oldState)
        {
            case EnemyStates.Idle:
                {
                    break;
                }
            case EnemyStates.Patrol:
                {
                    break;
                }
            case EnemyStates.Attack:
                {
                    canAttack = false;
                    break;
                }
            case EnemyStates.Dead:
                {
                    canAttack = false;
                    break;
                }
            default: { break; }
        }
    }

    public IEnumerator AttackRate()
    {
        yield return new WaitForSeconds(attackRate);

        canAttack = true;
    }

    public bool DetectPlayer()
    {
        if (health.isDead) return false;
        if (enemyDimension == Dimension.TwoD) // if 2D
        {
            if (attackCollider.GetComponent<TriggerDetector2D>().triggered)
            {
                foreach (Collider2D go in attackCollider.GetComponent<TriggerDetector2D>().otherColliders2D)
                {
                    if (go.CompareTag("Player"))
                    {
                        target = go.transform.position;
                        player = go.gameObject;
                        return true;
                    }
                }
            }
        }
        else // if 3D
        {
            if (attackCollider.GetComponent<TriggerDetector3D>().triggered)
            {
                foreach (Collider go in attackCollider.GetComponent<TriggerDetector3D>().otherColliders3D)
                {
                    if (go.CompareTag("Player"))
                    {
                        target = go.transform.position;
                        player = go.gameObject;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void PlaySound(Soundtype soundType)
    {
        switch (soundType)
        {
            case Soundtype.Attack:
                PlaySoundAlgorithm(attackSounds);
                break;
            case Soundtype.Hurt:
                PlaySoundAlgorithm(hurtSounds);
                break;
            case Soundtype.Death:
                PlaySoundAlgorithm(deathSounds);
                break;
        }
    }

    void PlaySoundAlgorithm(AudioClip[] clips)
    {
        if (clips.Length >= 1)
        {
            int randomSoundIndex = UnityEngine.Random.Range(0, clips.Length);
            audioSource.PlayOneShot(clips[randomSoundIndex]);
        }
    }

    void MoveTo2DTarget(bool isAttacking)
    {
        float currentSpeed = 0f;
        if (isAttacking) currentSpeed = speed * 1.5f;
        else currentSpeed = speed;

        if (target.x + flipSensitivity > transform.position.x && transform.rotation.y != 180)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (target.x + flipSensitivity < transform.position.x && transform.rotation.y != 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        Vector2 xOnlyTargetPosition;

        if (isFlying) xOnlyTargetPosition = target;
        else xOnlyTargetPosition = new Vector2(target.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, xOnlyTargetPosition, currentSpeed * Time.deltaTime);
    }

    void MoveTo3DTarget(bool isAttacking)
    {
        if (isAttacking)
        {
            agent.speed = runSpeed;
            animator.SetFloat("Speed", runSpeed);
        }
        else
        {
            agent.speed = walkSpeed;
            animator.SetFloat("Speed", walkSpeed);
        }

        agent.SetDestination(target);

        Vector3 direction = target - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
