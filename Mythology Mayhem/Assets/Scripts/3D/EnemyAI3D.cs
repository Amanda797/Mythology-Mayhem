using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3D : MonoBehaviour
{

    private float fixedDeltaTime;

    [Header("Movement")]
    int patrolSpeed = 40;
    int attackSpeed = 60;

    [Header("Navigation")]
    public UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    [Header("Combat")]
    bool isAttacking = false;
    int attackDamage = 2;
    int maxHealth = 10;
    int health;
    Collider player;
    

    void Awake() {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking) {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(transform.position, target) < 1) {
                IterateWaypointDestination();
                UpdateDestination();
            }
        }//end not attacking
        else {
            agent.speed = attackSpeed;
            agent.SetDestination(target);
            if(player != null && player.GetComponent<PlayerController3D>().GetHealth() > 0) {
                StartCoroutine(Attack(player));
            }
        }//end attacking
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
