using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy : MythologyMayhem
{
    public Animator animator;
    public Rigidbody rigidBody3D;
    public Rigidbody2D rigidBody2D;
    public SpriteRenderer spriteRenderer;
    public NavMeshAgent agent;
    public Health health;

    [Header("Stats")]
    [SerializeField] private Enemies enemyType;
    public float attackDamage;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private bool canAttack = true;
    [SerializeField] public float attackRate;
    public GameObject player;

    [Header("Enemy Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Enemy Waypoints")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex = 0;
    public Vector3 target;
    public float idleTimerMax = 3f;
    public float idleTimer;

    public bool CanAttack {
        get {return canAttack;} 
        set {canAttack = value;}
    }

    public UnityEvent IdleDelegate;
    public UnityEvent<Vector3> PatrolDelegate;
    public UnityEvent<GameObject> AttackDelegate;
    public UnityEvent DeadDelegate;

    public enum EnemyStates { Idle, Patrol, Attack, Dead };
    public EnemyStates currentState;
    public enum StatePosition { Exit, Current, Entry }; //0 = exit, 1 = current, 2 = entry
    public StatePosition currentStatePosition;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();

        currentState = EnemyStates.Idle;
        currentStatePosition = StatePosition.Entry;
        EnterState(currentState);
    }

    public void PrintState()
    {
        Debug.Log(currentState);
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.Idle:
                {
                    IdleDelegate.Invoke();
                    break;
                }
            case EnemyStates.Patrol:
                {
                    PatrolDelegate.Invoke(target);
                    break;
                }
            case EnemyStates.Attack:
                {
                    AttackDelegate.Invoke(player);
                    break;
                }
            case EnemyStates.Dead:
                {
                    DeadDelegate.Invoke();
                    break;
                }
            default: { break; }
        }
    }

    public void SwitchStates(EnemyStates newState)
    {
        ExitState(currentState);
        currentState = newState;
        EnterState(currentState);
    }//switch states

    public void EnterState(EnemyStates newState)
    {
        switch (newState)
        {
            case EnemyStates.Idle:
                {
                    idleTimer = idleTimerMax;
                    currentStatePosition = StatePosition.Current;
                    break;
                }
            case EnemyStates.Patrol:
                {
                    //Update waypoint
                    if(waypointIndex >= waypoints.Length - 1)
                    {
                        waypointIndex = 0;
                    } else
                    {
                        waypointIndex++;
                    }

                    //Update target
                    target = waypoints[waypointIndex].position;

                    //Update Nav mesh agent
                    if(agent != null)
                    {
                        agent.speed = walkSpeed;
                    }

                    currentStatePosition = StatePosition.Current;
                    break;
                }
            case EnemyStates.Attack:
                {
                    //Update target
                    target = player.transform.position;

                    //Update Nav mesh agent
                    if (agent != null)
                    {
                        agent.speed = runSpeed;
                    }

                    currentStatePosition = StatePosition.Current;
                    break;
                }
            case EnemyStates.Dead:
                {
                    currentStatePosition = StatePosition.Current;
                    break;
                }
            default: { break; }
        }

        PrintState();
    }

    public void ExitState(EnemyStates newState)
    {
        switch (newState)
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
                    break;
                }
            case EnemyStates.Dead:
                {
                    break;
                }
            default: { break; }
        }
    }

    public void TakeDamage(float damage) 
    {
        health.Life -= damage;

        animator.SetTrigger("Hurt");

        if(health.Life <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        if(animator != null) {
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsDead", true);
        }

        Debug.Log("Enemy Died");

        if(GetComponent<Collider2D>()) {
            GetComponent<Collider2D>().enabled = false;
        }
        if(GetComponent<Rigidbody2D>()) {
            GetComponent<Rigidbody2D>().simulated = false;
        }
        if(GetComponent<KnockBackFeedback>()) {
            GetComponent<KnockBackFeedback>().enabled = false;
        }
        if(GetComponent<MouseAI>()) {
            GetComponent<MouseAI>().dead = true;
            GetComponent<MouseAI>().enabled = false;
        }
        if(GetComponent<Bat2DAI>()) {
            GetComponent<Bat2DAI>().enabled = false;
        }
        if(GetComponent<DropScrolls>() != null) {
            GetComponent<DropScrolls>().enabled = false;
        }

        StartCoroutine(Disappear());
    }

    IEnumerator Disappear() {
        yield return new WaitForSeconds(4);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            SwitchStates(EnemyStates.Attack);            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            SwitchStates(EnemyStates.Attack);
        }
    }

    public void Attack(GameObject target) {
        if (target.tag == "Player")
        {
            if(canAttack) {
                if(target.GetComponent<PlayerStats>())
                    target.GetComponent<PlayerStats>().TakeDamage(attackDamage);
                if(target.GetComponent<KnockBackFeedback>())
                    target.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
                canAttack = false;
                StartCoroutine(AttackRate());
            }
            
        }
    }

    public IEnumerator AttackRate() {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}
