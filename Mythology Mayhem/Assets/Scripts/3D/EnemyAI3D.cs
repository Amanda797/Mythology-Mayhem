using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3D : MonoBehaviour
{

    private float fixedDeltaTime;

    [Header("Animation")]
    [SerializeField] Animator anim;
    [SerializeField] string walkingAnim;
    [SerializeField] string attackingAnim;
    [SerializeField] string attackAnim;

    [Header("Movement")]
    [SerializeField] int patrolSpeed = 20;
    [SerializeField] int attackSpeed = 40;
    [SerializeField] float idleTimer = 0f;
    [SerializeField] float idleDuration = 5f;

    [Header("Navigation")]
    [SerializeField] UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    [Header("Combat")]
    [SerializeField] int attackDamage = 2;
    [SerializeField] Collider player;
    bool isAttacking = false;
    
    void Awake() {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking) {
            anim.SetBool(walkingAnim, true);
            anim.SetBool(attackingAnim, false);
            agent.speed = patrolSpeed;
            if (Vector3.Distance(transform.position, target) < 2) {
                Idle();              
            }
        }//end not attacking
        else {
            anim.SetBool(attackingAnim, true);
            anim.SetBool(walkingAnim, false);
            agent.speed = attackSpeed;
            agent.SetDestination(target);
            if(player != null && player.GetComponent<PlayerController3D>().GetHealth() > 0) {
                StartCoroutine(Attack(player));
            }
        }//end attacking
    }

    void Idle() {
        anim.SetBool(walkingAnim, false);
        agent.speed = 0f;
        idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration) {
            idleTimer = 0f;
            agent.speed = patrolSpeed;
            anim.SetBool(walkingAnim, true);
            Move();
        }
    }

    void Move() {
        IterateWaypointDestination();
        UpdateDestination();
    }

    void UpdateDestination() {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }//end Update Destination

    void IterateWaypointDestination() {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player") {
            Debug.Log("Hit Player!");
            isAttacking = true;
            target = col.gameObject.transform.position;

            //Debug.Log(Vector3.Distance(transform.position, target));

            if (Vector3.Distance(transform.position, target) < 10) {
                player = col;
            }
        }
    }

    IEnumerator Attack(Collider col) {
        col.gameObject.GetComponent<PlayerController3D>().DamageHealth(attackDamage); 
        anim.SetTrigger(attackAnim);
        yield return new WaitForSeconds(5.0f);  
    }

    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player") {
            Debug.Log("No Player!");
            isAttacking = false;
            player = null;
            UpdateDestination();
        }
    }



}
